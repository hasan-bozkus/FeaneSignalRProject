using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
    public class EFBookingDal : GenericRepository<Booking>, IBookingDal
    {
        public EFBookingDal(SignalRContext signalRContext) : base(signalRContext)
        {
        }

        public void BookingStatusApproved(int id)
        {
            using var context = new SignalRContext();
            var values = context.Bookings.Find(id);
            values.Description = "Rezervasyon Onaylandı";
            context.Update(values);
            context.SaveChanges();
        }

        public void BookingStatusCancelled(int id)
        {
            using var context = new SignalRContext();
            var values = context.Bookings.Find(id);
            values.Description = "Rezervasyon İptal Edildi";
            context.Update(values);
            context.SaveChanges();
        }
    }
}
