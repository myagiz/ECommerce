using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAuthDal
    {
        Task RegisterAsync(RegisterDto model);
        Task<Token> LoginAsync(string emailAddress, string password);
    }
}
