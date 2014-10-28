//-----------------------------------------------------------------------
// <copyright file="MvcApplication.cs" company="U2U Consult NV/SA">
//     Copyright (c) U2U Consult NV/SA. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
        
namespace U2UConsult.AspNetMvcDemo
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {       
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}