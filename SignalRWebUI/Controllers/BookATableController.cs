using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.BookingDtos;
using SignalRWebUI.Dtos.ContactDtos;
using SignalRWebUI.Models;
using System.Text;

namespace SignalRWebUI.Controllers
{
    [AllowAnonymous]
    public class BookATableController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookATableController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44303/api/Contact");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
                ViewBag.location = values.Select(x => x.Location).FirstOrDefault();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateBookingDto createBookingDto)
        {
            createBookingDto.Description = "Herhangi bir istek yok.";

            var client = _httpClientFactory.CreateClient();
            var mapResponseMessage = await client.GetAsync("https://localhost:44303/api/Contact");
            if (mapResponseMessage.IsSuccessStatusCode)
            {
                var mapJsonData = await mapResponseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(mapJsonData);
                ViewBag.location = values.Select(x => x.Location).FirstOrDefault();
            }

            var jsonData = JsonConvert.SerializeObject(createBookingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44303/api/Booking", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var errorRespones = await responseMessage.Content.ReadFromJsonAsync<ApiValidationErrorResponse>();
                if(errorRespones?.Errors != null)
                {
                    foreach(var item in errorRespones.Errors)
                    {
                        foreach(var errorMessage in item.Value)
                        {
                            ModelState.AddModelError(item.Key, errorMessage);
                        }
                    }
                }
                
                return View(createBookingDto);
            }
        }
    }
}
