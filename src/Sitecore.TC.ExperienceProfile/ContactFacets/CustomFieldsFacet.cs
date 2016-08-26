using System;
using Sitecore.Analytics.Model.Framework;

namespace Sitecore.TC.ExperienceProfile.ContactFacets
{
	[Serializable]
	public class CustomFieldsFacet : Facet, ICustomFieldsFacet
	{
		public const string FACET_NAME = "CustomFields";
		private const string HOSPITAL_NAME = "HospitalName";
		private const string PROFESSION_NAME = "ProfessionName";		

		public CustomFieldsFacet()
		{
			EnsureAttribute<string>(HOSPITAL_NAME);
			EnsureAttribute<string>(PROFESSION_NAME);			
		}

		public string HospitalName
		{
			get { return GetAttribute<string>(HOSPITAL_NAME); }
			set { SetAttribute(HOSPITAL_NAME, value); }
		}

		public string ProfessionName
		{
			get { return GetAttribute<string>(PROFESSION_NAME); }
			set { SetAttribute(PROFESSION_NAME, value); }
		}		
	}
}