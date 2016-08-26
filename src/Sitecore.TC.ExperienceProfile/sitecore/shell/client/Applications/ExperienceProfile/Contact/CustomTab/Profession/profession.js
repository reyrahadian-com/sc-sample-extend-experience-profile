define(["sitecore", "/-/speak/v1/experienceprofile/DataProviderHelper.js", "/-/speak/v1/experienceprofile/CintelUtl.js"], function (sc, providerHelper, cintelUtil) {
	var app = sc.Definitions.App.extend({
		initialized: function () {
			var localUrl = "/customfields/";

			providerHelper.setupHeaders([
				{ urlKey: localUrl  }
			]);

			var url = sc.Contact.baseUrl + localUrl;
			var $that = this;
			
			providerHelper.initProvider(this.ProfessionDataProvider, "", url, this.ProfessionTabMessageBar);
			providerHelper.getData(this.ProfessionDataProvider,
				$.proxy(function (jsondata) {					
					cintelUtil.setText($that.ProfessionNameValue, jsondata.ProfessionName, true);
					cintelUtil.setText($that.HospitalNameValue, jsondata.HospitalName, true);
				}));			
		}
	});
	return app;
});