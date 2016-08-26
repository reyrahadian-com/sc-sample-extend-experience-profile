using System;
using Sitecore.Cintel.Reporting;

namespace Sitecore.TC.ExperienceProfile.CIntel.Reporting.Contact.DownloadedPdfFiles
{
	public static class Schema
	{
		public static string ViewName = "DownloadedPdfFiles";		
		public static ViewField<string> FileName = new ViewField<string>("FileName");
		public static ViewField<string> FileItemPath = new ViewField<string>("FileItemPath");
		public static ViewField<string> PageName = new ViewField<string>("PageName");
		public static ViewField<string> PageItemPath = new ViewField<string>("PageItemPath");
		public static ViewField<DateTime> DownloadedOn = new ViewField<DateTime>("DownloadedOn");
	}
}