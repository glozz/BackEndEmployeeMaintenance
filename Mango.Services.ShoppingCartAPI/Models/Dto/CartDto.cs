namespace Mango.Services.ShoppingCartAPI.Models.Dto
{
    public class CartDto
    {
        public CartHearderDto CartHearder { get; set; }
        public IEnumerable<CartDetailsDto> CartDetails { get; set; }
    }
}
