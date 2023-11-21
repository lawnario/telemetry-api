using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telemetry_api.Import.Service
{
    public interface ITelemetryRepo
    {
        public CosmosClient Client { get; }
        public Container TelemetryContainer { get; }
    }
    public class TelemetryRepo : ITelemetryRepo
    {
        public CosmosClient Client { get; }
        public Container TelemetryContainer { get; }
        private IConfigurationRoot _config;

        public TelemetryRepo(IConfigurationRoot config)
        {
            _config = config;
#if DEBUG
            Client = new CosmosClient(connectionString: config["TELEMETRY_DB_ENDPOINT"]);
#else
            Client = new CosmosClient(connectionString: config["TELEMETRY_DB_ENDPOINT"]);
#endif
            TelemetryContainer = Client.GetContainer("Telemetry", "Moisture");
        }
    }
}
