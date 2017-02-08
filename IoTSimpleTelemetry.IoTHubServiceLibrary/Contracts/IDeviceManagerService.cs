using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTSimpleTelemetry.IoTHubServiceLibrary.Contracts
{
    public interface IDeviceManagerService
    {
        Task<string> GetDeviceKeyAsync(string deviceId);
        void InitializeDevice(string connectionString);
    }
}
