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

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            var cartFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            cartFromDb.CouponCode = couponCode;
            _db.CartHeaders.Update(cartFromDb);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            if (cartHeaderFromDb != null)
            {
                _db.CartDetails.RemoveRange(_db.CartDetails.Where(u => u.CartHeaderId == cartHeaderFromDb.CartHeaderId));
                _db.CartHeaders.Remove(cartHeaderFromDb);
                SaveChanges();

                return true;
            }
            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {

            Cart cart = _mapper.Map<Cart>(cartDto);

            //check if product exist in the database, if not create
            var prodInDb = _db.Products
                .FirstOrDefault(p => p.ProductId == cartDto.CartDetails.FirstOrDefault()
                .ProductId);

            var cartProductId = cartDto.CartDetails.FirstOrDefault().ProductId;
            
            var prodInDb1 = _db.Products
             .FirstOrDefault(p => p.ProductId == cartProductId);


            if (prodInDb == null)
            {
                _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);

                SaveChanges();
            }

            //check if header is null 
                var cartHeaderFromDb = await _db.CartHeaders
                .FirstOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId);

            if (cartHeaderFromDb == null)
            {
                //create header and details
                _db.CartHeaders.Add(cart.CartHeader);
                SaveChanges();

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;

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
                    c.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                if (cartDetailsFromDb == null)
                {
                    //creatre details
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
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
            Cart cart = new()
            {
                CartHeader = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId)
            };

            cart.CartDetails = _db.CartDetails
                .Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId).Include(u => u.Product);

            return _mapper.Map<CartDto>(cart);
        }

        //public async Task<CartDto> GetCartByUserId(string userId)
        //{
        //    Cart cart = new Cart();
        //    var cartDetails = new List<CartDetails>();


        //    List<CartHeader> cartHeader = _db.CartHeaders.Where(u => u.UserId == userId).ToList();

        //    foreach (var item in cartHeader)
        //    {
        //        var cartDetail = _db.CartDetails
        //      .Where(u => u.CartHeaderId == item.CartHeaderId).Include(u => u.Product);

        //        cart.ca
        //        cartDetails.AddRange(cartDetail);
        //    }


        //    return _mapper.Map<CartDto>(cartDetails.AsEnumerable());
        //}


        //public async Task<CartDto> GetCartByUserId(string userId)
        //{
        //    var cartDetails = new List<CartDetails>();

        //    List<CartHeader> cartHeader = _db.CartHeaders.Where(u => u.UserId == userId).ToList();

        //    foreach (var item in cartHeader)
        //    {
        //        var cartDetail = _db.CartDetails
        //      .Where(u => u.CartHeaderId == item.CartHeaderId).Include(u => u.Product);

        //        cartDetails.AddRange(cartDetail);
        //    }

        //    Cart cart = new Cart
        //    {
        //        CartHeader = cartHeader,
        //        CartDetails = cartDetails
        //    };

        //    return _mapper.Map<CartDto>(cart);
        //}

        public async Task<bool> RemoveCoupon(string userId)
        {
            var cartFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            cartFromDb.CouponCode = "";
            _db.CartHeaders.Update(cartFromDb);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = _db.CartDetails
                    .FirstOrDefault(u => u.CartDetailId == cartDetailsId);

                int totalCountOfCartItems = _db.CartDetails
                    .Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();

                _db.CartDetails.Remove(cartDetails);
                if (totalCountOfCartItems == 1)
                {
                    var cartheaderToRemove = await _db.CartHeaders
                         .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    _db.CartHeaders.Remove(cartheaderToRemove);
                }
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
