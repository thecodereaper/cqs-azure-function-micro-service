using System.Threading;
using System.Threading.Tasks;
using Demo.Core.Domain.Heroes;
using Demo.Core.Repositories;
using MediatR;
using MediatR.DDD.Exceptions;

namespace Demo.Core.Services.Queries.GetHeroById
{
    internal sealed class GetHeroByIdQueryHandler : IRequestHandler<GetHeroByIdQuery, GetHeroByIdQueryResponse>
    {
        private readonly IHeroRepository _heroRepository;

        public GetHeroByIdQueryHandler(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public async Task<GetHeroByIdQueryResponse> Handle(GetHeroByIdQuery request, CancellationToken cancellationToken)
        {
            Hero hero = await _heroRepository.FindOneAsync(request.Id);

            if (hero == null)
                throw new NotFoundException();

            return new GetHeroByIdQueryResponse(hero);
        }
    }
}