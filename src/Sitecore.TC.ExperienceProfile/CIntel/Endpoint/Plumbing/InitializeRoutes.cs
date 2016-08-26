using System.Web.Http;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace Sitecore.TC.ExperienceProfile.CIntel.Endpoint.Plumbing
{
	public class InitializeRoutes
	{
		public virtual void Process(PipelineArgs args)
		{
			RegisterRoutes(RouteTable.Routes, args);
		}

		protected virtual void RegisterRoutes(RouteCollection routes, PipelineArgs args)
		{
			routes.MapHttpRoute("tc_customcontact_customfields", "sitecore/api/ao/v1/contacts/{contactId}/customfields", (object)new
			{
				controller = "CustomContact",
				action = "GetCustomFields"
			});
			routes.MapHttpRoute("tc_customcontact_newslettersubscriptions", "sitecore/api/ao/v1/contacts/{contactId}/newslettersubscriptions", (object)new
			{
				controller = "CustomContact",
				action = "GetNewletterSubscriptions"
			});
		}
	}
}