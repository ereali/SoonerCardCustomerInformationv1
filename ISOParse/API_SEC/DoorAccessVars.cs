using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ISOParse.API_SEC
{
    public class DoorAccessVarsRoot
    {
        [JsonProperty("doorAccessPlans")]
        public List<DoorAccessVars> DoorAccessPlans { get; set; }
    }

    public class DoorAccessVars
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("dsrAuthorizationTypeId")]
        public int DsrAuthorizationTypeId { get; set; }

        [JsonProperty("stopDate")]
        public string StopDate { get; set; }

        public DoorAccessVars()
        {
        }
    }
}
