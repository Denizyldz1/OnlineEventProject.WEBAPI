using System.Text.Json.Serialization;

namespace OnlineEvent.Model
{
    public class CustomResponseModel<T>
    {
        public T? Data { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<String>? Errors { get; set; }

        // İşlem başarılı ve geriye data döndürmek için
        public static CustomResponseModel<T> Success(int statusCode, T data)
        {
            return new CustomResponseModel<T> { Data = data, StatusCode = statusCode };
        }
        // İşlem başarılı ve geriye sadece durum kodu döndürmek için
        public static CustomResponseModel<T> Success(int statusCode)
        {
            return new CustomResponseModel<T> { StatusCode = statusCode };
        }
        // Birde fazla error mesaj için
        public static CustomResponseModel<T> Failure(int statusCode, List<String> errors)
        {
            return new CustomResponseModel<T> { StatusCode = statusCode, Errors = errors };
        }
        public static CustomResponseModel<T> Failure(int statusCode, string errors)
        {
            return new CustomResponseModel<T> { StatusCode = statusCode, Errors = new List<string> { errors } };
        }

    }
}
