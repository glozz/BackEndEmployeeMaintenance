using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ShoppingCartAPI.Models
{
    public class CartHearder
    {
        [Key]
        public int CartHearderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
    }
}
