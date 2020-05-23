using FluentValidation;

namespace Demo.Core.Services.Commands.CreateHero
{
    public sealed class CreateHeroCommandValidator : AbstractValidator<CreateHeroCommand>
    {
        public CreateHeroCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(5);
        }
    }
}