namespace Mango.Services.ShoppingCartAPI.Models
{
    public class Cart
    {
        public CartHearder CartHearder { get; set; }
        public IEnumerable<CartDetails> CartDetails { get; set; }
    }
}
