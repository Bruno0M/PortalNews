using System.Text.Json.Serialization;

namespace PortalNews.Domain.Entities
{
    public class TypeNews
    {
        public int Id { get; set; }
        public int JournalistId { get; set; }
        [JsonIgnore]
        public Journalist Journalist { get; set; }
        public string TypeName { get; set; }
    }
}
