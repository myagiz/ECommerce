using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.DTOs;
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

        public AuthManager(IAuthDal authDal, IUserDal userDal)
        {
            _authDal = authDal;
            _userDal = userDal;
        }

        public async Task<IDataResult<Token>> LoginAsync(LoginDto model)
        {
            IResult result = BusinessRules.Run(CheckCorrectEmailAndPassword(model.EmailAddress, model.Password));
            if (result == null)
            {
                var getToken = await _authDal.LoginAsync(model);
                return new SuccessDataResult<Token>(getToken, Messages.UserRegistered);
            }

            return new ErrorDataResult<Token>(Messages.UserNotFound);
        }

        public async Task<IResult> RegisterAsync(RegisterDto model)
        {
            IResult result = BusinessRules.Run(CheckIfUserEmailExists(model.EmailAddress));
            if (result == null)
            {
                await _authDal.RegisterAsync(model);
                return new SuccessResult(Messages.UserRegistered);
            }

            return result;

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

        private IResult CheckCorrectEmailAndPassword(string email, string password)
        {
            var result = _userDal.Get(x => x.IsActive == true && x.EmailAddress == email && x.Password == password);
            if (result == null)
            {
                return new ErrorResult(Messages.PasswordError);
            }
            return new SuccessResult();
        }
    }
}
