using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class TableBase
    {
        [Key]
        public int      Id                 { get; set; }
        
        public DateTime CreateDateTime     { get; set; }
        
        public DateTime LastUpdateDateTime { get; set; }
    }
}
