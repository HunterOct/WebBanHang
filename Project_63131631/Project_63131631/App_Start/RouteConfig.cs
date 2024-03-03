using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project_63131631
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
			name: "Default",
			url: "{controller}/{action}/{id}",
			defaults: new { controller = "Home_63131631", action = "Index", id = UrlParameter.Optional },
			namespaces: new string[] { "Project_63131631.Controllers" }
			);

			routes.MapRoute(
			name: "admin",
			url: "{controller}/{action}/{id}",
			defaults: new { controller = "Home_63131631", action = "Index", id = UrlParameter.Optional },
			namespaces: new string[] { "Project_63131631.Controllers" }
		).DataTokens["area"] = "Administrator";
		}
	}
}
