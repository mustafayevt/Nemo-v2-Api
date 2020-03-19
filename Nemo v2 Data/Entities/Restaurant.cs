using System;
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
    }
}