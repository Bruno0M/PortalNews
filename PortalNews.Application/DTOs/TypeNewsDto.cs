namespace PortalNews.Application.DTOs
{
    public record TypeNewsDto(
        int Id,
        int JournalistId,
        string TypeName
        );
}