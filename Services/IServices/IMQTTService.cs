using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athlantys.MQTT.Services.IServices
{
    public interface IMQTTService
    {
        Task StartServer();
    }
}
