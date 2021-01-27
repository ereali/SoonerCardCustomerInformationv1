using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ISOParse.API_SEC
{
    public class DoorAccessCustVars
    {
        [JsonProperty("doorAccessPlans")]
        public List<int> DoorAccessPlans { get; set; }
    }
}
