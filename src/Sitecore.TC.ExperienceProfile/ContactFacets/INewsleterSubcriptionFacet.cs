using Sitecore.Analytics.Model.Framework;

namespace Sitecore.TC.ExperienceProfile.ContactFacets
{
	public interface INewsletterSubscriptionFacet : IFacet
	{
		IElementCollection<INewsletterElement> Newsletters { get; }
	}
}