using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.CategoryDtos;
using SignalRWebUI.Dtos.ProductDtos;
using SignalRWebUI.Models.Products;

namespace SignalRWebUI.ViewComponents.DefaultComponents
{
    public class _DefaultOurMenuComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultOurMenuComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseProductMessage = await client.GetAsync("https://localhost:44303/api/Product/GetLast9Products");
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
    }
}
