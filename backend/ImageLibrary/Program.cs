using ImageLibrary.Domain;
using ImageLibrary.Domain.Exceptions;
using ImageLibrary.Domain.Services.Interfaces;
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
            try
            {
                var serviceCollection = Configure();

                var provider = serviceCollection.BuildServiceProvider();

                var service = provider.GetRequiredService<IImageService>();

                await ExecuteAsync(service);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured during execution: {ex.Message}");
            }
        }

        private static async Task ExecuteAsync(IImageService service)
        {
            try
            {
                if (!TryGetIdFromInput(out var id))
                {
                    Console.WriteLine("Wrong album id:");
                    return;
                }

                var images = await service.GetImagesByAlbumIdAsync(id);
                ProcessImages(id, images);
            }
            catch (ReceivingImageFailedException ex)
            {
                Console.WriteLine($"Receiving images failed: {ex.Message}");
            }

        }

        private static void ProcessImages(int id, System.Collections.Generic.IList<Domain.Models.AlbumImageDTO> images)
        {
            if (images.Count > 0)
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

        private static bool TryGetIdFromInput(out int id)
        {
            Console.WriteLine("Enter album id:");

            var idString = Console.ReadLine();

            if (!int.TryParse(idString, out id))
            {
                return false;
            }

            if (id <= 0)
            {
                return false;
            }

            return true;
        }

        private static IServiceCollection Configure()
        {
            var services = new ServiceCollection();

            var directory = Directory.GetParent(AppContext.BaseDirectory)?.FullName ?? Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
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
