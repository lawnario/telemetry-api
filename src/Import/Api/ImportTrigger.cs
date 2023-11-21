using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using telemetry_api.Import.Model;
using telemetry_api.Import.Service;

namespace telemetry_api.Import.Api
{
    public class ImportTrigger
    {
        private IImportService _service;
        public ImportTrigger(IImportService service)
        {
            _service = service;
        }

        [FunctionName("ImportTrigger")]
        public async Task Run([EventHubTrigger("evh-telemetry-dev-eastus2", Connection = "IOT_EVENTHUB_CONNECTION_STRING")] EventData[] events, ILogger log)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    var messageData = eventData.EventBody.ToArray();
                    string messageBody = Encoding.UTF8.GetString(messageData, 0, messageData.Count());
                    log.LogInformation($"C# Event Hub trigger function processed a message: {messageBody}");

                    if (eventData.Properties["deviceType"].ToString() =="wired")
                    {
                        WiredMoistureEvent objEvent = JsonConvert.DeserializeObject<WiredMoistureEvent>(messageBody);
                        WiredMoisture obj = new WiredMoisture()
                        {
                            Id = Guid.NewGuid().ToString(),
                            DeviceId = eventData.SystemProperties["iothub-connection-device-id"].ToString(),
                            Moist = float.Parse(objEvent.Moist),
                            Count = int.Parse(objEvent.Count)
                        };
                        await _service.UploadTelemetry(new MoistureSQL(obj));
                    }
                    else if (eventData.Properties["deviceType"].ToString() == "lora")
                    {
                        LoraMoistureEvent objEvent = JsonConvert.DeserializeObject<LoraMoistureEvent>(messageBody);
                        LoraMoisture obj = new LoraMoisture()
                        {
                            Id = Guid.NewGuid().ToString(),
                            DeviceId = eventData.SystemProperties["iothub-connection-device-id"].ToString(),
                            Moist = float.Parse(objEvent.Moist),
                            Count = int.Parse(objEvent.Count),
                            Battery = float.Parse(objEvent.Battery)
                        };
                        await _service.UploadTelemetry(new MoistureSQL(obj));
                    }
                        await Task.Yield();
                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
                    exceptions.Add(e);
                }
            }

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }
    }
}
