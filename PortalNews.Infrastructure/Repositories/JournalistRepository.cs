using Microsoft.EntityFrameworkCore;
using PortalNews.Application.DTOs;
using PortalNews.Domain.Entities;
using PortalNews.Infrastructure.Data;
using PortalNews.Infrastructure.Interfaces;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace PortalNews.Infrastructure.Repositories
{
    public class JournalistRepository : IJournalistRepository
    {
        private readonly AppDbContext _context;
        private readonly ITokenRepository _tokenRepository;
        public JournalistRepository(AppDbContext context, ITokenRepository tokenRepository)
        {
            _context = context;
            _tokenRepository = tokenRepository;
        }

        public async Task<ServiceResponse<JournalistResult>> GetDataJournalist(int id)
        {
            var serviceResponse = new ServiceResponse<JournalistResult>();

            try
            {
                var journalistData = await _context.Journalists.FirstOrDefaultAsync(j => j.Id == id);

                if (journalistData == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Journalist not found";
                    serviceResponse.Status = HttpStatusCode.NotFound;
                    return serviceResponse;
                }

                var journalistResult = new JournalistResult
                (
                    journalistData.Id,
                    journalistData.Email,
                    journalistData.FirstName,
                    journalistData.LastName
                );

                serviceResponse.Data = journalistResult;
                serviceResponse.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<(ServiceResponse<JournalistLoginDto>, string)> Login(JournalistLoginDto journalistDTO)
        {
            var serviceResponse = new ServiceResponse<JournalistLoginDto>();

            try
            {
                var journalistData = await _context.Journalists.FirstOrDefaultAsync(j => j.Email == journalistDTO.Email);
                if (journalistData == null || !VerifyPassword(journalistDTO.Password, journalistData.PasswordHash, journalistData.PasswordSalt))
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Invalid Credentials";
                    serviceResponse.Status = HttpStatusCode.BadRequest;
                    return (serviceResponse, null);
                }

                string token = _tokenRepository.GenerateToken(journalistData);

                serviceResponse.Data = journalistDTO;
                serviceResponse.Message = "Successfully logged in";
                serviceResponse.Status = HttpStatusCode.OK;

                return (serviceResponse, token);
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                return (serviceResponse, null);
            }

        }

        public async Task<ServiceResponse<JournalistResult>> Register(JournalistDto journalistDTO)
        {
            var serviceResponse = new ServiceResponse<JournalistResult>();
            var journalistResult = new JournalistResult
            (
                journalistDTO.Id,
                journalistDTO.Email,
                journalistDTO.FirstName,
                journalistDTO.LastName
            );

            try
            {
                if (!VerifyUserAndEmailExist(journalistDTO))
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "This email is already registered";
                    serviceResponse.Status = HttpStatusCode.BadRequest;
                    return serviceResponse;
                }

                CreateHashPassword(journalistDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

                Journalist journalist = new Journalist
                {
                    FirstName = journalistDTO.FirstName,
                    LastName = journalistDTO.LastName,
                    Email = journalistDTO.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                _context.Add(journalist);
                await _context.SaveChangesAsync();

                serviceResponse.Data = journalistResult;
                serviceResponse.Message = "Journalist successfully registered";
                serviceResponse.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        private bool VerifyUserAndEmailExist(JournalistDto journalistDTO)
        {
            var journalist = _context.Journalists.FirstOrDefault(journalistData => journalistData.Email == journalistDTO.Email);

            if (journalist != null) return false;

            return true;
        }

        private void CreateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
