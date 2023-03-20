//===============================================================================
// Microsoft FastTrack for Azure
// User Management Example
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
using Newtonsoft.Json;

namespace UserManagement.Web.Models.License
{
    public class PrepaidUnits
    {
        public int enabled { get; set; }
        public int suspended { get; set; }
        public int warning { get; set; }
    }

    public class Licenses
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        public List<License> value { get; set; }
    }

    public class ServicePlan
    {
        public string servicePlanId { get; set; }
        public string servicePlanName { get; set; }
        public string provisioningStatus { get; set; }
        public string appliesTo { get; set; }
    }

    public class License
    {
        public string capabilityStatus { get; set; }
        public int consumedUnits { get; set; }
        public string id { get; set; }
        public PrepaidUnits prepaidUnits { get; set; }
        public List<ServicePlan> servicePlans { get; set; }
        public string skuId { get; set; }
        public string skuPartNumber { get; set; }
        public string appliesTo { get; set; }
    }

}