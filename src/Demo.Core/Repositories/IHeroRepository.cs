using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Core.Domain.Heroes;
using MediatR.DDD;

namespace Demo.Core.Repositories
{
    internal interface IHeroRepository
    {
        Task<Hero> CreateAsync(Hero hero);

        Task<IEnumerable<Hero>> FindAllAsync(Func<Hero, bool> filter);

        Task<IEnumerable<Hero>> FindAllAsync(IPager pager);

        Task<Hero> FindOneAsync(Guid id);

        Task<Hero> UpdateAsync(Hero hero);

        Task<bool> DeleteAsync(Guid id);
    }
}