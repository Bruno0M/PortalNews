using System.Text.Json.Serialization;

namespace PortalNews.Domain.Entities
{
    public class News
    {
        public int Id { get; set; }
        public int JournalistId { get; set; }
        [JsonIgnore]
        public Journalist Journalist { get; set; }
        public int TypeNewsId { get; set; }
        [JsonIgnore]
        public TypeNews TypeNews { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string NewsBody { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
