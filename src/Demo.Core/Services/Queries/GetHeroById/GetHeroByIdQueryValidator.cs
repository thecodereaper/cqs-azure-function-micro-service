using FluentValidation;

namespace Demo.Core.Services.Queries.GetHeroById
{
    public sealed class GetHeroByIdQueryValidator : AbstractValidator<GetHeroByIdQuery>
    {
        public GetHeroByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}