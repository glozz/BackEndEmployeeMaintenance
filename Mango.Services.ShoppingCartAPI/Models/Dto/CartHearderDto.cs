using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ShoppingCartAPI.Models.Dto
{
    public class CartHearderDto
    {
        public int CartHearderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
    }
}
