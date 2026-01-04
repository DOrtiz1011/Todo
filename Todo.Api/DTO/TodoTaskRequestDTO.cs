namespace Todo.Api.DTO
{
    public class TodoTaskRequestDTO : BaseDTO
    {
        public string          title       { get; set; } = string.Empty;
        public string?         description { get; set; } = string.Empty;
        public DateTime?       duedatetime { get; set; }
        public required string status      { get; set; }
        public required string priority    { get; set; }
    }
}
