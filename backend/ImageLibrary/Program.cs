using ImageLibrary.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageLibrary
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = Configure();

            var provider = serviceCollection.BuildServiceProvider();
            var service = provider.GetService<IImageService>();

            await Execute(service);
        }

        private static async Task Execute(IImageService service)
        {
            Console.WriteLine("Enter album id:");

            try
            {
                var idString = Console.ReadLine();

                if (!int.TryParse(idString, out var id))
                {
                    Console.WriteLine("Wrong album id:");
                }

                var images = await service.GetImagesByAlbumIdAsync(id);

                if (images.Any())
                {
                    foreach (var image in images)
                        Console.WriteLine($"photo-album {id} [{image.Id}] {image.Title}");
                }
                else
                {
                    Console.WriteLine("No images");
                }

                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error occured during execution: {ex.Message}");
            }

        }

        private static IServiceCollection Configure()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddSingleton<IConfiguration>(configuration);
            
            var appConfig = new AppConfig();
            configuration.GetSection(nameof(AppConfig)).Bind(appConfig);

            services.AddSingleton(appConfig);
            services.AddServiceDependencies();

            return services;
        }
    }
}
