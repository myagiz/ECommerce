using Core.Utilities.Results.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IResult> CreateUserAsync(CreateUserDto model);
        Task<IResult> UpdateUserAsync(UpdateUserDto model);
        Task<IResult> DeleteUserAsync(int userId);
        Task<IDataResult<List<GetAllUserDto>>> GetAllUsersAsync();
        Task<IDataResult<GetAllUserDto>> GetUserByIdAsync(int userId);
    }
}
