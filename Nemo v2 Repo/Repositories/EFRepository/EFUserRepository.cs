using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.DbContexts;
using Nemo_v2_Repo.Helper;

namespace Nemo_v2_Repo.Repositories.EFRepository
{
    public class EFUserRepository : EFRepository<User>
    {
        public EFUserRepository(ApplicationContext context) : base(context)
        {
        }

        public override User Update(User entity, string[] notUpdateProperties)
        {
            try
            {
                var model = (context as ApplicationContext).Users
                    .Include(x => x.UserRoles)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == entity.Id);
                
                context.Entry(entity).State = EntityState.Modified;
                context.TryUpdateManyToMany(model.UserRoles, entity.UserRoles, x => x.RoleId);
                return base.Update(entity, notUpdateProperties);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw;
            }
        }
    }
}