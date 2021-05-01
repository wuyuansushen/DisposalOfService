using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DisposalOfService.DisposeExample;

namespace DisposalOfService
{
    class Program
    {
        public static void Main(string[] args)
        {
            var myHost = GoAsync(args);
            myHost.GetAwaiter().GetResult();
        }
        private static Task GoAsync(string[] args)
        {
            #region POC
            /*
            using (IHost host = CreateHostBuilder(args).Build())
            {
                ExemplifyDisposableScoping(host.Services, "Scope1");
                Console.WriteLine();
                ExemplifyDisposableScoping(host.Services, "Scope2");
                Console.WriteLine();
                var hostTask = host.RunAsync();
            }
            using(IHost host = CreateHostBuilder(args).Build())
            {
                ExemplifyDisposableScoping(host.Services, "Scope1");
                Console.WriteLine();
                ExemplifyDisposableScoping(host.Services, "Scope2");
                Console.WriteLine();
                var hostTask = host.RunAsync();
                return hostTask;
            }
            */
            #endregion
            IHost host = CreateHostBuilder(args).Build();
            ExemplifyDisposableScoping(host.Services, "Scope1");
            Console.WriteLine();
            ExemplifyDisposableScoping(host.Services, "Scope2");
            Console.WriteLine();
            var hostTask = host.RunAsync();


            for(int i =0;i<10;i++)
            {
                Console.WriteLine($" {i}");
            }
            return hostTask;
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
