using Demo.Core;
using Demo.Function;
using Demo.Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Demo.Function
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