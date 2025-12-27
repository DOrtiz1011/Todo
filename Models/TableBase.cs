using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    /// <summary>
    /// Defines the minimum required fields for a database table. All tables must inherit from this class
    /// </summary>
    public class TableBase
    {
        [Key]
        public int      Id                 { get; set; }
        
        public DateTime CreateDateTime     { get; set; }
        
        public DateTime LastUpdateDateTime { get; set; }
    }
}
