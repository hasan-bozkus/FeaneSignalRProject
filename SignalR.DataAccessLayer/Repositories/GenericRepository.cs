using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly SignalRContext _signalRContext;

        public GenericRepository(SignalRContext signalRContext)
        {
            _signalRContext = signalRContext;
        }

        public void Add(T entity)
        {
            _signalRContext.Add(entity);
            _signalRContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _signalRContext.Remove(entity);
            _signalRContext.SaveChanges();
        }

        public T GetByID(int id)
        {
            return _signalRContext.Set<T>().Find(id);
        }

        public List<T> GetListAll()
        {
            return _signalRContext.Set<T>().ToList();
        }

        public void Update(T entity)
        {
            _signalRContext.Update(entity);
            _signalRContext.SaveChanges();
        }
    }
}
