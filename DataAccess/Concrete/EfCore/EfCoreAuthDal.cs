using Core.Utilities.Security.Identity;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.DTOs;
using Entities.Entity;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreAuthDal : IAuthDal
    {
        private readonly IConfiguration _configuration;

        private readonly ITokenService _tokenService;

        private UserManager<ApplicationUser> _userManager;

        private RoleManager<ApplicationRole> _roleManager;

        public EfCoreAuthDal(IConfiguration configuration, ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Token> LoginAsync(LoginDto model)
        {
            using (var context = new ECommerceDbContext())
            {
                Token token = new Token();
                ApplicationUser getUser = await _userManager.FindByEmailAsync(model.EmailAddress.Trim());
                if (getUser != null && await _userManager.CheckPasswordAsync(getUser, model.Password.Trim()))
                {
                    TokenService tokenService = new TokenService(_configuration);
                    var getRole = await _userManager.GetRolesAsync(getUser);

                    List<string> roles = new List<string>();

                    if (getRole.Count > 0)
                    {
                        foreach (var item in getRole)
                        {
                            roles.Add(item);
                        }
                    }
                    token = _tokenService.CreateAccessToken(getUser, roles);
                    return token;

                }
                return token;
            }

        }

        public async Task<string> RegisterAsync(RegisterDto model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.EmailAddress.Trim(),
                Email = model.EmailAddress.Trim(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsActive = true
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password.Trim());
            if (result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync("Standart").Result)
                {
                    ApplicationRole role = new ApplicationRole()
                    {
                        Name = "Standart"
                    };
                    IdentityResult roleResult = await _roleManager.CreateAsync(role);
                    if (roleResult.Succeeded)
                    {
                        _userManager.AddToRoleAsync(user, "Standart").Wait();
                    }
                }
                _userManager.AddToRoleAsync(user, "Standart").Wait();
                return "success";
            }
            else
            {
                String errorMessage = String.Empty;
                foreach (var item in result.Errors)
                {
                    errorMessage += item.Description;
                }
                return errorMessage;
            }

        }

    }
}
