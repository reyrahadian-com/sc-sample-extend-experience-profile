using System;
using System.Data;
using Sitecore.Analytics.Reporting;
using Sitecore.Cintel.Reporting;
using Sitecore.Cintel.Reporting.Processors;
using Sitecore.Cintel.Reporting.ReportingServerDatasource;
using Sitecore.Diagnostics;

namespace Sitecore.TC.ExperienceProfile.CIntel.Reporting.ReportingServerDataSource.DownloadedPdfFiles
{
	public class GetDownloadedFiles : ReportProcessorBase
	{
		private const string ContactidParameter = "@contactid";
		private const string DataSourceName = "collection";

		private readonly QueryBuilder _pageEventsQuery = new QueryBuilder
		{
			collectionName = "Interactions",
			QueryParms =
			{
				{
					"ContactId",
					ContactidParameter
				}
			},
			Fields =
			{
				"ContactId",
				"_id",
				"StartDateTime",
				"EndDateTime",
				"Value",
				"VisitPageCount",
				"Keywords",
				"ReferringSite",
				"ChannelId",
				"TrafficType",
				"CampaignId",
				"ContactVisitIndex",				
				"Pages_PageEvents_PageEventDefinitionId",
				"Pages_PageEvents_DateTime",
				"Pages_PageEvents_Name",
				"Pages_Url_Path",
				"Pages_Url_QueryString",
				"Pages_Item__id",
				"Pages_VisitPageIndex",
				"Pages_PageEvents_ItemId",
				"Pages_PageEvents_Data",
				"Pages_PageEvents_DataKey",
				"GeoData_BusinessName",
				"GeoData_City",
				"GeoData_Region",
				"GeoData_Country",
				"GeoData_PostalCode"
			}
		};

		public override void Process(ReportProcessorArgs args)
		{
			Guid result;
			DataTable contactQueryExpression;
			if (Guid.TryParse(args.ReportParameters.ViewEntityId, out result))
			{
				if (!_pageEventsQuery.QueryParms.ContainsKey("_id"))
					_pageEventsQuery.QueryParms.Add("_id", "@interactionid");
				contactQueryExpression = GetTableFromContactQueryExpression(_pageEventsQuery.Build(),
					args.ReportParameters.ContactId, result);
			}
			else
			{
				if (_pageEventsQuery.QueryParms.ContainsKey("_id"))
					_pageEventsQuery.QueryParms.Remove("_id");
				contactQueryExpression = GetTableFromContactQueryExpression(_pageEventsQuery.Build(),
					args.ReportParameters.ContactId, new Guid?());
			}
			args.QueryResult = contactQueryExpression;
		}

		public DataTable GetInteractions(Guid contactId)
		{
			var reportDataProvider = GetReportDataProvider();
			Assert.IsNotNull(reportDataProvider, "provider should not be null");
			return reportDataProvider.GetData(DataSourceName, new ReportDataQuery(_pageEventsQuery.Build())
			{
				Parameters =
				{
					{
						ContactidParameter,
						contactId
					}
				}
			}, new CachingPolicy
			{
				NoCache = true
			}).GetDataTable();
		}
	}
}