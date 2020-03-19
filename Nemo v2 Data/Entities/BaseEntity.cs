using System;
using System.ComponentModel.DataAnnotations;

namespace Nemo_v2_Data.Entities
{
    public class BaseEntity  
    {  
        [Required]
        public long Id { get; set; }  
        public DateTime AddedDate { get; set; }  
        public DateTime ModifiedDate { get; set; }
    }  
}