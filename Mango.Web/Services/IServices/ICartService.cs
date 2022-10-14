using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsnyc<T>(string userId, string token = null);
        Task<T> AddToCartAsnyc<T>(CartDto cartDto, string token = null);
        Task<T> UpdateCartAsnyc<T>(CartDto cartDto, string token = null);
        Task<T> RemoveCartAsnyc<T>(int cartId, string token = null);
    }
}
