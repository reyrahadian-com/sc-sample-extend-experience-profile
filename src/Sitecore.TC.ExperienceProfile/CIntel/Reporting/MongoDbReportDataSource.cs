using System;
using System.Configuration;
using System.Data;
using MongoDB.Driver;
using Sitecore.Analytics.Data;
using Sitecore.Analytics.Reporting;
using Sitecore.Diagnostics;

namespace Sitecore.TC.ExperienceProfile.CIntel.Reporting
{
	public class MongoDbReportDataSource : Analytics.Reporting.MongoDbReportDataSource
	{
		private readonly MongoDatabase database;

		public MongoDbReportDataSource(string connectionStringName) : base(connectionStringName)
		{
			Assert.ArgumentNotNull(connectionStringName, "connectionStringName");
			var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
			Assert.IsNotNull(connectionString, "connectionString");
			var mongoUrl = new MongoUrl(connectionString);
			database = new MongoClient(connectionString).GetServer().GetDatabase(mongoUrl.DatabaseName);
		}

		public override DataTable GetData(ReportDataQuery request)
		{
			Assert.ArgumentNotNull(request, "request");
			var queryParser = GetQueryParser(request);
			var collection = queryParser.GetCollection();
			var fields = queryParser.GetFields();
			var query = queryParser.GetQuery();
			foreach (var filter in request.Filters)
			{
				var dbValueListFilter = GetFilter(filter) as MongoDbValueListFilter;
				Assert.IsNotNull(dbValueListFilter, "dataSourceFilter");
				dbValueListFilter.Inject(query);
			}
			var mongoCursor = database[collection].Find(query);
			var flag1 = IncludesInteractionDocumentField(collection, fields, "ChannelId");
			var flag2 = IncludesInteractionDocumentField(collection, fields, "TrafficType");
			if (flag1 && !flag2)
			{
				var strArray = new string[fields.Length + 1];
				Array.Copy(fields, strArray, fields.Length);
				strArray[strArray.Length - 1] = "TrafficType";
				fields = strArray;
			}
			mongoCursor.SetFields(fields);
			var sortBy = queryParser.GetSortBy();
			if (sortBy != null)
				mongoCursor.SetSortOrder(sortBy);
			var skip = queryParser.GetSkip();
			mongoCursor.Skip = skip;
			var limit = queryParser.GetLimit();
			if (limit != -1)
				mongoCursor.Limit = limit;
			
			var dataTable = new BsonDocumentLoader().GetDataTable(fields, mongoCursor);
			dataTable.TableName = collection + "Data";
			if (flag1)
			{
				for (var index = 0; index < dataTable.Rows.Count; ++index)
				{
					var guid = Guid.Empty;
					if (dataTable.Rows[index]["ChannelId"] != DBNull.Value)
						guid = (Guid) dataTable.Rows[index]["ChannelId"];
					if (guid == Guid.Empty && dataTable.Rows[index]["TrafficType"] != DBNull.Value)
					{
						var channelId = TrafficTypeConverter.ConvertToChannelId((int) dataTable.Rows[index]["TrafficType"]);
						dataTable.Rows[index]["ChannelId"] = !channelId.HasValue ? Guid.Empty : (object) channelId.Value;
					}
				}
				dataTable.AcceptChanges();
				if (!flag2 && dataTable.Columns.Contains("TrafficType"))
					dataTable.Columns.Remove("TrafficType");
			}
			return dataTable;
		}

		private bool IncludesInteractionDocumentField(string collection, string[] fields, string field)
		{
			var flag = false;
			if (string.Equals(collection, "Interactions", StringComparison.Ordinal))
			{
				for (var index = 0; index < fields.Length; ++index)
				{
					flag = string.Equals(fields[index], field, StringComparison.Ordinal);
					if (flag)
						break;
				}
			}
			return flag;
		}
	}
}