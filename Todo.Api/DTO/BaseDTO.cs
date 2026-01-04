namespace Todo.Api.DTO
{
    public abstract class BaseDTO
    {
        public          int      id                 { get; set; }
        public required DateTime createdatetime     { get; set; }
        public required DateTime lastupdatedatetime { get; set; }
    }
}
