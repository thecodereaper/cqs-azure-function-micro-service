using System;

namespace Demo.Core.Domain.Heroes
{
    public interface IHero
    {
        Guid Id { get; }
        string Name { get; }
    }
}