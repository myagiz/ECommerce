using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class Messages
    {
        public static string Added = "Added Successfully !";
        public static string Deleted = "Deleted Successfully !";
        public static string Updated = "Updated Successfully !";
        public static string Error = "Error !";
        public static string Listed = "Listed Successfully !";
        public static string Succeed = "Success !";
        public static string GetById = "Get by Id !";

        public static string UserNotFound = "User not found !";
        public static string NotFoundData = "Data not found !";
        public static string PasswordError = "Password Error";
        public static string SuccessfulLogin = "Login successfully!";
        public static string UserAlreadyExists = "User already exists !";
        public static string UserRegistered = "User created successfully !";
        public static string AccessTokenCreated = "Access token created successfully !";
        public static string AuthorizationDenied = "You don't have authorization !";
        public static string[] ValidImageFileTypes = { ".JPG", ".JPEG", ".PNG", ".TIF", ".TIFF", ".GIF", ".BMP", ".ICO" };
    }
}
