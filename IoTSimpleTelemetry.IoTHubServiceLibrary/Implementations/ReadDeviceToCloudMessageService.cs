using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using IoTSimpleTelemetry.IoTHubServiceLibrary.Contracts;
/// <summary>
/// Microsoft.ServiceBus
/// </summary>
namespace IoTSimpleTelemetry.IoTHubServiceLibrary.Implementations
{
    public class ReadDeviceToCloudMessageService : IReadDeviceToCloudMessageService
    {
        private string _connectionString;
        private string _iotHubD2cEventsEndpoint;
        List<string> _partitions;
        EventHubClient _eventHubClient;
        public List<string> Partitions
        {
            get
            {
                return _partitions;
            }
        }
        public void InitializeService(string connectinoString, string iotHubD2cEventsEndpoint)
        {
            _connectionString = connectinoString;
            _iotHubD2cEventsEndpoint = iotHubD2cEventsEndpoint;
            _eventHubClient = EventHubClient.CreateFromConnectionString(_connectionString, _iotHubD2cEventsEndpoint);
            _partitions = _eventHubClient.GetRuntimeInformation().PartitionIds.ToList();
        }

        public async Task<string> ReceiveMessagesFromDeviceAsync(string partition, DateTime startFrom)
        {
            var eventHubReceiver = _eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, startFrom);

            //Solo lee el ultimo mensaje
            EventData eventData = await eventHubReceiver.ReceiveAsync();
            string data = Encoding.UTF8.GetString(eventData.GetBytes());
            string resultMessage = string.Format("Message received. Partition: {0} Data: '{1}'", partition, data);
            return resultMessage;

        }
    }
}
