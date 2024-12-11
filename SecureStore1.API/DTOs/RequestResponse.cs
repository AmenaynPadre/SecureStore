namespace SecureStore1.API.DTOs
{
    public class RequestResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpContext Context { get; set; }

        public void get()
        {
            Context.Response.StatusCode = 200;
        }
    }
}
