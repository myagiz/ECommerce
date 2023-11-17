using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenService
    {
        public string CreateRefreshToken();
        public Token CreateAccessToken(User user);
    }
}
