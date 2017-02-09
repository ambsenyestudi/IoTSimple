using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoTSimpleTelemetry.IoTHubServiceLibrary.Contracts;
using IoTSimpleTelemetry.IoTHubServiceLibrary.Implementations;
using System.Configuration;
/*need to add assembly refrence to System.Configuration*/
namespace IoTSimpleTelemetry.IoTHubServiceLibrary.IT
{
    
    [TestClass]
    public class DeviceManagerServiceIT
    {
        private IDeviceManagerService _deviceManagerService;
        string _connectionString;
        [TestInitialize]
        public void InitializeTest()
        {
            _deviceManagerService = new IOTHubDeviceManagerService();

            _connectionString = ConfigurationManager.AppSettings["IotHubConnectionString"].ToString();
            //I this throws an exception review App.config for correct connectionstring
            _deviceManagerService.InitializeDevice(_connectionString);


        }
        [TestMethod]
        public void NullDeviceID_TestMethod()
        {
            string deviceKey=_deviceManagerService.GetDeviceKeyAsync(null).Result;
            Assert.IsNull(deviceKey);
        }
        [TestMethod]
        public void EmptyDeviceID_TestMethod()
        {
            string deviceKey = _deviceManagerService.GetDeviceKeyAsync(string.Empty).Result;
            Assert.IsNull(deviceKey);
        }
        [TestMethod]
        public void WhiteSpaceDeviceID_TestMethod()
        {
            string deviceKey = _deviceManagerService.GetDeviceKeyAsync(" ").Result;
            Assert.IsNull(deviceKey);
        }
        [TestMethod]
        public void NotNullDeviceKey_TestMethod()
        {
            string deviceKey = _deviceManagerService.GetDeviceKeyAsync(_deviceId).Result;
            Assert.IsNotNull(deviceKey);
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void NullURi_TestMethod()
        {
            _deviceManagerService.InitializeDevice(null);
        }

    }
}
