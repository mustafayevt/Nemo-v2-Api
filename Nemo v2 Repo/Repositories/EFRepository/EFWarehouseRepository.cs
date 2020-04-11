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
    public class EFWarehouseRepository : EFRepository<Warehouse>,IWarehouseRepository
    {
        public EFWarehouseRepository(ApplicationContext context) : base(context)
        {
        }

        public override Warehouse Update(Warehouse entity)
        {
            try
            {
                if (entity.RestWareRels == null) entity.RestWareRels = new List<RestWareRel>();

                var model = (context as ApplicationContext).Warehouses
                    .AsNoTracking()
                    .Include(x => x.RestWareRels)
                    .FirstOrDefault(x => x.Id == entity.Id);
                context.TryUpdateManyToMany(model.RestWareRels, entity.RestWareRels, x => x.RestaurantId);


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