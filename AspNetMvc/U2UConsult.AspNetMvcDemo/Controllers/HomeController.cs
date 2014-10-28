//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="U2U Consult NV/SA">
//     Copyright (c) U2U Consult NV/SA. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace U2UConsult.AspNetMvcDemo.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using TheIdentityHub;
    using U2UConsult.AspNetMvcDemo.Models;

    public class HomeController : Controller
    {
        /// <summary>
        /// The identity service.
        /// </summary>
        private IdentityService identityService = new IdentityService("[Your Client Id]", new Uri("[Your Tenant]"));

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var friends = await identityService.GetFriendsAsync();
            var profile = await identityService.GetProfileAsync();
            var accountProviders = await identityService.GetAccountProvidersAsync();

            var model = new IndexModel { Friends = friends, Profile = profile, AccountProviders = accountProviders };

            return View(model);
        }
    }
}