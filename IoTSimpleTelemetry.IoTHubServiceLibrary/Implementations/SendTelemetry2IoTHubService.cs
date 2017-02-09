using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTSimpleTelemetry.IoTHubServiceLibrary.Contracts;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
/// <summary>
/// Needs to intsatll Nuget Microsoft.Azure.Devices.Client
/// </summary>
namespace IoTSimpleTelemetry.IoTHubServiceLibrary.Implementations
{
    public class SendTelemetry2IoTHubService : ISendDev2CldService
    {
        private DeviceClient _deviceClient;
       
        private string _iotHubUri;
        private string _deviceKey;
        private string _deviceID;
        public Task SendDeviceToCloudMessagesAsync(string date, string message)
        {
            var telemetry = new
            {
                deviceID = _deviceID,
                date = date,
                message = message
            };
            //This is for teaching purpuses, The seralization would have to happen in a separate service and have a model
            string json = JsonConvert.SerializeObject(telemetry);
            var cloudMessage = new Message(Encoding.ASCII.GetBytes(json));
            return _deviceClient.SendEventAsync(cloudMessage);
        }

        public void InitalizeMessagingService(string iotHubUri, string deviceKey, string deviceID)
        {
            if(!string.IsNullOrWhiteSpace(iotHubUri) && !string.IsNullOrWhiteSpace(deviceKey)&& !string.IsNullOrWhiteSpace(deviceID))
            {
                _iotHubUri = iotHubUri;
                _deviceKey = deviceKey;
                _deviceID = deviceID;
            }
            else
            {
                throw new ArgumentNullException("InitalizeMessagingService some parameter is not right [iotHubUri,deviceKey,deviceID]");
            }
            _deviceClient = DeviceClient.Create(_iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(_deviceID, _deviceKey), TransportType.Mqtt);
        }
    }
}
