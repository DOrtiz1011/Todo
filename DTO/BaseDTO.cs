namespace Todo.DTO
{
    public abstract class BaseDTO
    {
        public int      id                 { get; set; }
        public DateTime createdatetime     { get; set; }
        public DateTime lastupdatedatetime { get; set; }
    }
}
