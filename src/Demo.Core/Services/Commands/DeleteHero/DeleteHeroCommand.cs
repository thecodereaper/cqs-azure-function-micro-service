using System;
using MediatR;

namespace Demo.Core.Services.Commands.DeleteHero
{
    public sealed class DeleteHeroCommand : IRequest<DeleteHeroCommandResponse>
    {
        public DeleteHeroCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}