namespace Todo.Api.DTO
{
    public class TodoTaskResponseDTO : BaseDTO
    {
        public string  title       { get; set; } = string.Empty;
        public string  description { get; set; } = string.Empty;
        public string? duedatetime { get; set; } = string.Empty;
        public string  status      { get; set; } = string.Empty; // Changed to string
        public string  priority    { get; set; } = string.Empty; // Changed to string
    }
}
