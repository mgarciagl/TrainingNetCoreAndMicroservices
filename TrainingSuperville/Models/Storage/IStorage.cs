using System;
using System.Collections.Generic;
using TrainingSuperville.Models.Entities;

namespace TrainingSuperville.Models
{
    public interface IStorage<T> where T : StorableEntity
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Add(T entity);
        void Update(T entity);
        bool Remove(Guid id);
    }
}
