using System.Threading;
using System.Threading.Tasks;
using Demo.Core.Domain.Heroes;
using Demo.Core.Repositories;
using MediatR;
using MediatR.DDD.Exceptions;

namespace Demo.Core.Services.Commands.DeleteHero
{
    internal sealed class DeleteHeroCommandHandler : IRequestHandler<DeleteHeroCommand, DeleteHeroCommandResponse>
    {
        private readonly IHeroRepository _heroRepository;

        public DeleteHeroCommandHandler(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public async Task<DeleteHeroCommandResponse> Handle(DeleteHeroCommand request, CancellationToken cancellationToken)
        {
            Hero hero = await _heroRepository.FindOneAsync(request.Id);

            if (hero == null)
                throw new NotFoundException();

            await _heroRepository.DeleteAsync(hero.Id);

            return new DeleteHeroCommandResponse(hero.Id);
        }
    }
}