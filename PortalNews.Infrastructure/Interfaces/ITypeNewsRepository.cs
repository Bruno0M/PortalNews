using PortalNews.Application.DTOs;
using PortalNews.Domain.Entities;

namespace PortalNews.Infrastructure.Interfaces
{
    public interface ITypeNewsRepository
    {
        Task<ServiceResponse<TypeNewsDto>> Create(TypeNewsDto typeNews);
        Task<ServiceResponse<TypeNewsDto>> Update(int typeId);
        Task<ServiceResponse<TypeNewsDto>> Delete(int typeId);
        Task<ServiceResponse<TypeNewsDto>> GetAllNews();
        Task<ServiceResponse<TypeNewsDto>> GetNews(int typeId);
    }
}
