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
    public class EFDiscountDal : GenericRepository<Discount>, IDiscountDal
    {
        public EFDiscountDal(SignalRContext signalRContext) : base(signalRContext)
        {
        }

        public void ChangeStatusToFalse(int id)
        {
            using var context = new SignalRContext();
            var value = context.Discounts.Find(id);
            value.Status = false;
            context.Update(value);
            context.SaveChanges();
        }

        public void ChangeStatusToTrue(int id)
        {
            using var context = new SignalRContext();
            var value = context.Discounts.Find(id);
            value.Status = true;
            context.Update(value);
            context.SaveChanges();
        }

        public List<Discount> GetListByStatusTrue()
        {
            using var context = new SignalRContext();
            var value = context.Discounts.Where(x => x.Status == true).ToList();
            return value;
        }
    }
}
