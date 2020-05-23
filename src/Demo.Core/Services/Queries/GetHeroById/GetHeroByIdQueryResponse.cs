using Demo.Core.Domain.Heroes;

namespace Demo.Core.Services.Queries.GetHeroById
{
    public sealed class GetHeroByIdQueryResponse
    {
        public GetHeroByIdQueryResponse(IHero hero)
        {
            Hero = hero;
        }

        public IHero Hero { get; }
    }
}