using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoTSimpleTelemetry.IoTHubServiceLibrary.Contracts;
using IoTSimpleTelemetry.IoTHubServiceLibrary.Implementations;
using System.Configuration;
using System.Threading.Tasks;

namespace IoTSimpleTelemetry.IoTHubServiceLibrary.IT
{
    [TestClass]
    public class SendTelemetry2IoTHubServiceIT
    {
        private ISendDev2CldService _telemetryService;
        private IDeviceManagerService _deviceManagerService;
        string _connectionString;
        string _iotHubUri;
        string _deviceId;
        string _deviceKey;
        [TestInitialize]
        public void InitializeTest()
        {
            _deviceManagerService = new IOTHubDeviceManagerService();           
            _telemetryService = new SendTelemetry2IoTHubService();

            _connectionString = ConfigurationManager.AppSettings["IotHubConnectionString"].ToString();
            _iotHubUri = ConfigurationManager.AppSettings["IotHubUri"].ToString();
            _deviceId = ConfigurationManager.AppSettings["DeviceId"].ToString();

            //I this throws an exception review App.config for correct connectionstring
            _deviceManagerService.InitializeDevice(_connectionString);
            //Running it synchronoulsy
            _deviceKey = _deviceManagerService.GetDeviceKeyAsync(_deviceId).Result;
            _telemetryService.InitalizeMessagingService(_iotHubUri,_deviceKey, _deviceId);
        }
        
        [TestMethod]
        public async Task SendMessage_IT()
        {
            string message = "Integration Testing";
            _telemetryService.SendDeviceToCloudMessagesAsync(DateTime.Now.ToString(), message).Wait();
            //Simply waiting for the task to end
            bool isSuccess = true; 
            Assert.IsTrue(isSuccess);
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void MessangingService_BadUri_IT()
        {
            _telemetryService.InitalizeMessagingService(null, null, null);
        }

        
    }
}
