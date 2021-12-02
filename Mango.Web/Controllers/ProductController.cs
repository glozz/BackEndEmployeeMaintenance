using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productionService;

        public ProductController(IProductServices productionService)
        {
            _productionService = productionService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new List<ProductDto>();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _productionService.GetAllProductAsync<ResponseDto>(accessToken);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productionService.CreateProductAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditProduct(int productId)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productionService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
                if (response != null && response.IsSuccess)
                {
                    ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productionService.UpdateProductAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productionService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
                if (response != null && response.IsSuccess)
                {
                    ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productionService.DeleteProductAsync<ResponseDto>(model.ProductId, accessToken);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }
    }
}
