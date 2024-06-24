namespace EntityLayer.Responses
{
    public class Response
    {
        public ResponseType Code { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
