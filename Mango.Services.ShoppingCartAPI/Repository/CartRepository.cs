using AutoMapper;
using Mango.Services.ShoppingCartAPI.DbContexts;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> ClearCart(string UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            Cart cart = _mapper.Map<Cart>(cartDto);

            //check if product exist in the database, if not create
            var prodInDb = _db.Products
                .FirstOrDefault(p => p.ProductId == cartDto.CartDetails.FirstOrDefault()
                .ProductId);

            if (prodInDb == null)
            {
                _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                
                SaveChanges();
            }

            //check if header is null 
            var cartHearderFromDb = await _db.CartHearders.AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == cart.CartHearder.UserId);

            if (cartHearderFromDb == null)
            {
                //create header and details
                _db.CartHearders.Add(cart.CartHearder);
                SaveChanges();

                cart.CartDetails.FirstOrDefault().CartHearderId = cart.CartHearder.CartHearderId;

                cart.CartDetails.FirstOrDefault().Product = null;
                _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());

                SaveChanges();
            }
            else
            {
                //if header is not null 
                // check if details has the same product
                var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    c => c.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    c.CartHearderId == cartHearderFromDb.CartHearderId);

                if (cartDetailsFromDb == null)
                {
                    //creatre details
                    cart.CartDetails.FirstOrDefault().CartHearderId = cartHearderFromDb.CartHearderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    SaveChanges();
                }
                else
                {
                    //Update the count / cart details
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                    _db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    SaveChanges();
                }
            }
            return _mapper.Map<CartDto>(cart);
         }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(int cartDetailsDto)
        {
            throw new NotImplementedException();
        }

        private async void SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
