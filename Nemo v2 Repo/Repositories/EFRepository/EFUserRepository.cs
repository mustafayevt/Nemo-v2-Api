using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Repo.DbContexts;
using Nemo_v2_Repo.Helper;

namespace Nemo_v2_Repo.Repositories.EFRepository
{
    public class EFUserRepository : EFRepository<User>,IUserRepository
    {
        public EFUserRepository(ApplicationContext context) : base(context)
        {
        }

        public override User Update(User entity)
        {
            try
            {
                if (entity.UserRoles == null) entity.UserRoles = new List<UserRole>();

                var model = (context as ApplicationContext).Users
                    .AsNoTracking()
                    .Include(x => x.UserRoles)
                    .FirstOrDefault(x => x.Id == entity.Id);
                context.TryUpdateManyToMany(model.UserRoles, entity.UserRoles, x => x.RoleId);


                return base.Update(entity);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw;
            }
        }
    }
}