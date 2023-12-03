using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using Core.Utilities.Security.Identity;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IAuthDal _authDal;

        private readonly IUserDal _userDal;

        private UserManager<ApplicationUser> _userManager;


        public AuthManager(IAuthDal authDal, IUserDal userDal, UserManager<ApplicationUser> userManager)
        {
            _authDal = authDal;
            _userDal = userDal;
            _userManager = userManager;
        }

        public async Task<IDataResult<Token>> LoginAsync(LoginDto model)
        {
            //    IResult result = BusinessRules.RunAsync(CheckCorrectEmailAndPassword(model.EmailAddress, model.Password));
            //    if (result == null)
            //    {
            var getToken = await _authDal.LoginAsync(model);
            return new SuccessDataResult<Token>(getToken, Messages.SuccessfulLogin);
            //}

            //return new ErrorDataResult<Token>(Messages.UserNotFound);
        }

        public async Task<IResult> RegisterAsync(RegisterDto model)
        {
            //IResult result = BusinessRules.Run(CheckIfUserEmailExists(model.EmailAddress));
            //if (result == null)
            //{

            var result = await _authDal.RegisterAsync(model);
            if (result == "success")
            {
                return new SuccessResult(Messages.UserRegistered);

            }
            return new ErrorResult(result.ToString());

            //}

            //return result;

        }

        private IResult CheckIfUserEmailExists(string email)
        {
            var result = _userDal.GetAll(x => x.EmailAddress == email).Any();
            if (result)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        private async Task<IResult> CheckCorrectEmailAndPassword(string email, string password)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email.Trim());
            if (user != null && await _userManager.CheckPasswordAsync(user, password.Trim()))
            {
                return new SuccessResult();

            }
            return new ErrorResult(Messages.PasswordError);

        }
    }
}
