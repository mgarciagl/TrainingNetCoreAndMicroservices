﻿using System;
using System.Collections.Generic;
using WebApi2.Models.Entities;

namespace WebApi2.Models
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
