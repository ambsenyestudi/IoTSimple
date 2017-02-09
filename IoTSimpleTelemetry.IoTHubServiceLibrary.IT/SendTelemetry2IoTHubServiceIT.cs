using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoTSimpleTelemetry.IoTHubServiceLibrary.Contracts;
using IoTSimpleTelemetry.IoTHubServiceLibrary.Implementations;
using System.Configuration;

namespace IoTSimpleTelemetry.IoTHubServiceLibrary.IT
{
    [TestClass]
    public class SendTelemetry2IoTHubServiceIT
    {
        private ISendDev2CldService _telemetryService;
        string _connectionString;
        string _iotHubUri;
        string _deviceId;
        string _deviceKey;
        [TestInitialize]
        public void InitializeTest()
        {
            _telemetryService = new SendTelemetry2IoTHubService();

            _connectionString = ConfigurationManager.AppSettings["IotHubConnectionString"].ToString();
            _iotHubUri = ConfigurationManager.AppSettings["IotHubUri"].ToString();
            _deviceId = ConfigurationManager.AppSettings["DeviceId"].ToString();
            //ToDo Get Device Key
            
            //I this throws an exception review App.config for correct connectionstring
            //_telemetryService.InitalizeMessagingService(_iotHubUri, _deviceId,_deviceKey);


        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void MessangingService_BadUri_IT()
        {
            _telemetryService.InitalizeMessagingService(null, null, null);
        }
    }
}
