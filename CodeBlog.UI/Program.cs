using Blazorise;
using Blazorise.Material;
using Blazorise.Icons.Material;
using Blazor.Metadata.Utilities;
using CodeBlog.Data.Domain;
using CodeBlog.Data.Services;
using CodeBlog.Data.Interfaces;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMetadata(IRouteMetadataProviderImplementation: typeof(MetadataRepository));

builder.Services.AddScoped<IRepository<MetadataEntity>, MetadataRepository>();
builder.Services.AddScoped<IRepository<Post>, PostRepository>();

builder.Services
    .AddBlazorise(options =>
    {
        options.Debounce = true;
        options.DebounceInterval = 300;
    })
    .AddMaterialProviders()
    .AddMaterialIcons();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseResponseCompression();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.RunAsync();
