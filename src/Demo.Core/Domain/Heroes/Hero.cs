using System;

namespace Demo.Core.Domain.Heroes
{
    internal sealed class Hero : IHero
    {
        public Hero(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; private set; }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}