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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System.Diagnostics;
using UserManagement.Web.Models;
using UserManagement.Web.Models.Group;
using UserManagement.Web.Models.Invitation;
using UserManagement.Web.Models.License;
using UserManagement.Web.Models.User;

namespace UserManagement.Web.Controllers
{
    [AuthorizeForScopes(ScopeKeySection = "MicrosoftGraph:Scopes")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ITokenAcquisition tokenAcquistion)
        {
            _logger = logger;
            _configuration = configuration;
            _tokenAcquisition = tokenAcquistion;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult InviteUser()
        {
            return View();
        }

        [HttpPost]
        [ActionName("InviteUser")]
        public async Task<IActionResult> InviteUserPost(InvitationRequest model)
        {
            if (!ModelState.IsValid)
            {
                string graphResult = string.Empty;
                string accessToken = await GetGraphAccessToken();
                string request = "https://graph.microsoft.com/v1.0/invitations";
                HttpContent content = JsonContent.Create<InvitationRequest>(model, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                try
                {
                    // Call MS Graph Invitation API
                    graphResult = await CallGraph(accessToken, request, HttpMethod.Post, content);
                    ViewBag.Message = "User invited successfully";
                    ViewBag.Alert = "alert-success";
                }
                catch (ApplicationException)
                {
                    ViewBag.Message = "User invitation failed. See logs for more details";
                    ViewBag.Alert = "alert-danger";
                }
            }

            return View("InviteUser");
        }

        [HttpGet]
        public async Task<IActionResult> AssignUserToGroup()
        {
            List<Group> ownedGroups = new List<Group>();
            string graphResult = string.Empty;
            string accessToken = await GetGraphAccessToken();
            string request = "https://graph.microsoft.com/v1.0/me/memberOf";
            try
            {
                // Call MS Graph to get group membership of the current user
                graphResult = await CallGraph(accessToken, request, HttpMethod.Get);
                Groups groups = JsonConvert.DeserializeObject<Groups>(graphResult);
                foreach (Group group in groups.value)
                {
                    if (group.odatatype == "#microsoft.graph.group")
                    {
                        request = $"https://graph.microsoft.com/v1.0/groups/{group.id}/owners";
                        try
                        {
                            // Call MS Graph to get owners for each group the current user belongs to
                            graphResult = await CallGraph(accessToken, request, HttpMethod.Get);
                            Owners owners = JsonConvert.DeserializeObject<Owners>(graphResult);
                            foreach (Owner owner in owners.value)
                            {
                                // Only include groups where the current user is the owner
                                if (owner.userPrincipalName == User.Identity.Name)
                                {
                                    ownedGroups.Add(group);
                                }
                            }
                        }
                        catch (ApplicationException)
                        {
                            ViewBag.Message = "Retrieving group owners failed. See logs for more details";
                            ViewBag.Alert = "alert-danger";
                        }
                    }
                }
            }
            catch (ApplicationException)
            {
                ViewBag.Message = "Retrieving group membership failed. See logs for more details";
                ViewBag.Alert = "alert-danger";
            }

            return View(ownedGroups);
        }

        [HttpPost]
        [ActionName("AssignUserToGroup")]
        public async Task<IActionResult> AssignUserToGroupPost()
        {
            string graphResult = string.Empty;
            // Retrieve specified user's full profile by email address
            string emailAddress = Request.Form["txtEmailAddress"][0];
            string accessToken = await GetGraphAccessToken();
            UserFullProfile user = await GetUserByEmailAddress(emailAddress);
            // Make sure the specified user has accepted their invitation
            if (user != null && user.value[0].externalUserState == "Accepted")
            {
                foreach (string groupId in Request.Form["chkGroup"])
                {
                    string request = $"https://graph.microsoft.com/v1.0/groups/{groupId}/members/$ref";
                    string content = "{\"@odata.id\": \"https://graph.microsoft.com/v1.0/directoryObjects/" + user.value[0].id + "\"}";
                    HttpContent httpContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
                    try
                    {
                        // Call MS Graph to assign user to group
                        graphResult = await CallGraph(accessToken, request, HttpMethod.Post, httpContent);
                        ViewBag.Message = "User added to group";
                        ViewBag.Alert = "alert-success";
                    }
                    catch (ApplicationException)
                    {
                        ViewBag.Message = "Add user to group failed. See logs for more details";
                        ViewBag.Alert = "alert-danger";
                    }
                }
            }
            else
            {
                ViewBag.Message = "User not found or user has not accepted invitation";
                ViewBag.Alert = "alert-danger";
            }

            List<Group> model = new List<Group>();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AssignLicenseToUser()
        {
            List<License> licensePlans = new List<License>();
            string graphResult = string.Empty;
            string accessToken = await GetGraphAccessToken();
            string request = "https://graph.microsoft.com/v1.0/subscribedSkus";
            try
            {
                // Call MS Graph to get license plans for this tenant
                graphResult = await CallGraph(accessToken, request, HttpMethod.Get);
                Licenses licenses = JsonConvert.DeserializeObject<Licenses>(graphResult);
                foreach (License license in licenses.value)
                {
                    licensePlans.Add(license);
                }
            }
            catch (ApplicationException)
            {
                ViewBag.Message = "Retrieve license plans failed. See logs for more details";
                ViewBag.Alert = "alert-danger";
            }

            return View(licensePlans);
        }

        [HttpPost]
        [ActionName("AssignLicenseToUser")]
        public async Task<IActionResult> AssignLicenseToUserPost()
        {
            string graphResult = string.Empty;
            // Retrieve specified user's full profile by email address
            string emailAddress = Request.Form["txtEmailAddress"][0];
            string accessToken = await GetGraphAccessToken();
            UserFullProfile user = await GetUserByEmailAddress(emailAddress);
            // Make sure the specified user has accepted their invitation
            if (user != null && user.value[0].externalUserState == "Accepted")
            {
                AssignLicense assignLicense = new AssignLicense();
                assignLicense.addLicenses = new List<AddLicense>();
                assignLicense.removeLicenses = new List<string>();
                foreach (string skuId in Request.Form["chkLicense"])
                {
                    AddLicense addLicense = new AddLicense();
                    addLicense.disabledPlans = new List<string>();
                    addLicense.skuId = skuId;
                    assignLicense.addLicenses.Add(addLicense);
                }
                string request = $"https://graph.microsoft.com/v1.0/users/{user.value[0].id}/assignLicense";
                HttpContent content = JsonContent.Create<AssignLicense>(assignLicense, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                try
                {
                    // Call MS Graph to assign the license(s) to the user
                    graphResult = await CallGraph(accessToken, request, HttpMethod.Post, content);
                    ViewBag.Message = "License assigned to user";
                    ViewBag.Alert = "alert-success";
                }
                catch (ApplicationException)
                {
                    ViewBag.Message = "Assign license to user failed. See logs for more details";
                    ViewBag.Alert = "alert-danger";
                }
            }
            else
            {
                ViewBag.Message = "User not found or user has not accepted invitation";
                ViewBag.Alert = "alert-danger";
            }

            List<License> model = new List<License>();

            return View(model);
        }

        private async Task<UserFullProfile> GetUserByEmailAddress(string emailAddress)
        {
            UserFullProfile user = null;
            string accessToken = await GetGraphAccessToken();
            string request = $"https://graph.microsoft.com/beta/users?$filter=mail eq '{emailAddress}'";
            try
            {
                string graphResult = await CallGraph(accessToken, request, HttpMethod.Get);
                user = JsonConvert.DeserializeObject<UserFullProfile>(graphResult);
            }
            catch (ApplicationException)
            {

            }

            return user;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<string> CallGraph(string accessToken, string request, HttpMethod httpMethod, HttpContent? content = null)
        {
            string graphResult = string.Empty;

            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = null;
                if (httpMethod == HttpMethod.Get)
                {
                    response = await _httpClient.GetAsync(request);
                }
                else if (httpMethod == HttpMethod.Post)
                {
                    response = await _httpClient.PostAsync(request, content);
                }

                graphResult = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Call to the Graph API failed with the following error {graphResult}", graphResult);
                    throw new ApplicationException("Microsoft Graph call failed. See logs for more details.");
                }
            }
            return graphResult;
        }

        private async Task<string> GetGraphAccessToken()
        {
            // Get access token for the Microsoft Graph from the cache
            string accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new List<string>() { _configuration.GetValue<string>("MicrosoftGraph:Scopes") });

            return accessToken;
        }
    }
}