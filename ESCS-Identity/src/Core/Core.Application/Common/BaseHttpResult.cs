namespace Core.Application.Common
{
    public class BaseHttpResult
    {
        public bool Succeeded { get; set; }
        public int StatusCode { get; set; } = default!;
    }


    public class BaseHttpResult<T> : BaseHttpResult
    {
        public T Data { get; set; } = default!;

    }
}
