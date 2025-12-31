namespace Todo.APi.DTO
{
    public abstract class BaseDTO
    {
        public int    id                 { get; set; }
        public string createdatetime     { get; set; }
        public string lastupdatedatetime { get; set; }
    }
}
