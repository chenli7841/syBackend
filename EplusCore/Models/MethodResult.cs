namespace WebUI.Models
{
    public sealed class MethodResult<T>
    {
        public Error Error { get; set; }
        public T Data { get; set; }

        public MethodResult(T data)
        {
            Data = data;
        }

        public MethodResult(Error error)
        {
            Error = error;
        }
	}
}
