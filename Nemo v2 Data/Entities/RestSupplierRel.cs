﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class RestSupplierRel
    {
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }
        [Required,ForeignKey(nameof(Supplier))]
        public long SupplierId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}