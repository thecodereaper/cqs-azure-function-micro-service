using System;
using MediatR;

namespace Demo.Core.Services.Queries.GetHeroById
{
    public sealed class GetHeroByIdQuery : IRequest<GetHeroByIdQueryResponse>
    {
        public GetHeroByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}