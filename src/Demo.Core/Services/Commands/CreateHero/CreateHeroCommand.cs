using MediatR;

namespace Demo.Core.Services.Commands.CreateHero
{
    public sealed class CreateHeroCommand : IRequest<CreateHeroCommandResponse>
    {
        public CreateHeroCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}