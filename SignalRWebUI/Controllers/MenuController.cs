using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.BasketDtos;
using SignalRWebUI.Dtos.CategoryDtos;
using SignalRWebUI.Dtos.ProductDtos;
using SignalRWebUI.Models.Products;
using System.Net.Http;
using System.Text;

namespace SignalRWebUI.Controllers
{
    [AllowAnonymous]
    public class MenuController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MenuController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.v = id;
            TempData["x"] = id;

            var client = _httpClientFactory.CreateClient();
            var responseProductMessage = await client.GetAsync("https://localhost:44303/api/Product/ProdctListWithCategory");
            var productsWithCategory = new List<ResultProductWithCategoryDto>();
            if (responseProductMessage.IsSuccessStatusCode)
            {
                var prodctJsonData = await responseProductMessage.Content.ReadAsStringAsync();
                productsWithCategory = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(prodctJsonData);
            }

            var responseCategoryMessage = await client.GetAsync("https://localhost:44303/api/Category");
            var categoryDtos = new List<ResultCategoryDto>();
            if (responseCategoryMessage.IsSuccessStatusCode)
            {
                var categoryJsonData = await responseCategoryMessage.Content.ReadAsStringAsync();
                categoryDtos = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(categoryJsonData);
            }

            productsWithCategory = productsWithCategory.OrderBy(p => p.CategoryName).ToList();

            var viewModel = new ProductandCategoryViewModel
            {
                Products = productsWithCategory,
                Categories = categoryDtos
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket([FromBody] CreateBasketDto createBasketDto)
        {         

            var client = _httpClientFactory.CreateClient();
           
            var jsonData = JsonConvert.SerializeObject(createBasketDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44303/api/Baskets/", stringContent);

            await client.GetAsync($"https://localhost:44303/api/MenuTables/ChangeMenuTableStatusToTrue/{createBasketDto.MenuTableID}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return Json(new {success = false, message = "Sepete ekleme başarısız oldu." });
        }
    }
}
