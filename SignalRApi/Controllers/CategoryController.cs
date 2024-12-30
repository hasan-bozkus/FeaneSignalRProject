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

        [HttpGet("CategoryCount")]
        public IActionResult CategoryCount()
        {
            var values = _categoryService.TCategoryCount();
            return Ok(values);
        }

        [HttpGet("ActiveCategoryCount")]
        public IActionResult ActiveCategoryCount()
        {
            var values = _categoryService.TActiveCategoryCount();
            return Ok(values);
        }

        [HttpGet("PassiveCategoryCount")]
        public IActionResult PassiveCategoryCount()
        {
            var values = _categoryService.TPassiveCategoryCount();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryDto createCategoryDto)
        {
            createCategoryDto.Status = true;
            var value = _mapper.Map<Category>(createCategoryDto);
            _categoryService.TAdd(value);
            return Ok("Kategori başarılı bir şekilde eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var value = _categoryService.TGetByID(id);
            _categoryService.TDelete(value);
            return Ok("Kategori başarılı bir şekilde silindi.");
        }

        [HttpPut]
        public IActionResult UpdateCategory(UpdateCategroyDto updateCategroyDto)
        {
            var value = _mapper.Map<Category>(updateCategroyDto);
           _categoryService.TUpdate(value);
            return Ok("Kategori başarılı bir şekilde güncellendi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetCategroy(int id)
        {
            var value = _mapper.Map<GetCategoryDto>(_categoryService.TGetByID(id));
            return Ok(value);
        }
    }
}
