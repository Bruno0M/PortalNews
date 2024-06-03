using System.Net;

namespace PortalNews.Domain.Entities
{
    public class ServiceResponse <T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public HttpStatusCode Status { get; set; }
    }
}
