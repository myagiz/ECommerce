using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Current
{
    public static class ClaimsExtensions
    {
        static string GetUserEmail(this ClaimsIdentity identity)
        {
            return identity.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        }

        public static string GetUserEmail(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            return claimsIdentity != null ? GetUserEmail(claimsIdentity) : "";
        }

        static string GetUserNameIdentifier(this ClaimsIdentity identity)
        {
            return identity.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetUserNameIdentifier(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            return claimsIdentity != null ? GetUserNameIdentifier(claimsIdentity) : "";
        }

        static int GetUserId(this ClaimsIdentity identity)
        {
            if (identity.IsAuthenticated)
                return int.Parse(identity.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value);
            else
                return 0;
        }
        public static int GetUserId(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            return claimsIdentity != null ? GetUserId(claimsIdentity) : 0;
        }

        //static Guid GetCompanyId(this ClaimsIdentity identity)
        //{
        //    if (identity.IsAuthenticated)
        //        return Guid.Parse(identity.Claims?.FirstOrDefault(c => c.Type == "CompanyId")?.Value);
        //    else
        //        return Guid.Empty;
        //}
        //public static Guid GetCompanyId(this IIdentity identity)
        //{
        //    var claimsIdentity = identity as ClaimsIdentity;
        //    return claimsIdentity != null ? GetCompanyId(claimsIdentity) : Guid.Empty;
        //}


        static List<Claim> GetAll(this ClaimsIdentity identity)
        {
            return identity.Claims.ToList();

        }

        public static List<Claim> GetAll(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            return claimsIdentity != null ? GetAll(claimsIdentity) : null;
        }

        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }

}
