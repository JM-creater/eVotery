using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using OnlineVotingSystem.Application.ImageDirectory;

namespace OnlineVotingSystem.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IApplicationBuilder app, IConfiguration configuration)
    {
        var serviceProvider = app.ApplicationServices;
        var imagePathOptions = serviceProvider.GetRequiredService<IOptions<ImagePathOptions>>().Value;
        var env = serviceProvider.GetService<IWebHostEnvironment>();

        var imageFolderPath = Path.Combine(env.ContentRootPath, imagePathOptions.PathImages);

        if (!Directory.Exists(imageFolderPath))
        {
            Directory.CreateDirectory(imageFolderPath);
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(imageFolderPath),
            RequestPath = $"/{imagePathOptions.PathImages}"
        });
    }
}
