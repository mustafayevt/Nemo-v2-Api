using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.DbContexts;
using Nemo_v2_Repo.Helper;

namespace Nemo_v2_Repo.Repositories.EFRepository
{
    public class EFIngredientCategoryRepository:EFRepository<IngredientCategory>
    {
        public EFIngredientCategoryRepository(ApplicationContext context) : base(context)
        {
        }

        // public override IngredientCategory Update(IngredientCategory entity, string[] notUpdateProperties)
        // {
        //     try
        //     {
        //         if (entity.IngredientCategoryRels == null) entity.IngredientCategoryRels = new List<IngredientCategoryRel>();
        //
        //         var model = (context as ApplicationContext).IngredientCategories
        //             .AsNoTracking()
        //             .Include(x => x.IngredientCategoryRels)
        //             .FirstOrDefault(x => x.Id == entity.Id);
        //         context.TryUpdateManyToMany(model.IngredientCategoryRels, entity.IngredientCategoryRels, x => x.IngredientId);
        //
        //         return base.Update(entity, notUpdateProperties);
        //     }
        //     catch (Exception e)
        //     {
        //         if (e.InnerException != null) throw e.InnerException;
        //         throw;
        //     }
        // }
    }
}