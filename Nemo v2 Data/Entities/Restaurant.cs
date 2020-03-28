using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class Restaurant:BaseEntity
    {
        [Required,MaxLength(Int32.MaxValue)]
        public string Name { get; set; }
        [ForeignKey(nameof(Branch))]
        public long? BranchId { get; set; }
        public virtual Restaurant Branch { get; set; }
        public List<RestWareRel> WareHouses { get; set; }

        public virtual List<Food> Foods { get; set; }
        public virtual List<FoodGroup> FoodGroups { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }
        public virtual List<IngredientCategory> IngredientCategories { get; set; }
        public virtual List<Restaurant> Branches { get; set; }
        public virtual List<Role> Roles { get; set; }
        public virtual List<Section> Sections { get; set; }
        public virtual List<Supplier> Suppliers { get; set; }
        public virtual List<Table> Tables { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<WarehouseInvoice> WarehouseInvoices { get; set; }
    }
}