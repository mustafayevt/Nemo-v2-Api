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
    public class EFIngredientRepository:EFRepository<Ingredient>,IIngredientRepository
    {
        public EFIngredientRepository(ApplicationContext context) : base(context)
        {
        }

        public override Ingredient Update(Ingredient entity)
        {
            try
            {
                if (entity.IngredientCategories == null) entity.IngredientCategories = new List<IngredientCategoryRel>();

                var model = (context as ApplicationContext).Ingredients
                    .AsNoTracking()
                    .Include(x => x.IngredientCategories)
                    .FirstOrDefault(x => x.Id == entity.Id);
                context.TryUpdateManyToMany(model.IngredientCategories, entity.IngredientCategories, x => x.IngredientCategoryId);


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