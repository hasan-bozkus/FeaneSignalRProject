using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.NotificationDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationsController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult NotificationList()
        {
            var values = _mapper.Map<ResultNotificationDto>(_notificationService.TGetListAll());
            return Ok(values);
        }

        [HttpGet("NotificationCountByStatusFalse")]
        public IActionResult NotificationCountByStatusFalse()
        {
            var values = _notificationService.TNotificationCountByStatusFalse();
            return Ok(values);
        }

        [HttpGet("GetAllNotificationByFalse")]
        public IActionResult TGetAllNotificationByFalse()
        {
            var values = _notificationService.TGetAllNotificationByFalse();
            return Ok(values);
        }

        [HttpPost]
        public ActionResult CreateNotification(CreateNotificationDto createNotificationDto)
        {
            createNotificationDto.Status = false;
            createNotificationDto.Date = Convert.ToDateTime(DateTime.Now.ToString());
            var value = _mapper.Map<Notification>(createNotificationDto);
            _notificationService.TAdd(value);
            return Ok("bildirim eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var value = _notificationService.TGetByID(id);
            _notificationService.TDelete(value);
            return Ok("bildirim silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetNotification(int id)
        {
            var value = _mapper.Map<GetNotificationDto>(_notificationService.TGetByID(id));
            return Ok(value);
        }

        [HttpPut]
        public ActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            updateNotificationDto.Status = false;
            var value = _mapper.Map<Notification>(updateNotificationDto);
            _notificationService.TUpdate(value);
            return Ok("bildirim güncellendi");
        }

        [HttpGet("NotificationStatusChangeToFalse/{id}")]
        public IActionResult NotificationStatusChangeToFalse(int id)
        {
            _notificationService.TNotificationStatusChangeToFalse(id);
            return Ok("Güncelleme Yapıldı");
        }
        [HttpGet("NotificationStatusChangeToTrue/{id}")]
        public IActionResult NotificationStatusChangeToTrue(int id)
        {
            _notificationService.TNotificationStatusChangeToTrue(id);
            return Ok("Güncelleme Yapıldı");
        }
    }
}
