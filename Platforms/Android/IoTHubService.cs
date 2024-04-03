using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using Resource = Microsoft.Maui.Resource;

namespace Athlantys.MQTT.Platforms.Android
{
    [Service]
    public class IoTHubService : Service
    {

public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            // Create a notification for the foreground service
            CreateNotificationChannel();
            var notification = CreateNotification();
            StartForeground(10002, notification);

            // Your service logic here
         

            return StartCommandResult.Sticky;
        }

        
        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            var channelName = "Athlantys MQTT";
            var channelDescription = "Athlantys MQTT Servis Kanali";
            var channel = new NotificationChannel("10005", channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        private Notification CreateNotification()
        {
            var notificationBuilder = new NotificationCompat.Builder(this, "10005")
                .SetContentTitle("Athlantys MQTT")
                .SetContentText("Servis Calisiyor")
                .SetSmallIcon(Resource.Drawable.caricon24x)
                .SetOngoing(true);

            return notificationBuilder.Build();
        }

       
    }
}
