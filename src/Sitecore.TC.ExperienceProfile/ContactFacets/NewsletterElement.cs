using System;
using Sitecore.Analytics.Model.Framework;

namespace Sitecore.TC.ExperienceProfile.ContactFacets
{
	[Serializable]
	public class NewsletterElement : Element, INewsletterElement
	{
		private const string NEWSLETTER_NAME = "NewsletterName";
		public NewsletterElement()
		{
			EnsureAttribute<string>(NEWSLETTER_NAME);
		}

		public string NewsletterName
		{
			get { return GetAttribute<string>(NEWSLETTER_NAME); }
			set { SetAttribute(NEWSLETTER_NAME, value); }
		}
	}
}