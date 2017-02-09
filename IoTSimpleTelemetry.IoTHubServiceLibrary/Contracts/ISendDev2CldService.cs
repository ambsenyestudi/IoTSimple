using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTSimpleTelemetry.IoTHubServiceLibrary.Contracts
{
    public interface ISendDev2CldService
    {
        Task SendDeviceToCloudMessagesAsync(string date, string message);
        
        void InitalizeMessagingService(string iotHubUri, string deviceKey, string deviceID);
    }
}
