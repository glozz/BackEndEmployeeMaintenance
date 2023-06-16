using Mango.Web.Services.IServices;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Mango.Web.Services
{
    public class CouponService : BaseService, ICouponService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CouponService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<T> GetCoupon<T>(string couponCode,string token = null)
        {
           return await this.SendAsync<T>(new Models.ApiRequest()
           {
               ApiType = SD.ApiTpe.GET,
               Url = SD.CouponAPIBase + "/api/Coupon" + couponCode,
               AccessToken = token
           }
        }
    }
}
