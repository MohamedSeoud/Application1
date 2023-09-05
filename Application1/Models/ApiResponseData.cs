using System.Net;

namespace Application1.Models
{
    public class ApiResponseData<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
