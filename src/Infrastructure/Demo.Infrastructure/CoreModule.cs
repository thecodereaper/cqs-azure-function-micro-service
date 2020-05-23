using Demo.Core;
using Demo.Core.Repositories;
using Demo.Core.Services;
using Demo.Infrastructure.Repositories;
using Demo.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Infrastructure
{
    public sealed class CoreModule : ICoreModule
    {
        private readonly IServiceCollection _serviceCollection;

        public CoreModule(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public void Initialize()
        {
            _serviceCollection.AddSingleton<IHeroRepository, HeroRepository>();
            _serviceCollection.AddMediatR(typeof(ICoreModule));
            _serviceCollection.AddValidatorsFromAssemblyContaining(typeof(ICoreModule));
            _serviceCollection.AddSingleton<IValidatorService, ValidatorService>();
        }
    }
}