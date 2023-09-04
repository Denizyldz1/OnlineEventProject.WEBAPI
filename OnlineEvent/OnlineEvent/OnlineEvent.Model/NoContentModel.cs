using System.Text.Json.Serialization;

namespace OnlineEvent.Model
{
    public class NoContentModel
    {
        //CustomResponseDto'da T Data dönerken null dönmek istemez isek bunu oluşturabiliriz. İsteğe bağlı
        [JsonIgnore] // Bu veriyi json dönüştürürken Ignore(Yok say) et demek
        public int StatusCode { get; set; }
        public List<String>? Errors { get; set; }
        public static NoContentModel Success(int statusCode)
        {
            return new NoContentModel { StatusCode = statusCode };
        }
        public static NoContentModel Failure(int statusCode, List<String> errors)
        {
            return new NoContentModel { StatusCode = statusCode, Errors = errors };
        }
        public static NoContentModel Failure(int statusCode, string errors)
        {
            return new NoContentModel { StatusCode = statusCode, Errors = new List<string> { errors } };
        }
    }
}
