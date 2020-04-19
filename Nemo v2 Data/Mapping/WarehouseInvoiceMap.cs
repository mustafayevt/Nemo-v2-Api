using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nemo_v2_Data.Entities;

namespace Nemo_v2_Data.Mapping
{
    public class WarehouseInvoiceMap
    {
        public WarehouseInvoiceMap(EntityTypeBuilder<WarehouseInvoice> entityBuilder)
        {
            entityBuilder.HasIndex(x => new {x.ComputedNumber, x.RestaurantId}).IsUnique();
             // entityBuilder.Property(x => x.ComputedNumber).ValueGeneratedOnAdd();
        }
    }
}