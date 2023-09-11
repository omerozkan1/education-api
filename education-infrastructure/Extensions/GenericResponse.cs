using System.Net;
using System.Text.Json.Serialization;

namespace education_infrastructure.Extensions
{
    public class GenericResponse<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        [JsonIgnore]
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; }

        // Static Factory Method
        public static GenericResponse<T> Success(T data, HttpStatusCode statusCode)
        {
            return new GenericResponse<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static GenericResponse<T> Success(HttpStatusCode statusCode)
        {
            return new GenericResponse<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = true };
        }

        public static GenericResponse<T> Fail(List<string> errors, HttpStatusCode statusCode)
        {
            return new GenericResponse<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }

        public static GenericResponse<T> Fail(string error, HttpStatusCode statusCode)
        {
            return new GenericResponse<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
