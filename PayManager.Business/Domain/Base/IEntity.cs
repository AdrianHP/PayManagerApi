using System;

namespace PayManager.Business.Domain;

public interface IEntity<T>
{
    T Id { get; set; }
}

public interface IEntity : IEntity<Guid>
{
}