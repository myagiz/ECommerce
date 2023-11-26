using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.DTOs;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<IResult> CreateUserAsync(CreateUserDto model)
        {
            IResult result = BusinessRules.Run(CheckIfUserEmailExists(model.EmailAddress));
            if (result == null)
            {
                await _userDal.CreateUserAsync(model);
                return new SuccessResult(Messages.UserRegistered);
            }
            return result;
        }

        public async Task<IResult> DeleteUserAsync(int userId)
        {
            var control = _userDal.Get(x => x.IsActive == true && x.Id == userId);
            if (control != null)
            {
                await _userDal.DeleteUserAsync(userId);
                return new SuccessResult(Messages.Deleted);
            }
            return new ErrorResult(Messages.NotFoundData);
        }

        public async Task<IDataResult<List<GetAllUserDto>>> GetAllUsersAsync()
        {
            var result = await _userDal.GetAllUsersAsync();
            return new SuccessDataResult<List<GetAllUserDto>>(result, Messages.Listed);
        }

        public async Task<IDataResult<GetAllUserDto>> GetUserByIdAsync(int userId)
        {
            var result = await _userDal.GetUserByIdAsync(userId);
            if (result != null)
            {
                return new SuccessDataResult<GetAllUserDto>(result, Messages.Succeed);
            }
            return new ErrorDataResult<GetAllUserDto>(Messages.NotFoundData);
        }

        public async Task<IResult> UpdateUserAsync(UpdateUserDto model)
        {
            var control = _userDal.Get(x => x.IsActive == true && x.Id == model.Id);
            if (control != null)
            {
                await _userDal.UpdateUserAsync(model);
                return new SuccessResult(Messages.Updated);
            }
            return new ErrorResult(Messages.NotFoundData);
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

    }
}
