﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.MessageDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult MessageList()
        {
            var values = _messageService.TGetListAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateMessage(CreateMessageDto createMessageDto)
        {
            Message message = new Message()
            {
                Mail = createMessageDto.Mail,
                MessageContent = createMessageDto.MessageContent,
                MessageSendDate = createMessageDto.MessageSendDate,
                NameSurname = createMessageDto.NameSurname,
                Phone = createMessageDto.Phone,
                Subject = createMessageDto.Subject,
                Status = false
            };

            _messageService.TAdd(message);
            return Ok("Mesaj başarılı bir şekilde gönderildi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            var value = _messageService.TGetByID(id);
            _messageService.TDelete(value);
            return Ok("Mesaj silindi.");
        }

        [HttpPut]
        public IActionResult UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            Message message = new Message()
            {
                MessageID = updateMessageDto.MessageID,
                Mail = updateMessageDto.Mail,
                MessageContent = updateMessageDto.MessageContent,
                MessageSendDate = updateMessageDto.MessageSendDate,
                NameSurname = updateMessageDto.NameSurname,
                Phone = updateMessageDto.Phone,
                Subject = updateMessageDto.Subject,
                Status = false
            };
            _messageService.TUpdate(message);
            return Ok("Mesaj bilgisi güncellendi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetMessage(int id)
        {
            var value = _messageService.TGetByID(id);
            return Ok(value);
        }
    }
}
