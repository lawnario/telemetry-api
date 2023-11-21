using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telemetry_api.Import.Model
{
    public class LoraMoistureEvent
    {
        [JsonProperty(PropertyName = "msgCount")]
        public string Count { get; set; }
        [JsonProperty(PropertyName = "moist")]
        public string Moist { get; set; }
        [JsonProperty(PropertyName = "sensor")]
        public string Sensor { get; set; }
        [JsonProperty(PropertyName = "battery")]
        public string Battery { get; set; }
    }
}
