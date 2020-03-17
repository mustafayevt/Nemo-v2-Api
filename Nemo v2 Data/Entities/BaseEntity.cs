using System;

namespace Nemo_v2_Data.Entities
{
    public class BaseEntity  
    {  
        public long Id { get; set; }  
        public DateTime AddedDate { get; set; }  
        public DateTime ModifiedDate { get; set; }
    }  
}