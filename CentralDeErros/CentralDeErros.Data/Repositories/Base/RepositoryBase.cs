using CentralDeErros.Domain.Interfaces.Base;
using CentralDeErros.Domain.Interfaces;
using CentralDeErros.Domain.Models.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CentralDeErros.Data.Context;
using System.Linq;

namespace CentralDeErros.Data.Repositories.Base
{
    public class RepositoryBase<TModel> : IRepositoryBase<TModel> where TModel : ModelBase
    {
        protected readonly CentralDeErrosContext _context;
        public RepositoryBase(CentralDeErrosContext context)
        {
            _context = context;
        }

        public async Task<TModel> Add(TModel obj)
        {
            obj.Id = Guid.NewGuid();
            obj.CreatedAt = obj.UpdatedAt = DateTime.UtcNow;
                         
            await _context.Set<TModel>().AddAsync(obj);
            await _context.SaveChangesAsync();
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

        public void Update(TModel obj)
        {
            _context.Set<TModel>().Update(obj);
        }
    }
}
