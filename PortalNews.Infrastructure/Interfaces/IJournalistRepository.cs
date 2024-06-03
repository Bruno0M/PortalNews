using PortalNews.Application.DTOs;
using PortalNews.Domain.Entities;

namespace PortalNews.Infrastructure.Interfaces
{
    public interface IJournalistRepository
    {
        Task<ServiceResponse<JournalistResult>> Register(JournalistDto journalistDTO);
        Task<(ServiceResponse<JournalistLoginDto>, string)> Login(JournalistLoginDto journalistDTO);
        Task<ServiceResponse<JournalistResult>> GetDataJournalist(int id);
    }
}
