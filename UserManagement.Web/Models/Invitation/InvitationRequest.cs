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
using Microsoft.Graph;
using Newtonsoft.Json;

namespace UserManagement.Web.Models.Invitation
{
    public class InvitedUser
    {
        [JsonProperty("@odata.type")]
        public string odatatype { get; set; }
        public string id { get; set; }
    }
    public class CcRecipient
    {
        [JsonProperty("@odata.type")]
        public string odatatype { get; set; }
        public EmailAddress emailAddress { get; set; }
    }

    public class InvitedUserMessageInfo
    {
        [JsonProperty("@odata.type")]
        public string odatatype { get; set; }
        public List<CcRecipient> ccRecipients { get; set; }
        public string customizedMessageBody { get; set; }
        public string messageLanguage { get; set; }
    }

    public class InvitationRequest
    {
        public string invitedUserDisplayName { get; set; }
        public string invitedUserEmailAddress { get; set; }
        public InvitedUserMessageInfo invitedUserMessageInfo { get; set; }
        public bool sendInvitationMessage { get; set; }
        public string inviteRedirectUrl { get; set; }
        public string inviteRedeemUrl { get; set; }
        public bool resetRedemption { get; set; }
        public string status { get; set; }
        public InvitedUser invitedUser { get; set; }
        public string invitedUserType { get; set; }
    }
}