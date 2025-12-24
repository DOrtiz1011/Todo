using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class TableBase
    {
        [Key]
        public int      ID                 { get; set; }
        
        public DateTime CreateDateTime     { get; set; }
        
        public DateTime LastUpdateDateTime { get; set; }
    }
}
