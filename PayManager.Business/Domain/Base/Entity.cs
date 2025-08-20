using Newtonsoft.Json;
using System;

namespace PayManager.Business.Domain
{
    public class Entity<T> : IEntity<T>
    {
        public virtual T Id { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
    }

    public class Entity : Entity<Guid>, IEntity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
