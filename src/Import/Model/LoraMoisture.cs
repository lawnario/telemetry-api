using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telemetry_api.Import.Model
{
    public class LoraMoisture
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName ="deviceId")]
        public string DeviceId { get; set; }
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
        [JsonProperty(PropertyName = "moist")]
        public float Moist { get; set; }
        [JsonProperty(PropertyName = "battery")]
        public float Battery { get; set; }

    }
}
