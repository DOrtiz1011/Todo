namespace Todo.APi.DTO
{
    public class TodoTaskRequestDTO
    {
        public int             id          { get; set; }
        public string          title       { get; set; } = string.Empty;
        public string?         description { get; set; } = string.Empty;
        public DateTime?       duedatetime { get; set; }
        public required string status      { get; set; }
        public required string priority    { get; set; }
    }
}
