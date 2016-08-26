using System;
using System.Data;
using System.Globalization;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;

namespace Sitecore.TC.ExperienceProfile.CIntel.Reporting.Contact.DownloadedPdfFiles
{
	/// <summary>
	/// Responsible to populate the result table with the data coming from xDB
	/// </summary>
	public class PopulateDownloadedPdfFilesWithXdbData : ReportProcessorBase
	{
		private readonly Guid _downloadPdfFilePageEventItemId = new Guid("{C7C9427A-0EC3-4DBB-BF80-6864489102AC}");

		public override void Process(ReportProcessorArgs args)
		{
			var queryResult = args.QueryResult;
			var resultTableForView = args.ResultTableForView;

			foreach (var sourceRow in queryResult.AsEnumerable())
				if (!RowShouldBeSkipped(sourceRow))
				{
					var dataRow = resultTableForView.NewRow();

					var mediaPath = sourceRow.Field<string>("Pages_PageEvents_DataKey");
					var mediaItem = Context.Database.GetItem(mediaPath);
					if (mediaItem != null)
					{
						dataRow.SetField(Schema.FileName.Name, mediaItem.DisplayName);
						dataRow.SetField(Schema.FileItemPath.Name, mediaPath);
					}

					var pageItemId = sourceRow.Field<Guid>("Pages_Item__id");
					var pageItem = GetItemFromCurrentContext(pageItemId);
					if (pageItem != null)
					{
						dataRow.SetField(Schema.PageName.Name, pageItem.DisplayName);
						dataRow.SetField(Schema.PageItemPath.Name, pageItem.Paths.FullPath);
					}
					var datetime = sourceRow.Field<DateTime>("Pages_PageEvents_DateTime").ToLocalTime();
					dataRow.SetField(Schema.DownloadedOn.Name, datetime.ToString(CultureInfo.CurrentUICulture));

					resultTableForView.Rows.Add(dataRow);
				}
		}

		/// <summary>
		/// Skip processing the current row if it's the page event that we want to process
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		private bool RowShouldBeSkipped(DataRow row)
		{
			return row.Field<Guid?>("Pages_PageEvents_PageEventDefinitionId").GetValueOrDefault() !=
			       _downloadPdfFilePageEventItemId;
		}
	}
}