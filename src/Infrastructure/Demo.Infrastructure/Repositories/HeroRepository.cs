using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Core.Domain.Heroes;
using Demo.Core.Repositories;
using MediatR.DDD;

namespace Demo.Infrastructure.Repositories
{
    internal sealed class HeroRepository : IHeroRepository
    {
        private readonly IList<Hero> _heroes;

        public HeroRepository()
        {
            _heroes = new List<Hero>();

            foreach (string hero in _data) _heroes.Add(new Hero(Guid.NewGuid(), hero));
        }

        private static readonly string[] _data =
        {
            "Wolverine",
            "Iron Man",
            "Ant-Man",
            "Aquaman",
            "Asterix", "The Atom",
            "Batman",
            "Black Canary",
            "Captain America",
            "Captain Marvel",
            "Doctor Strange",
            "Ghost Rider",
            "Incredible Hulk",
            "Robin",
            "Spider-Man",
            "Superman",
            "Thor",
            "Wonder Woman"
        };

#pragma warning disable 1998
        public async Task<Hero> CreateAsync(Hero hero)
#pragma warning restore 1998
        {
            _heroes.Add(hero);
            return hero;
        }

#pragma warning disable 1998
        public async Task<IEnumerable<Hero>> FindAllAsync(Func<Hero, bool> filter)
#pragma warning restore 1998
        {
            return _heroes.Where(filter).ToList();
        }

#pragma warning disable 1998
        public async Task<IEnumerable<Hero>> FindAllAsync(IPager pager)
#pragma warning restore 1998
        {
            IEnumerable<Hero> heroes = _heroes;

            if (!string.IsNullOrEmpty(pager.Filter))
                heroes = heroes.Where(h => h.Name.ToLower().Contains(pager.Filter.ToLower()));

            if (pager.Offset > 0)
                heroes = heroes.Skip(pager.Limit * pager.Offset).Take(pager.Limit);

            if (!string.IsNullOrEmpty(pager.SortBy))
            {
                if (pager.SortBy.ToLower() == "id")
                    heroes = pager.SortOrder == "desc" ? heroes.OrderByDescending(h => h.Id) : heroes.OrderBy(h => h.Id);
                else if (pager.SortOrder == "desc")
                    heroes = heroes.OrderByDescending(h => h.Name);
                else
                    heroes = heroes.OrderBy(h => h.Name);
            }

            return heroes.ToList();
        }

#pragma warning disable 1998
        public async Task<Hero> FindOneAsync(Guid id)
#pragma warning restore 1998
        {
            return _heroes.FirstOrDefault(h => h.Id == id);
        }

#pragma warning disable 1998
        public async Task<Hero> UpdateAsync(Hero hero)
#pragma warning restore 1998
        {
            Hero existingHero = _heroes.FirstOrDefault(h => h.Id == hero.Id);

            _heroes.Remove(existingHero);
            _heroes.Add(hero);

            return hero;
        }

#pragma warning disable 1998
        public async Task<bool> DeleteAsync(Guid id)
#pragma warning restore 1998
        {
            Hero hero = _heroes.FirstOrDefault(h => h.Id == id);
            _heroes.Remove(hero);

            return true;
        }
    }
}