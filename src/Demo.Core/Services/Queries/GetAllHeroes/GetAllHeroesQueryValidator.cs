using FluentValidation;

namespace Demo.Core.Services.Queries.GetAllHeroes
{
    public sealed class GetAllHeroesQueryValidator : AbstractValidator<GetAllHeroesQuery>
    {
        public GetAllHeroesQueryValidator()
        {
            When
            (
                x => x.Offset > 0, () => { RuleFor(x => x.Limit).GreaterThan(0); }
            );

            When
            (
                x => !string.IsNullOrEmpty(x.SortOrder), () => { RuleFor(x => x.SortBy).NotNull().NotEmpty(); }
            );

            When
            (
                x => !string.IsNullOrEmpty(x.SortBy), () => { RuleFor(x => x.SortOrder).NotNull().NotEmpty(); }
            );
        }
    }
}