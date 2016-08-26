using System.Data;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;

namespace Sitecore.TC.ExperienceProfile.CIntel.Reporting.Contact.DownloadedPdfFiles
{
	public class ConstructDownloadedPdfFilesDataTable : ReportProcessorBase
	{
		public override void Process(ReportProcessorArgs args)
		{
			args.ResultTableForView = new DataTable();
			args.ResultTableForView.Columns.Add(Schema.FileName.ToColumn());
			args.ResultTableForView.Columns.Add(Schema.FileItemPath.ToColumn());
			args.ResultTableForView.Columns.Add(Schema.PageName.ToColumn());
			args.ResultTableForView.Columns.Add(Schema.PageItemPath.ToColumn());
			args.ResultTableForView.Columns.Add(Schema.DownloadedOn.ToColumn());
		}
	}
}