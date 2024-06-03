using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNews.Application.DTOs
{
    public record JournalistLoginDto(
        string Email,
        string Password);
}
