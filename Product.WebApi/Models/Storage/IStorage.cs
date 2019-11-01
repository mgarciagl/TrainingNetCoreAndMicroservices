using Product.WebApi.Models.Entities;
using System;
using System.Collections.Generic;

namespace Product.WebApi.Models
{
    public interface IStorage<T> where T : StorableEntity
    {
        IEnumerable<T> GetAll(Type type);
        T Get(Type type, Guid id);
        void Add(T entity);
        void Update(T entity);
        bool Remove(Type type, Guid id);
    }
}
