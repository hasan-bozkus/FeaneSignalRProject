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
    public class EFMenuTableDal : GenericRepository<MenuTable>, IMenuTableDal

    {
        public EFMenuTableDal(SignalRContext signalRContext) : base(signalRContext)
        {
        }

        public void ChangeMenuTableStatusToFalse(int id)
        {
            using var context = new SignalRContext();
            var value =context.MenuTables.Where(x => x.MenuTableID == id).FirstOrDefault();
            value.Status = false;
            context.Update(value);
            context.SaveChanges();        
        }

        public void ChangeMenuTableStatusToTrue(int id)
        {
            using var context = new SignalRContext();
            var value = context.MenuTables.Where(x => x.MenuTableID == id).FirstOrDefault();
            value.Status = true;
            context.Update(value);
            context.SaveChanges();
        }

        public int MenuTableCount()
        {
            using var context = new SignalRContext();
            return context.MenuTables.Count();
        }
    }
}
