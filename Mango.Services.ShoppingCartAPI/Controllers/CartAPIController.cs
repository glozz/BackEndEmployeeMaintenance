using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartAPIController : Controller
    {
        private readonly ICartRepository _cartRepository;
        protected readonly ResponseDto _response;

        public CartAPIController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            this._response = new ResponseDto();
        }

        [Authorize]
        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                CartDto cartDto = await _cartRepository.GetCartByUserId(userId);
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [Authorize]
        [IgnoreAntiforgeryToken]
        [HttpPost("AddCart")]
        public async Task<object> AddCart([FromBody]CartDto cartDto)
        {
            try
            {
                CartDto cartD = await _cartRepository.CreateUpdateCart(cartDto);
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        //[Authorize]
        //[HttpPost("UpdateCart")]
        //public async Task<object> UpdateCart(CartDto cartDto)
        //{
        //    try
        //    {
        //        CartDto cartD = await _cartRepository.CreateUpdateCart(cartDto);
        //        _response.Result = cartDto;
        //    }
        //    catch (Exception ex)
        //    {

        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = new List<string> { ex.ToString() };
        //    }
        //    return _response;
        //}

        //[Authorize]
        //[HttpPost("RemoveCart")]
        //public async Task<object> UpdateCart([FromBody]int cartId)
        //{
        //    try
        //    {
        //        bool isSuccess= await _cartRepository.RemoveFromCart(cartId);
        //        _response.Result = isSuccess;
        //    }
        //    catch (Exception ex)
        //    {

        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = new List<string> { ex.ToString() };
        //    }
        //    return _response;
        //}

    }
}
