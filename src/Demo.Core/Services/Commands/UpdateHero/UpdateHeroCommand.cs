using System;
using MediatR;

namespace Demo.Core.Services.Commands.UpdateHero
{
    public sealed class UpdateHeroCommand : IRequest<UpdateHeroCommandResponse>
    {
        public UpdateHeroCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}