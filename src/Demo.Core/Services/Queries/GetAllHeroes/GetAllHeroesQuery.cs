using MediatR;
using MediatR.DDD;

namespace Demo.Core.Services.Queries.GetAllHeroes
{
    public sealed class GetAllHeroesQuery : IRequest<GetAllHeroesQueryResponse>, IPager
    {
        public GetAllHeroesQuery(int offset, int limit, string sortBy, string sortOrder, string filter)
        {
            Offset = offset;
            Limit = limit;
            SortBy = sortBy;
            SortOrder = sortOrder;
            Filter = filter;
        }

        public int Offset { get; }
        public int Limit { get; }
        public string SortBy { get; }
        public string SortOrder { get; }
        public string Filter { get; }
    }
}