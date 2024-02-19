using ImageLibrary.Domain.Services;
using ImageLibrary.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ImageLibrary.Domain
{
    public static class Dependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            //single instance per every launch
            services.AddSingleton<IImageService, ImageService>();
            services.AddHttpClient();

            return services;
        }
    }
}
