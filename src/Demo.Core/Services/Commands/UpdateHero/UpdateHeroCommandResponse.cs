using Demo.Core.Domain.Heroes;

namespace Demo.Core.Services.Commands.UpdateHero
{
    public sealed class UpdateHeroCommandResponse
    {
        public UpdateHeroCommandResponse(IHero hero)
        {
            Hero = hero;
        }

        public IHero Hero { get; }
    }
}