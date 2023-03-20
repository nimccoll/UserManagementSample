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
namespace UserManagement.Web.Models.License
{
    public class AddLicense
    {
        public List<string> disabledPlans { get; set; }
        public string skuId { get; set; }
    }

    public class AssignLicense
    {
        public List<AddLicense> addLicenses { get; set; }
        public List<string> removeLicenses { get; set; }
    }
}