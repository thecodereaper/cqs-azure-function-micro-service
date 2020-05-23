using Demo.Core.Domain.Heroes;

namespace Demo.Core.Services.Commands.CreateHero
{
    public sealed class CreateHeroCommandResponse
    {
        public CreateHeroCommandResponse(IHero hero)
        {
            Hero = hero;
        }

        public IHero Hero { get; }
    }
}