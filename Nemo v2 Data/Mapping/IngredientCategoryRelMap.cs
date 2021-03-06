﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Mapping
{
    public class IngredientCategoryRelMap
    {
        public IngredientCategoryRelMap(EntityTypeBuilder<IngredientCategoryRel> entityBuilder)  
        {  
            entityBuilder.HasKey(t => new {t.IngredientId, t.IngredientCategoryId});
            entityBuilder
                .HasOne(pt => pt.Ingredient)
                .WithMany(p => p.IngredientCategories)
                .HasForeignKey(pt => pt.IngredientId);
            
            // entityBuilder
            //     .HasOne(pt => pt.IngredientCategory)
            //     .WithMany(p => p.IngredientCategoryRels)
            //     .HasForeignKey(pt => pt.IngredientCategoryId);
        } 
    }
}