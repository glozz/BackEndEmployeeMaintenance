using static Mango.Web.SD;

namespace Mango.Web.Models
{
    public class ApiRequest
    {
        public ApiTpe ApiType { get; set; } = ApiTpe.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public object AccessToken { get; set; }
    }
}
