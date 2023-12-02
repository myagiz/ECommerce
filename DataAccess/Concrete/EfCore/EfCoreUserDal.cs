using Core.Repository.Concrete;
using Core.Utilities.Current;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.DTOs;
using Entities.Entity;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreUserDal : EfEntityRepository<User, ECommerceDbContext>, IUserDal
    {
        public async Task CreateUserAsync(CreateUserDto model)
        {
            using (var context = new ECommerceDbContext())
            {
                User entity = new User();
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.EmailAddress = model.EmailAddress;
                entity.Password = "123456";
                entity.IsTwoFactor = false;
                entity.IsConfirm = true;
                entity.IsActive = true;
                context.Users.Add(entity);
                await context.SaveChangesAsync();

                UserRole role = new UserRole();
                role.UserId = entity.Id;
                role.RoleId = model.RoleId;
                role.CreateUserId = 1; //Refactor
                role.CreateDate = DateTime.Now;
                role.IsActive = true;
                context.UserRoles.Add(role);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            using (var context = new ECommerceDbContext())
            {

                var getData = await context.Users.FirstOrDefaultAsync(x => x.IsActive == true && x.Id == userId);
                if (getData != null) { getData.IsActive = false; await context.SaveChangesAsync(); }
            }
        }

        public async Task<List<GetAllUserDto>> GetAllUsersAsync()
        {
            using (var context = new ECommerceDbContext())
            {
                var data = await (from a in context.Users.Where(x => x.IsActive == true)
                                  join b in context.UserRoles.Where(x => x.IsActive == true) on a.Id equals b.UserId into leftJoinUserRoles
                                  from b in leftJoinUserRoles.DefaultIfEmpty()
                                  select new GetAllUserDto
                                  {
                                      Id = a.Id,
                                      EmailAddress = a.EmailAddress,
                                      FirstName = a.FirstName,
                                      LastName = a.LastName,
                                      RoleId = b.RoleId,
                                      RoleName = b.RoleId == Convert.ToInt32(UserRoleTypesEnum.Admin) ? "Admin" : b.RoleId == Convert.ToInt32(UserRoleTypesEnum.Standart) ? "Standart" : "NoRole",
                                  }

                                ).OrderBy(x => x.FirstName).ToListAsync();
                return data;
            }
        }

        public async Task<GetAllUserDto> GetUserByIdAsync(int userId)
        {
            using (var context = new ECommerceDbContext())
            {
                var data = await (from a in context.Users.Where(x => x.IsActive == true && x.Id == userId)
                                  join b in context.UserRoles.Where(x => x.IsActive == true) on a.Id equals b.UserId into leftJoinUserRoles
                                  from b in leftJoinUserRoles.DefaultIfEmpty()
                                  select new GetAllUserDto
                                  {
                                      Id = a.Id,
                                      EmailAddress = a.EmailAddress,
                                      FirstName = a.FirstName,
                                      LastName = a.LastName,
                                      RoleId = b.RoleId,
                                      RoleName = b.RoleId == Convert.ToInt32(UserRoleTypesEnum.Admin) ? "Admin" : b.RoleId == Convert.ToInt32(UserRoleTypesEnum.Standart) ? "Standart" : "NoRole",
                                  }

                                ).OrderBy(x => x.FirstName).FirstOrDefaultAsync();
                return data;
            }
        }

        public async Task UpdateUserAsync(UpdateUserDto model)
        {
            using (var context = new ECommerceDbContext())
            {

                var getData = await context.Users.FirstOrDefaultAsync(x => x.IsActive == true && x.Id == model.Id);
                if (getData != null)
                {
                    getData.FirstName = model.FirstName;
                    getData.LastName = model.LastName;
                    getData.EmailAddress = model.EmailAddress;
                    await context.SaveChangesAsync();

                }
                var getRole = await context.UserRoles.Where(x => x.IsActive == true && x.UserId == model.Id).FirstOrDefaultAsync();
                if (getRole != null)
                {
                    getRole.RoleId = model.RoleId;
                    getRole.UpdateUserId = UserCurrents.UserId();
                    getRole.UpdateDate = DateTime.Now;
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
