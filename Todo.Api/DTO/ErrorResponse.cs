namespace Todo.Api.DTO
{
    public class ErrorResponse
    {
        public          int     StatusCode { get; set; }
        public required string  Message    { get; set; }
        public          string? Details    { get; set; }
    }
}
