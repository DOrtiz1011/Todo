namespace Todo.Api.DTO
{
    public abstract class BaseDTO
    {
        public          int    id                 { get; set; }
        public required string createdatetime     { get; set; }
        public required string lastupdatedatetime { get; set; }
    }
}
