using System;

namespace Demo.Core.Services.Commands.DeleteHero
{
    public sealed class DeleteHeroCommandResponse
    {
        public DeleteHeroCommandResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}