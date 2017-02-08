using IoTSimpleTelemetry.IoTHubServiceLibrary.Contracts;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTSimpleTelemetry.IoTHubServiceLibrary.Implementations
{
    public class IOTHubDeviceManagerService : IDeviceManagerService
    {
        private RegistryManager registryManager;
        private string _connectionString;
        private string _deviceId;

        /// <summary>
        /// it returns de devices primary key
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetDeviceKeyAsync(string deviceId)
        {
            string newKey = null;
            if(!string.IsNullOrWhiteSpace(deviceId))
            {
                _deviceId = deviceId;
                Device device;
                try
                {
                    device = await registryManager.AddDeviceAsync(new Device(deviceId));
                }
                catch (DeviceAlreadyExistsException)
                {
                    device = await registryManager.GetDeviceAsync(deviceId);
                }
                newKey = device.Authentication.SymmetricKey.PrimaryKey;
            }

            return newKey;
        }

        public void InitializeDevice(string connectionString)
        {
            if(string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("connectionString was null or white space when intializing IotHub Device");
            }
            _connectionString = connectionString;
            registryManager = RegistryManager.CreateFromConnectionString(_connectionString);
        }
    }
}
