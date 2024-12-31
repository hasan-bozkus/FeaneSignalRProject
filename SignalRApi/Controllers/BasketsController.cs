using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.BasketDto;
using SignalR.DtoLayer.DiscountDto;
using SignalR.EntityLayer.Entities;
using SignalRApi.Models;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("GetBasketByMenuTableNumber/{id}")]
        public IActionResult GetBasketByMenuTableNumber(int id)
        {
            var result = _basketService.TGetBasketByMenuTableNumber(id);
            return Ok(result);
        }

        [HttpGet("BasketListByMenuTableWithProductName/{id}")]
        public IActionResult BasketListByMenuTableWithProductName(int id)
        {
            using var context = new SignalRContext();
            var values = context.Baskets.Include(x => x.Product).Where(y => y.MenuTableID == id).Select(z => new ResultBasketListWithProducts
            {
                BasketID = z.BasketID,
                Count = z.Count,
                Price = z.Price,
                MenuTableID = z.MenuTableID,
                ProductID = z.ProductID,
                TotalPrice = z.TotalPrice,
                ProductName = z.Product.ProductName
            }).ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateBasket(CreateBasketDto createBasketDto)
        {
            //Bahçe 01 --> 45
            using var context = new SignalRContext();
            _basketService.TAdd(new Basket()
            {
                ProductID = createBasketDto.ProductID,
                MenuTableID = createBasketDto.MenuTableID,
                Price = context.Products.Where(x => x.ProductID == createBasketDto.ProductID).Select(y => y.Price).FirstOrDefault(),
                Count = 1,
                TotalPrice = createBasketDto.TotalPrice
            });
            return Ok("Sepete Eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBasket(int id)
        {
            var value = _basketService.TGetByID(id);
            _basketService.TDelete(value);
            return Ok("Septteki seçilen ürün silindi");
        }
    }
}
