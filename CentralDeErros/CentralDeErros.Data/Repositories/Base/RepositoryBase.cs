using CentralDeErros.Data.Context;
using CentralDeErros.Domain.Interfaces.Base;
using CentralDeErros.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Data.Repositories.Base
{
    public class RepositoryBase<TModel> : IRepositoryBase<TModel> where TModel : ModelBase
    {
        protected readonly CentralDeErrosContext _context;
        public RepositoryBase(CentralDeErrosContext context)
        {
            _context = context;
        }

        public TModel Add(TModel obj)
        {
            obj.Id = Guid.NewGuid();
            obj.CreatedAt = obj.UpdatedAt = DateTime.UtcNow;

            _context.Set<TModel>().Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public IList<TModel> Find(Func<TModel, bool> predicate)
        {
            return _context.Set<TModel>().Where(predicate).ToList();
        }

        public IList<TModel> GetAll()
        {
            return _context.Set<TModel>().ToList();
        }

        public TModel GetById(Guid id)
        {
            return _context.Set<TModel>().FirstOrDefault(p => p.Id == id);
        }

        public void Remove(Guid id)
        {
            _context.Set<TModel>().Remove(this.GetById(id));
            _context.SaveChanges();
        }

        public TModel Update(TModel obj)
        {
            //_context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
            return obj;
        }
    }
}
