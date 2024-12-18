﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.DiscountDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountService discountService, IMapper mapper)
        {
            _discountService = discountService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult DiscountList()
        {
            var value = _mapper.Map<List<ResultDiscountDto>>(_discountService.TGetListAll());
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateDiscount(CreateDiscountDto createDiscountDto)
        {
            _discountService.TAdd(new Discount()
            {
               Amount = createDiscountDto.Amount,
               Description = createDiscountDto.Description,
               ImageUrl = createDiscountDto.ImageUrl,   
               Title = createDiscountDto.Title,
               Status = createDiscountDto.Status,
            });
            return Ok("Rezervasyon başarılı bir şekilde eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDiscount(int id)
        {
            var value = _discountService.TGetByID(id);
            _discountService.TDelete(value);
            return Ok("Rezervasyon başarılı bir şekilde silindi.");
        }

        [HttpPut]
        public IActionResult UpdateDiscount(UpdateDsicountDto updateDsicountDto)
        {

            _discountService.TUpdate(new Discount()
            {
                DiscountID = updateDsicountDto.DiscountID,
                Amount = updateDsicountDto.Amount,
                Description = updateDsicountDto.Description,
                ImageUrl = updateDsicountDto.ImageUrl,
                Title = updateDsicountDto.Title,
                Status = updateDsicountDto.Status
            });
            return Ok("Rezervasyon başarılı bir şekilde güncellendi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetDiscount(int id)
        {
            var value = _discountService.TGetByID(id);
            return Ok(value);
        }

        [HttpGet("ChangeStatusToFalse/{id}")]
        public IActionResult ChangeStatusToFalse(int id)
        {
            _discountService.TChangeStatusToFalse(id);
            return Ok("İndirim Pasif oldu");
        }

        [HttpGet("ChangeStatusToTrue/{id}")]
        public IActionResult ChangeStatusToTrue(int id)
        {
            _discountService.TChangeStatusToTrue(id);
            return Ok("İndirim Aktif oldu");
        }

        [HttpGet("GetListByStatusTrue")]
        public IActionResult GetListByStatusTrue()
        {
            var values = _discountService.TGetListByStatusTrue();
            return Ok(values);
        }
    }
}
