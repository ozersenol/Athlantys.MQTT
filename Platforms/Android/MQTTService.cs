using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Athlantys.MQTT.Services.IServices;
using MQTTnet.Server;
using MQTTnet;

namespace Athlantys.MQTT.Platforms.Android
{
    public class MQTTService : IMQTTService
    {
        public async Task StartServer()
        {

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            var result = new List<IPAddress>();


            try
            {
                var upAndNotLoopbackNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces().Where(n =>
                    n.NetworkInterfaceType != NetworkInterfaceType.Loopback
                    && n.OperationalStatus == OperationalStatus.Up);

                foreach (var networkInterface in upAndNotLoopbackNetworkInterfaces)
                {
                    var iPInterfaceProperties = networkInterface.GetIPProperties();

                    var unicastIpAddressInformation =
                        iPInterfaceProperties.UnicastAddresses.FirstOrDefault(u =>
                            u.Address.AddressFamily == AddressFamily.InterNetwork);
                    if (unicastIpAddressInformation == null) continue;

                    result.Add(unicastIpAddressInformation.Address);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to find IP: {ex.Message}");
            }


            //if (result.Count() > 0)
            //{
            //     ipAddress = result.FirstOrDefault();

            //}

            Console.Write("IP ADRESS: ");
            Console.WriteLine(ipAddress);

            var options = new MqttServerOptionsBuilder()
                .WithDefaultEndpointPort(1883)
                .WithDefaultEndpointBoundIPAddress(ipAddress)
                .WithDefaultEndpointBoundIPV6Address(IPAddress.None)
                .WithPersistentSessions()
                .WithDefaultEndpointPort(1883)
                .WithKeepAlive()
                .WithoutEncryptedEndpoint()
                .Build();

            var server = new MqttFactory().CreateMqttServer(options);
            try
            {
                await server.StartAsync();
                Console.WriteLine("MQTT SERVER STARTED..............");

                await Task.CompletedTask;
                // await server.StopAsync();
                // Console.WriteLine("MQTT SERVER STOPPED..............");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }


}
