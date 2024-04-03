using Athlantys.MQTT.Platforms.Android;
using Athlantys.MQTT.Services.IServices;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using ProxiWash.KasaApp.Platforms.Android;

namespace Athlantys.MQTT
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                // Initialize the .NET MAUI Community Toolkit by adding the below line of code
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();


            builder.Services.AddTransient<IoTHubService>();
            builder.Services.AddTransient<IForegroundServiceManager, ForegroundServiceManager>();
            builder.Services.AddTransient<IMQTTService, MQTTService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif


            var servicePRovider = builder.Services.BuildServiceProvider();
            var fgService = servicePRovider.GetService<IForegroundServiceManager>();


            builder.ConfigureLifecycleEvents(lifecycle =>
            {
                lifecycle.AddAndroid(android =>
                {
                    android.OnStart((activity) => OnStart(fgService));
                    //android.OnResume((activity) => OnResume(fgService));
                    //android.OnPause((activity) => OnSleep(fgService));
                    //android.OnStop((activity) => OnStop(fgService));

                });
            });

            return builder.Build();
        }


        static bool OnSleep(IForegroundServiceManager sm)
        {
            sm.Start();
            return true;
        }

        static bool OnResume(IForegroundServiceManager sm)
        {
            sm.Start();
            return true;
        }

        static bool OnStart(IForegroundServiceManager sm)
        {
            sm.Start();
            return true;
        }
        static bool OnStop(IForegroundServiceManager sm)
        {
            sm.Start();
            return true;
        }
    }
}
