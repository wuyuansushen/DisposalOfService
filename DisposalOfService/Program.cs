using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DisposalOfService.DisposeExample;

namespace DisposalOfService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            ExemplifyDisposableScoping(host.Services, "Scope1");
            Console.WriteLine();
            ExemplifyDisposableScoping(host.Services, "Scope2");
            Console.WriteLine();
            await host.RunAsync();
        }

        internal static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddTransient<TransientDisposable>();
                    services.AddScoped<ScopedDisposable>();
                    services.AddSingleton<SingletonDisposable>();
                });
        }
        
        internal static void ExemplifyDisposableScoping(IServiceProvider serviceProvider,string scopeName)
        {
            Console.WriteLine($"-----{scopeName}-----");
            using IServiceScope serviceScope = serviceProvider.CreateScope();
            IServiceProvider serviceProvider1 = serviceScope.ServiceProvider;

            _ = serviceProvider1.GetRequiredService<SingletonDisposable>();

            _ = serviceProvider1.GetRequiredService<TransientDisposable>();
            _ = serviceProvider1.GetRequiredService<ScopedDisposable>();

        }
    }
}
