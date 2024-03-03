using System.Web.Mvc;

namespace Project_63131631.Areas.Administrator
{
	public class AdministratorAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Administrator";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Administrator_Default",
				"Administrator/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}