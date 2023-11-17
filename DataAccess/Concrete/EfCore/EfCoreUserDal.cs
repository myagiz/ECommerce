using Core.Repository.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using Entities.Entity;
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
                entity.Password = model.Password;
                entity.IsTwoFactor = false;
                entity.IsConfirm = true;
                entity.IsActive = true;
                context.Users.Add(entity);
                await context.SaveChangesAsync();

                UserRole role=new UserRole();
                role.UserId = entity.Id;
                role.RoleId = model.RoleId;
                role.CreateUserId = 1; //Refactor
                role.CreateDate = DateTime.Now;
                role.IsActive = true;
                context.UserRoles.Add(role);
                await context.SaveChangesAsync();
            }
        }
    }
}
