using System.Data;
using Sitecore.Cintel.Client;
using Sitecore.Cintel.Client.Transformers;
using Sitecore.Cintel.Commons;
using Sitecore.Cintel.Endpoint.Transfomers;
using Sitecore.Diagnostics;

namespace Sitecore.TC.ExperienceProfile.CIntel.Client.Transformers.Contact
{
	public class DefaultResultTransformer : IIntelResultTransformer
	{
		private ResultSetExtender resultSetExtender;

		public DefaultResultTransformer()
		{
			resultSetExtender = ClientFactory.Instance.GetResultSetExtender();
		}

		public DefaultResultTransformer(ResultSetExtender resultSetExtender)
		{
			this.resultSetExtender = resultSetExtender;
		}

		public object Transform(ResultSet<DataTable> resultSet)
		{
			Assert.ArgumentNotNull(resultSet, "resultSet");

			return resultSet;
		}
	}
}