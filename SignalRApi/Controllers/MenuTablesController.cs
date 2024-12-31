using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.MenuTableDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuTablesController : ControllerBase
    {
        private readonly IMenuTableService _menuTableService;
        private readonly IMapper _mapper;

        public MenuTablesController(IMenuTableService menuTableService, IMapper mapper)
        {
            _menuTableService = menuTableService;
            _mapper = mapper;
        }

        [HttpGet("MenuTableCount")]
        public IActionResult MenuTableCount()
        {
            var values = _menuTableService.TMenuTableCount();
            return Ok(values);
        }

        [HttpGet]
        public IActionResult MenuTableList()
        {
            var values = _mapper.Map<List<ResultMenuTableDto>>(_menuTableService.TGetListAll());
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateMenuTable(CreateMenuTableDto createMenuTableDto)
        {
            createMenuTableDto.Status = false;
            var value = _mapper.Map<MenuTable>(createMenuTableDto);
            _menuTableService.TAdd(value);
            return Ok("Masa başarılı bir şekilde eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenuTable(int id)
        {
            var value = _menuTableService.TGetByID(id);
            _menuTableService.TDelete(value);
            return Ok("Masa alanı silindi.");
        }

        [HttpPut]
        public IActionResult UpdateMenuTable(UpdateMenuTableDto updateMenuTableDto)
        {
            updateMenuTableDto.Status = false;
            var value = _mapper.Map<MenuTable>(updateMenuTableDto);
            _menuTableService.TUpdate(value);
            return Ok("Masa bilgisi güncellendi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetMenuTable(int id)
        {
            var value = _mapper.Map<GetMenuTableDto>(_menuTableService.TGetByID(id));
            return Ok(value);
        }

        [HttpGet("ChangeMenuTableStatusToFalse/{id}")]
        public IActionResult ChangeMenuTableStatusToFalse(int id)
        {
            _menuTableService.TChangeMenuTableStatusToFalse(id);
            return Ok("İşlem başarılı");
        }

        [HttpGet("ChangeMenuTableStatusToTrue/{id}")]
        public IActionResult ChangeMenuTableStatusToTrue(int id)
        {
            _menuTableService.TChangeMenuTableStatusToTrue(id);
            return Ok("İşlem başarılı");
        }
    }
}
