using FluentValidation;

namespace Demo.Core.Services.Commands.UpdateHero
{
    public sealed class UpdateHeroCommandValidator : AbstractValidator<UpdateHeroCommand>
    {
        public UpdateHeroCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(5);
        }
    }
}