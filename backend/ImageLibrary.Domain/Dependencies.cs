﻿
using ImageLibrary.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ImageLibrary.Domain
{
    public static class Dependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            //single instance per every launch
            services.AddSingleton<IImageService, ImageService>();
            services.AddSingleton<IImageLibraryClient, ImageLibraryClient>();
            services.AddHttpClient();

            return services;
        }
    }
}
