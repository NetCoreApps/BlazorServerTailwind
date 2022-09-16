using ServiceStack;
using BlazorServerTailwind.Data;
using BlazorServerTailwind.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using ServiceStack.Blazor;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
});
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<LocalStorage>();
var baseUrl = Environment.GetEnvironmentVariable("DEPLOY_API");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl ?? builder.Configuration["ApiBaseUrl"] ?? "https://localhost:5001/") });
builder.Services.AddBlazorApiClient(baseUrl ?? builder.Configuration["ApiBaseUrl"] ?? "https://localhost:5001/"); // builder.HostEnvironment.BaseAddress);

builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<ServiceStackStateProvider>());
builder.Services.AddScoped<ServiceStackStateProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseServiceStack(new AppHost());

BlazorConfig.EnableVerboseLogging = true;

app.Run();
