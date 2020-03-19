﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data.Entities
{
    public class Warehouse:BaseEntity
    {
        [Required,MaxLength(Int32.MaxValue)]
        public string Name { get; set; }
    }
}