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

namespace UserManagement.Web.Models.Group
{
    public class Owners
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        public List<Owner> value { get; set; }
    }

    public class Owner
    {
        [JsonProperty("@odata.type")]
        public string odatatype { get; set; }
        public string id { get; set; }
        public List<object> businessPhones { get; set; }
        public string displayName { get; set; }
        public string givenName { get; set; }
        public object jobTitle { get; set; }
        public object mail { get; set; }
        public string mobilePhone { get; set; }
        public string officeLocation { get; set; }
        public object preferredLanguage { get; set; }
        public string surname { get; set; }
        public string userPrincipalName { get; set; }
    }
}