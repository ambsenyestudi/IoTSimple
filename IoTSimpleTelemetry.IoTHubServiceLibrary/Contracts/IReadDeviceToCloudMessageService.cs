using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTSimpleTelemetry.IoTHubServiceLibrary.Contracts
{
    public interface IReadDeviceToCloudMessageService
    {
        List<string> Partitions { get; }
        void InitializeService(string connectinoString, string iotHubD2cEventsEndpoint);
        Task<string> ReceiveMessagesFromDeviceAsync(string partition, DateTime startFrom);
    }
}
