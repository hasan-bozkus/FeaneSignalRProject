using AutoMapper;
using SignalR.DtoLayer.NotificationDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Mapping
{
    public class NotificationMapping : Profile
    {
        protected NotificationMapping()
        {
            CreateMap<CreateNotificationDto, Notification>().ReverseMap();
            CreateMap<ResultNotificationDto, Notification>().ReverseMap();
            CreateMap<GetNotificationDto, Notification>().ReverseMap();
            CreateMap<UpdateNotificationDto, Notification>().ReverseMap();
        }
    }
}
