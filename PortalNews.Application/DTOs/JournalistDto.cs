using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNews.Application.DTOs
{
    public record JournalistDto(
        int Id,
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string ConfirmPassword);
}
