using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingSuperville.Models.Entities;

namespace TrainingSuperville.Models
{
    public interface IStorage<T> where T : StorableEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int entityId);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Remove(int id);
    }
}
