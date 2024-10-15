using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.CategoryDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CategoryList()
        {
            var value = _mapper.Map<List<ResultCategoryDto>>(_categoryService.TGetListAll());
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryDto createCategoryDto)
        {
            _categoryService.TAdd(new Category()
            {
                CategoryName = createCategoryDto.CategoryName,
                Status = true
            });
            return Ok("Kategori başarılı bir şekilde eklendi.");
        }

        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var value = _categoryService.TGetByID(id);
            _categoryService.TDelete(value);
            return Ok("Kategori başarılı bir şekilde silindi.");
        }

        [HttpPut]
        public IActionResult UpdateCategory(UpdateCategroyDto updateCategroyDto)
        {
           
            _categoryService.TUpdate(new Category()
            {
                CategoryID = updateCategroyDto.CategoryID,
                CategoryName = updateCategroyDto.CategoryName,
                Status = updateCategroyDto.Status
            });
            return Ok("Kategori başarılı bir şekilde güncellendi.");
        }

        [HttpGet("GetBooking")]
        public IActionResult GetCategroy(int id)
        {
            var value = _categoryService.TGetByID(id);
            return Ok(value);
        }
    }
}
