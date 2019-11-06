using System;

namespace TrainingSuperville.Models.Entities
{
    public abstract class StorableEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
