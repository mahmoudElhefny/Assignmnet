namespace Assignment.Domain.ViewModels
{
    public class Response<T>
    {
        public Response()
        {
            IsSucceded = true;
        }

        public T Data { get; set; }
        public bool IsSucceded { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public string ErrorMessage { get; set; }
    }

    public class Empty { }
}
