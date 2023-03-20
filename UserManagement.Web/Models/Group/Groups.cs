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
    public class Groups
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        public List<Group> value { get; set; }
    }

    public class Group
    {
        [JsonProperty("@odata.type")]
        public string odatatype { get; set; }
        public string id { get; set; }
        public object deletedDateTime { get; set; }
        public object classification { get; set; }
        public DateTime createdDateTime { get; set; }
        public List<object> creationOptions { get; set; }
        public string description { get; set; }
        public string displayName { get; set; }
        public object expirationDateTime { get; set; }
        public List<object> groupTypes { get; set; }
        public object isAssignableToRole { get; set; }
        public object mail { get; set; }
        public bool mailEnabled { get; set; }
        public string mailNickname { get; set; }
        public object membershipRule { get; set; }
        public object membershipRuleProcessingState { get; set; }
        public object onPremisesDomainName { get; set; }
        public object onPremisesLastSyncDateTime { get; set; }
        public object onPremisesNetBiosName { get; set; }
        public object onPremisesSamAccountName { get; set; }
        public object onPremisesSecurityIdentifier { get; set; }
        public object onPremisesSyncEnabled { get; set; }
        public object preferredDataLocation { get; set; }
        public object preferredLanguage { get; set; }
        public List<object> proxyAddresses { get; set; }
        public DateTime renewedDateTime { get; set; }
        public List<object> resourceBehaviorOptions { get; set; }
        public List<object> resourceProvisioningOptions { get; set; }
        public bool securityEnabled { get; set; }
        public string securityIdentifier { get; set; }
        public object theme { get; set; }
        public object visibility { get; set; }
        public List<object> onPremisesProvisioningErrors { get; set; }
    }
}