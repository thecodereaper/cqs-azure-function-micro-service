using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Demo.Core.Domain.Heroes;
using Demo.Core.Repositories;
using MediatR;
using MediatR.DDD.Exceptions;

namespace Demo.Core.Services.Commands.UpdateHero
{
    internal sealed class UpdateHeroCommandHandler : IRequestHandler<UpdateHeroCommand, UpdateHeroCommandResponse>
    {
        private readonly IHeroRepository _heroRepository;

        public UpdateHeroCommandHandler(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public async Task<UpdateHeroCommandResponse> Handle(UpdateHeroCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<Hero> existingHero = await _heroRepository.FindAllAsync(h => h.Name == request.Name && h.Id != request.Id);

            if (existingHero.Any())
                throw new ConflictException();

            Hero hero = await _heroRepository.FindOneAsync(request.Id);
            hero.ChangeName(request.Name);

            await _heroRepository.UpdateAsync(hero);

            return new UpdateHeroCommandResponse(hero);
        }
    }
}