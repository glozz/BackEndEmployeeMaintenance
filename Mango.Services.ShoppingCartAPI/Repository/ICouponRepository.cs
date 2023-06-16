﻿using Mango.Services.ShoppingCartAPI.Models;

namespace Mango.Services.ShoppingCartAPI.Repository
{
    public interface ICouponRepository 
    {
        Task<CouponDto> GetCoupon(string couponName);
    }
}
