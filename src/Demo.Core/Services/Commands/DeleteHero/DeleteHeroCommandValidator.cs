using FluentValidation;

namespace Demo.Core.Services.Commands.DeleteHero
{
    public sealed class DeleteHeroCommandValidator : AbstractValidator<DeleteHeroCommand>
    {
        public DeleteHeroCommandValidator()
        {
            RuleFor(h => h.Id).NotNull().NotEmpty();
        }
    }
}