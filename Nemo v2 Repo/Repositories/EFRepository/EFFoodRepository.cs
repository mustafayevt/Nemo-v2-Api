﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.DbContexts;
using Nemo_v2_Repo.Helper;

namespace Nemo_v2_Repo.Repositories.EFRepository
{
    public class EFFoodRepository:EFRepository<Food>
    {
        public EFFoodRepository(ApplicationContext context) : base(context)
        {
        }

        public override Food Update(Food entity, string[] notUpdateProperties)
        {
            try
            {
                if (entity.Ingredients == null) entity.Ingredients = new List<IngredientFoodRel>();

                var model = (context as ApplicationContext).Foods
                    .AsNoTracking()
                    .Include(x => x.Ingredients)
                    .FirstOrDefault(x => x.Id == entity.Id);
                context.TryUpdateManyToMany(model.Ingredients, entity.Ingredients, x => x.IngredientId);if (entity.Ingredients == null) entity.Ingredients = new List<IngredientFoodRel>();
                
                model = (context as ApplicationContext).Foods
                    .AsNoTracking()
                    .Include(x => x.FoodGroups)
                    .FirstOrDefault(x => x.Id == entity.Id);
                context.TryUpdateManyToMany(model.FoodGroups, entity.FoodGroups, x => x.FoodGroupId);


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