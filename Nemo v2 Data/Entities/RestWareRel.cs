﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nemo_v2_Data.Entities
{
    public class RestWareRel
    {
        [Required,ForeignKey(nameof(Restaurant))]
        public long RestaurantId { get; set; }
        [Required,ForeignKey(nameof(Warehouse))]
        public long WarehouseId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}