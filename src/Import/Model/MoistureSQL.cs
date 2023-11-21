using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telemetry_api.Import.Model
{
    public class MoistureSQL
    {
        public string Id { get; set; }
        public string DeviceId { get; set; }
        public int Count { get; set; }
        public float Moist { get; set; }
        public DateTime DateTime { get; set; }
        public float? Battery { get; set; }
        public MoistureSQL() 
        { 
        }
        public MoistureSQL(WiredMoisture obj)
        {
            Id = obj.Id;
            DeviceId = obj.DeviceId;
            Count = obj.Count;
            Moist = obj.Moist;
            DateTime = DateTime.Now;
        }
        public MoistureSQL(LoraMoisture obj)
        {
            Id = obj.Id;
            DeviceId = obj.DeviceId;
            Count = obj.Count;
            Moist = obj.Moist;
            DateTime = DateTime.Now;
            Battery = obj.Battery;
        }

    }
}
