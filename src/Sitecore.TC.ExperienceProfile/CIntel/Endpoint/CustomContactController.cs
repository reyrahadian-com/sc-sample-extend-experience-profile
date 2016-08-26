using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sitecore.Analytics.Tracking;
using Sitecore.Cintel.Commons;
using Sitecore.Cintel.ContactService;
using Sitecore.Cintel.Endpoint.Plumbing;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.TC.ExperienceProfile.ContactFacets;

namespace Sitecore.TC.ExperienceProfile.CIntel.Endpoint
{
	[AuthorizedReportingUserFilter]
	public class CustomContactController : ApiController
	{
		[HttpGet]
		public object GetCustomFields(Guid contactId)
		{
			try
			{
				var contactManager = GetContactManager();
				var contact = contactManager.LoadContactReadOnly(contactId);
				if (contact == null)
				{
					throw new ContactNotFoundException();
				}

				var customFacet = contact.GetFacet<ICustomFieldsFacet>(CustomFieldsFacet.FACET_NAME);
				return customFacet;
			}
			catch (ContactNotFoundException ex)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
			}
		}

		private static ContactManager GetContactManager()
		{
			var contactManager = Factory.CreateObject("tracking/contactManager", true) as ContactManager;
			Assert.IsNotNull(contactManager, "Could not create instance of ContactManager");

			return contactManager;
		}

		[HttpGet]
		public object GetNewletterSubscriptions(Guid contactId)
		{
			try
			{
				var contactManager = GetContactManager();
				var contact = contactManager.LoadContactReadOnly(contactId);
				if (contact == null)
				{
					throw new ContactNotFoundException();
				}

				var customFacet = contact.GetFacet<INewsletterSubscriptionFacet>(NewsletterSubscriptionFacet.FACET_NAME);
				var resultSet = new ResultSet<List<INewsletterElement>>(1, 100);
				if (!customFacet.Newsletters.Any())
				{
					return resultSet;
				}

				resultSet.TotalResultCount = customFacet.Newsletters.Count;
				resultSet.Data.Dataset.Add("newsletters", customFacet.Newsletters.ToList());

				return resultSet;
			}
			catch (ContactNotFoundException ex)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
			}
		}
	}
}