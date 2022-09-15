using Funq;
using ServiceStack;
using BlazorServerTailwind.ServiceInterface;

[assembly: HostingStartup(typeof(BlazorServerTailwind.AppHost))]

namespace BlazorServerTailwind;

public class AppHost : AppHostBase, IHostingStartup
{
    public AppHost() : base("BlazorServerTailwind", typeof(MyServices).Assembly) { }

    public override void Configure(Container container)
    {
        SetConfig(new HostConfig {
        });

        Plugins.Add(new CorsFeature(allowedHeaders: "Content-Type,Authorization",
            allowOriginWhitelist: new[]{
            "http://localhost:5000",
            "https://localhost:5001"
        }, allowCredentials: true));
    }

    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) => 
            services.ConfigureNonBreakingSameSiteCookies(context.HostingEnvironment));
}
