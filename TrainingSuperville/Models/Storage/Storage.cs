using System;
using System.Collections.Generic;
using System.Linq;
using TrainingSuperville.Models.Entities;

namespace TrainingSuperville.Models
{
    public class Storage : IStorage<StorableEntity>
    {
        private static readonly Storage _storage = new Storage();

        public Storage() { }

        public static Storage GetInstance() => _storage;

        public static List<StorableEntity> storableEntities { get; set; } = new List<StorableEntity>();

        public void Add(StorableEntity entity)
        {
            if (storableEntities != null)
            {
                entity.Id = Guid.NewGuid();
                storableEntities.Add(entity);
            }
        }

        public void Update(StorableEntity entity)
        {
            var storableEntity = storableEntities.FirstOrDefault(d => d.Id == entity.Id);
            if (storableEntity != null && storableEntity.GetType().Equals(typeof(Client)))
            {
                var client = (Client)storableEntity;
                var clientUpdated = (Client)entity;

                client.Name = clientUpdated.Name;
                client.Email = clientUpdated.Email;
            }
            else if (storableEntity != null && storableEntity.GetType().Equals(typeof(Product)))
            {
                var product = (Product)storableEntity;
                var productUpdated = (Product)entity;

                product.Code = productUpdated.Code;
                product.Name = productUpdated.Name;
            }
        }

        public StorableEntity Get(Guid entityId)
        {
            return storableEntities.FirstOrDefault(d => d.Id == entityId);
        }

        public IEnumerable<StorableEntity> GetAll()
        {
            return storableEntities.ToList();
        }

        public bool Remove(Guid entityId)
        {
            var entity = Get(entityId);

            return storableEntities.Remove(entity);
        }
    }
}
