using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class ProductService : BaseService, IProductServices
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto, string token)
        {
           return await this.SendAsync<T>(new ApiRequest() 
            {
              ApiType = SD.ApiTpe.POST,
              Data = productDto,
              Url = SD.ProductAPIBase + "/api/products",
              AccessToken = token
           });
        }

        public async Task<T> DeleteProductAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiTpe.DELETE,
                Url = SD.ProductAPIBase + "/api/products/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllProductAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiTpe.GET,
                Url = SD.ProductAPIBase + "/api/products",
                AccessToken = token
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiTpe.GET,
                Url = SD.ProductAPIBase + "/api/products/" + id,
                AccessToken = token
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiTpe.PUT,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/products",
                AccessToken = token
            });
        }
    }
}
