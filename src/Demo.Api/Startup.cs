using Demo.Api;
using Demo.Core;
using Demo.Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Demo.Api
{
    public sealed class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            ICoreModule coreModule = new CoreModule(builder.Services);
            coreModule.Initialize();
        }
    }
}