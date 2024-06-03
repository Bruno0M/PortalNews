using PortalNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNews.Infrastructure.Interfaces
{
    public interface ITokenRepository
    {
        public string GenerateToken(Journalist journalist);
    }
}
