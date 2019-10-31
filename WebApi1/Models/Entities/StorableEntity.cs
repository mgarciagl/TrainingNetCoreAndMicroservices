using System;

namespace WebApi1.Models.Entities
{
    public abstract class StorableEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
