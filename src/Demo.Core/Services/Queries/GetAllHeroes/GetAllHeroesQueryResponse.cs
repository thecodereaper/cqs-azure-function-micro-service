using System.Collections.Generic;
using Demo.Core.Domain.Heroes;

namespace Demo.Core.Services.Queries.GetAllHeroes
{
    public sealed class GetAllHeroesQueryResponse
    {
        public GetAllHeroesQueryResponse(IEnumerable<IHero> heroes)
        {
            Heroes = heroes;
        }

        public IEnumerable<IHero> Heroes { get; }
    }
}