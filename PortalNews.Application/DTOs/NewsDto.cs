namespace PortalNews.Application.DTOs
{
    public record NewsDto(
        int Id,
        string Title,
        string Description,
        string NewsBody
        );
}
