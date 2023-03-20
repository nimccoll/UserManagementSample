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

namespace UserManagement.Web.Models.User
{
    public class AuthorizationInfo
    {
        public List<object> certificateUserIds { get; set; }
    }

    public class Identity
    {
        public string signInType { get; set; }
        public string issuer { get; set; }
        public string issuerAssignedId { get; set; }
    }

    public class OnPremisesExtensionAttributes
    {
        public object extensionAttribute1 { get; set; }
        public object extensionAttribute2 { get; set; }
        public object extensionAttribute3 { get; set; }
        public object extensionAttribute4 { get; set; }
        public object extensionAttribute5 { get; set; }
        public object extensionAttribute6 { get; set; }
        public object extensionAttribute7 { get; set; }
        public object extensionAttribute8 { get; set; }
        public object extensionAttribute9 { get; set; }
        public object extensionAttribute10 { get; set; }
        public object extensionAttribute11 { get; set; }
        public object extensionAttribute12 { get; set; }
        public object extensionAttribute13 { get; set; }
        public object extensionAttribute14 { get; set; }
        public object extensionAttribute15 { get; set; }
    }

    public class UserFullProfile
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        public List<Value> value { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public object deletedDateTime { get; set; }
        public bool accountEnabled { get; set; }
        public object ageGroup { get; set; }
        public List<object> businessPhones { get; set; }
        public object city { get; set; }
        public DateTime createdDateTime { get; set; }
        public string creationType { get; set; }
        public object companyName { get; set; }
        public object consentProvidedForMinor { get; set; }
        public object country { get; set; }
        public object department { get; set; }
        public string displayName { get; set; }
        public object employeeId { get; set; }
        public object employeeHireDate { get; set; }
        public object employeeLeaveDateTime { get; set; }
        public object employeeType { get; set; }
        public object faxNumber { get; set; }
        public string givenName { get; set; }
        public List<object> imAddresses { get; set; }
        public List<object> infoCatalogs { get; set; }
        public object isManagementRestricted { get; set; }
        public object isResourceAccount { get; set; }
        public object jobTitle { get; set; }
        public object legalAgeGroupClassification { get; set; }
        public string mail { get; set; }
        public string mailNickname { get; set; }
        public object mobilePhone { get; set; }
        public object onPremisesDistinguishedName { get; set; }
        public object officeLocation { get; set; }
        public object onPremisesDomainName { get; set; }
        public object onPremisesImmutableId { get; set; }
        public object onPremisesLastSyncDateTime { get; set; }
        public object onPremisesSecurityIdentifier { get; set; }
        public object onPremisesSamAccountName { get; set; }
        public object onPremisesSyncEnabled { get; set; }
        public object onPremisesUserPrincipalName { get; set; }
        public List<string> otherMails { get; set; }
        public object passwordPolicies { get; set; }
        public object postalCode { get; set; }
        public object preferredDataLocation { get; set; }
        public object preferredLanguage { get; set; }
        public List<string> proxyAddresses { get; set; }
        public DateTime refreshTokensValidFromDateTime { get; set; }
        public string securityIdentifier { get; set; }
        public bool showInAddressList { get; set; }
        public DateTime signInSessionsValidFromDateTime { get; set; }
        public object state { get; set; }
        public object streetAddress { get; set; }
        public string surname { get; set; }
        public object usageLocation { get; set; }
        public string userPrincipalName { get; set; }
        public object externalUserConvertedOn { get; set; }
        public string externalUserState { get; set; }
        public DateTime externalUserStateChangeDateTime { get; set; }
        public string userType { get; set; }
        public object employeeOrgData { get; set; }
        public object passwordProfile { get; set; }
        public List<object> assignedLicenses { get; set; }
        public List<object> assignedPlans { get; set; }
        public AuthorizationInfo authorizationInfo { get; set; }
        public List<object> deviceKeys { get; set; }
        public List<Identity> identities { get; set; }
        public OnPremisesExtensionAttributes onPremisesExtensionAttributes { get; set; }
        public List<object> onPremisesProvisioningErrors { get; set; }
        public List<object> provisionedPlans { get; set; }
    }
}