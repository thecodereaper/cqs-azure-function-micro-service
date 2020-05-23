using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Demo.Core.Domain.Heroes;
using Demo.Core.Repositories;
using MediatR;
using MediatR.DDD.Exceptions;

namespace Demo.Core.Services.Commands.CreateHero
{
    internal sealed class CreateHeroCommandHandler : IRequestHandler<CreateHeroCommand, CreateHeroCommandResponse>
    {
        private readonly IHeroRepository _heroRepository;

        public CreateHeroCommandHandler(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public async Task<CreateHeroCommandResponse> Handle(CreateHeroCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<Hero> existingHero = await _heroRepository.FindAllAsync(h => h.Name == request.Name);

            if (existingHero.Any())
                throw new ConflictException();

            Hero hero = new Hero(Guid.NewGuid(), request.Name);
            await _heroRepository.CreateAsync(hero);

            return new CreateHeroCommandResponse(hero);
        }
    }
}