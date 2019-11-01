using System;
using System.Collections.Generic;
using System.Linq;
using Client.WebApi.Models.Entities;

namespace Client.WebApi.Models
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
            if (storableEntity != null && storableEntity.GetType().Equals(typeof(Entities.Client)))
            {
                var client = (Entities.Client)storableEntity;
                var clientUpdated = (Entities.Client)entity;

                client.Name = clientUpdated.Name;
                client.Email = clientUpdated.Email;
            }
        }

        public StorableEntity Get(Type type, Guid id)
        {
            return storableEntities.FirstOrDefault(d => d.GetType() == type && d.Id == id);
        }

        public IEnumerable<StorableEntity> GetAll(Type type)
        {
            return storableEntities.Where(d => d.GetType() == type).ToList();
        }

        public bool Remove(Type type, Guid id)
        {
            var entity = Get(type, id);

            return storableEntities.Remove(entity);
        }
    }
}
