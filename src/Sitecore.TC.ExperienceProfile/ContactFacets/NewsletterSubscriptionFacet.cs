using System;
using Sitecore.Analytics.Model.Framework;

namespace Sitecore.TC.ExperienceProfile.ContactFacets
{
	[Serializable]
	public class NewsletterSubscriptionFacet : Facet, INewsletterSubscriptionFacet
	{
		public const string FACET_NAME = "NewsletterSubscriptions";
		private const string NEWSLETTERS = "Newsletters";
		public NewsletterSubscriptionFacet()
		{
			EnsureCollection<INewsletterElement>(NEWSLETTERS);
		}

		public IElementCollection<INewsletterElement> Newsletters
		{
			get { return GetCollection<INewsletterElement>(NEWSLETTERS); }
		}
	}
}