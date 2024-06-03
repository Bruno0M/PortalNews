using PortalNews.Application.DTOs;
using PortalNews.Domain.Entities;

namespace PortalNews.Infrastructure.Interfaces
{
    public interface INewsRepository
    {
        Task<ServiceResponse<NewsDto>> Create();
        Task<ServiceResponse<NewsDto>> Update(int newsId);
        Task<ServiceResponse<NewsDto>> Delete(int newsId);
        Task<ServiceResponse<NewsDto>> GetAllNews();
        Task<ServiceResponse<NewsDto>> GetNews(int typeId);

    }
}
