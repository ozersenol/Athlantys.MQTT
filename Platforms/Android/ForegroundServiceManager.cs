using Android.OS;
using Android.Content;
using Athlantys.MQTT.Platforms.Android;
using Athlantys.MQTT.Services.IServices;
using ProxiWash.KasaApp.Platforms.Android;

[assembly: Dependency(typeof(ForegroundServiceManager))]
namespace ProxiWash.KasaApp.Platforms.Android
{
 
	public class ForegroundServiceManager : IForegroundServiceManager
	{
	
		private readonly IMQTTService _mqttService;

        public ForegroundServiceManager(IMQTTService mqttService)
        {
            _mqttService= mqttService;
        }

        public void Start()
		{
			try
			{
				// Pass the subscriberService instance to the IoTHubService constructor
				//IoTHubService ioTHubService = new IoTHubService();


                var intent = new Intent(MauiApplication.Current.ApplicationContext, typeof(IoTHubService));
				if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
				{
					MauiApplication.Current.ApplicationContext.StartForegroundService(intent);

                }
				else
				{
					MauiApplication.Current.ApplicationContext.StartService(intent);
				}

                _mqttService.StartServer();
            }
			catch (Exception ex)
			{
				// Handle the exception
				Console.WriteLine(ex.Message);
			}

		}

        public void Stop()
        {
            var intent = new Intent(MauiApplication.Current.ApplicationContext, typeof(IoTHubService));
         
                MauiApplication.Current.ApplicationContext.StopService(intent);
        }
    }
}
