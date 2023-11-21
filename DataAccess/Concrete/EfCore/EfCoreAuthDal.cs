using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.DTOs;
using Entities.Entity;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreAuthDal : IAuthDal
    {
        private readonly IConfiguration _configuration;

        private readonly ITokenService _tokenService;

        public EfCoreAuthDal(IConfiguration configuration, ITokenService tokenService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<Token> LoginAsync(string emailAddress, string password)
        {
            using (var context = new ECommerceDbContext())
            {
                Token token = new Token();
                var getUser = await context.Users.FirstOrDefaultAsync(x => x.EmailAddress == emailAddress && x.Password == password);
                if (getUser != null)
                {
                    TokenService tokenService = new TokenService(_configuration);
                    var getRole = await context.UserRoles.Where(x => x.UserId == getUser.Id && x.IsActive == true).ToListAsync();

                    List<string> roles = new List<string>();

                    if (getRole.Count > 0)
                    {
                        foreach (var item in getRole)
                        {
                            string roleName = Enum.GetName(typeof(UserRoleTypesEnum), item.RoleId);
                            roles.Add(roleName);
                        }
                    }
                    token = _tokenService.CreateAccessToken(getUser, roles);
                    getUser.RefreshToken = token.RefreshToken;
                    getUser.TokenStartDate = DateTime.Now;
                    getUser.TokenExpiredDate = token.Expiration;
                    await context.SaveChangesAsync();
                    return token;
                }

                return token;
            }
        }

        public async Task RegisterAsync(RegisterDto model)
        {
            using (var context = new ECommerceDbContext())
            {
                User entity = new User();
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.EmailAddress = model.EmailAddress;
                entity.Password = model.Password;
                entity.IsTwoFactor = false;
                entity.IsConfirm = true;
                entity.IsActive = true;
                context.Users.Add(entity);
                await context.SaveChangesAsync();

                UserRole role = new UserRole();
                role.UserId = entity.Id;
                role.RoleId = Convert.ToInt32(UserRoleTypesEnum.Standart);
                role.CreateUserId = entity.Id;
                role.CreateDate = DateTime.Now;
                role.IsActive = true;
                context.UserRoles.Add(role);
                await context.SaveChangesAsync();
            }
        }
    }
}
