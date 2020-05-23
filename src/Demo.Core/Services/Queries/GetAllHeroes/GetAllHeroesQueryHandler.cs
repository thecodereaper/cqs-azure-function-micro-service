using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Demo.Core.Domain.Heroes;
using Demo.Core.Repositories;
using MediatR;

namespace Demo.Core.Services.Queries.GetAllHeroes
{
    internal sealed class GetAllHeroesQueryHandler : IRequestHandler<GetAllHeroesQuery, GetAllHeroesQueryResponse>
    {
        private readonly IHeroRepository _heroRepository;

        public GetAllHeroesQueryHandler(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public async Task<GetAllHeroesQueryResponse> Handle(GetAllHeroesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<IHero> heroes = await _heroRepository.FindAllAsync(request);
            return new GetAllHeroesQueryResponse(heroes);
        }
    }
}