using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoTSimpleTelemetry.IoTHubServiceLibrary.Contracts;
using IoTSimpleTelemetry.IoTHubServiceLibrary.Implementations;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace IoTSimpleTelemetry.IoTHubServiceLibrary.IT
{
    [TestClass]
    public class AllMessagingChainIT
    {
        private ISendDev2CldService _telemetryService;
        private IDeviceManagerService _deviceManagerService;
        private IReadDeviceToCloudMessageService _readDeviceToCloudMessageService;
        private string _connectionString;
        private string _iotHubUri;
        private string _deviceId;
        private string _deviceKey;
        private string _iotHubD2cEndpoint;
        [TestInitialize]
        public void InitializeTest()
        {
            _deviceManagerService = new IOTHubDeviceManagerService();
            _telemetryService = new SendTelemetry2IoTHubService();
            _readDeviceToCloudMessageService = new ReadDeviceToCloudMessageService();
            _connectionString = ConfigurationManager.AppSettings["IotHubConnectionString"].ToString();
            _iotHubUri = ConfigurationManager.AppSettings["IotHubUri"].ToString();
            _deviceId = ConfigurationManager.AppSettings["DeviceId"].ToString();
            _iotHubD2cEndpoint = ConfigurationManager.AppSettings["iotHubD2cEndpoint"].ToString();
            //I this throws an exception review App.config for correct connectionstring
            _deviceManagerService.InitializeDevice(_connectionString);
            _deviceKey = _deviceManagerService.GetDeviceKeyAsync(_deviceId).Result;
            _telemetryService.InitalizeMessagingService(_iotHubUri, _deviceKey, _deviceId);
            _readDeviceToCloudMessageService.InitializeService(_connectionString, _iotHubD2cEndpoint);


        }
        [TestMethod]
        public async Task MessagingReadingIsNotNull_IT()
        {
            string message = "Integration Testing";
            await _telemetryService.SendDeviceToCloudMessagesAsync(DateTime.Now.ToString(), message);
            string partition = _readDeviceToCloudMessageService.Partitions.FirstOrDefault();
            string resultMessage = await _readDeviceToCloudMessageService.ReceiveMessagesFromDeviceAsync(partition, DateTime.Today);
            //Printing it to test output
            System.Diagnostics.Debug.WriteLine(resultMessage);
            Assert.IsNotNull(resultMessage);
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void BadReadingServiceInitzalization_IT()
        {
            _readDeviceToCloudMessageService.InitializeService(null, null);
        }
    }
}
