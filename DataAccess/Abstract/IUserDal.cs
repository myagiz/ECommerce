﻿using Core.Repository.Abstract;
using Entities.DTOs;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task CreateUserAsync(CreateUserDto model);
        Task UpdateUserAsync(UpdateUserDto model);
        Task DeleteUserAsync(int userId);
        Task<List<GetAllUserDto>> GetAllUsersAsync();
        Task<GetAllUserDto> GetUserByIdAsync(int userId);
    }
}
