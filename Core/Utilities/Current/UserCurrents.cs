using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Current
{
    public class UserCurrents
    {
        public static HttpContext Current => new HttpContextAccessor().HttpContext;

        public static bool IsAuthenticated()
        {
            return Current.User.Identity.IsAuthenticated;
        }
        public static string UserName()
        {
            return Current.User.Identity.Name;
        }
        public static string Email()
        {
            return Current.User.Identity.GetUserEmail();
        }
        public static string NameSurname()
        {
            return Current.User.Identity.GetUserNameIdentifier();
        }
      
        public static int UserId()
        {
            return Current.User.Identity.GetUserId();
        }
        //public static Guid CompanyId()
        //{
        //    return Current.User.Identity.GetCompanyId();
        //}

        public static List<string> GetRoles()
        {
            return Current.User.ClaimRoles();
        }

        public static string FirstNameLastName()
        {
            return Current.User.Identity.Name;
        }

    }
}
