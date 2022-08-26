using Blazor.Metadata.Domain;
using Blazor.Metadata.Interfaces;
using Blazor.Metadata.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Metadata.Utilities
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMetadata(this IServiceCollection services, Type IRouteMetadataProviderImplementation)
        {
            services.AddSingleton(typeof(IRouteMetadataProvider), IRouteMetadataProviderImplementation);
            services.AddScoped<MetadataTransferService>();
            services.AddOptions<MetadataOptions>("MetadataOptions")
                .Configure<IRouteMetadataProvider>(async (options, provider) =>
                {
                    options.Metadatas = new Dictionary<string, MetadataValues>
                    {
                        {
                            "/",
                            new MetadataValues()
                            {
                                Author = "Matthew Goedde",
                                Description = "Matthew Goedde's Code Blog. A CMS site serving Markdown blog posts to readers on the topic of code.",
                                Keywords = new string[] { "code", "tutorials", "blog", "C#", "dotnet", ".NET", "blazor", "SQL", "Markdown" },
                                Title = "Codre Blog"
                            }
                        },
                        {
                            "/post/create",
                            new MetadataValues()
                            {
                                Author = "Matthew Goedde",
                                Description = "Matthew Goedde's Code Blog. A CMS site serving Markdown blog posts to readers on the topic of code.",
                                Keywords = new string[] { "code", "tutorials", "blog", "C#", "dotnet", ".NET", "blazor", "SQL", "Markdown" },
                                Title = "Create a Post"
                            }
                        }
                    };
                    options.Metadatas = options.Metadatas.Union(await provider.GetRouteMetadataAsync()).ToDictionary(k => k.Key, v => v.Value);
                });

            return services;
        }
    }
}
