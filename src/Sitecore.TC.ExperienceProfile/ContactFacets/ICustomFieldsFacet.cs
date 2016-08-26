using Sitecore.Analytics.Model.Framework;

namespace Sitecore.TC.ExperienceProfile.ContactFacets
{
	public interface ICustomFieldsFacet : IFacet
	{
		string HospitalName { get; set; }
		string ProfessionName { get; set; }		
	}
}