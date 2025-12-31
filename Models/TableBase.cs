using System.ComponentModel.DataAnnotations;

namespace Todo.APi.Models
{
    /// <summary>
    /// Defines the minimum required fields for a database table. All tables must inherit from this class
    /// </summary>
    public class TableBase
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public int Id { get; set; }        

        /// <summary>
        /// Date the record was created in UTC
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        
        /// <summary>
        /// Date the record was last created in UTC
        /// </summary>
        public DateTime LastUpdateDateTime { get; set; }
    }
}
