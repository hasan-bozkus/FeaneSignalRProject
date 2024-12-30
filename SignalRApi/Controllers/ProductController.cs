using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.ProductDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var value = _mapper.Map<List<ResultProductDto>>(_productService.TGetListAll());
            return Ok(value);
        }

        [HttpGet("ProductCount")]
        public IActionResult ProductCount()
        {
            var values = _productService.TProductCount();
            return Ok(values);
        }

        [HttpGet("ProductCountByNameDrink")]
        public IActionResult ProductCountByNameDrink()
        {
            var values = _productService.TProductCountByNameDrink();
            return Ok(values);
        }

        [HttpGet("ProductCountByNameHamburger")]
        public IActionResult ProductCountByNameHamburger()
        {
            var values = _productService.TProductCountByNameHumburger();
            return Ok(values);
        }

        [HttpGet("ProdctListWithCategory")]
        public IActionResult ProdctListWithCategory()
        {
            //var context = new SignalRContext();
            //var values = context.Products.Include(x => x.Category).Select(y => new ResultProductWithCategoryDto
            //{
            //    Description = y.Description,
            //    ImageUrl = y.ImageUrl,
            //    Price = y.Price,
            //    ProductID = y.ProductID,
            //    ProductName = y.ProductName,
            //    ProductStatus = y.ProductStatus,
            //    CategoryName = y.Category.CategoryName
            //}).ToList();

            var values = _mapper.Map<List<ResultProductWithCategoryDto>>(_productService.TGetProductsWithCategories());
            return Ok(values);
        }

        [HttpGet("ProductPriceAvg")]
        public IActionResult ProductPriceAvg()
        {
            var values = _productService.TProductPriceAvg();
            return Ok(values);
        }

        [HttpGet("ProductNameByMaxPrice")]
        public IActionResult ProductNameByMaxPrice()
        {
            var values = _productService.TProductNameByMaxPrice();
            return Ok(values);
        }

        [HttpGet("ProductNameByMinPrice")]
        public IActionResult ProductNameByMinPrice()
        {
            var values = _productService.TProductNameByMinPrice();
            return Ok(values);
        }

        [HttpGet("ProductAvgPriceByHamburger")]
        public IActionResult ProductAvgPriceByHamburger()
        {
            var values = _productService.TProductAvgPriceByHamburger();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            var value = _mapper.Map<Product>(createProductDto);
            _productService.TAdd(value);
            return Ok("Ürün başarılı bir şekilde eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var value = _productService.TGetByID(id);
            _productService.TDelete(value);
            return Ok("Ürün başarılı bir şekilde silindi.");
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);
            _productService.TUpdate(value);
            return Ok("Ürün başarılı bir şekilde güncellendi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var value = _mapper.Map<GetProductDto>(_productService.TGetByID(id));
            return Ok(value);
        }

        [HttpGet("ProductPriceBySetakBurger")]
        public IActionResult ProductPriceBySetakBurger()
        {
            var values = _productService.TProductPriceBySetakBurger();
            return Ok(values);
        }

        [HttpGet("TotalPriceByDringCategory")]
        public IActionResult TotalPriceByDrinkCategory()
        {
            var values = _productService.TTotalPriceByDrinkCategory();
            return Ok(values);
        }

        [HttpGet("TotalPriceBySalatCategory")]
        public IActionResult TotalPriceBySalatCategory()
        {
            var values = _productService.TTotalPriceBySalatCategory();
            return Ok(values);
        }

        [HttpGet("GetLast9Products")]
        public IActionResult GetLast9Products()
        {
            var value = _mapper.Map<List<ResultGetLast9ProductsDto>>(_productService.TGetLast9Products());
            return Ok(value);
        }
    }
}
