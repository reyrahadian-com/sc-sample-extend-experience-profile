define(["sitecore", "/-/speak/v1/experienceprofile/DataProviderHelper.js", "/-/speak/v1/experienceprofile/CintelUtl.js"], function (sc, providerHelper, cintelUtil) {
	var app = sc.Definitions.App.extend({
		initialized: function () {
			var tableName = "downloadedpdffiles"; // Case Sensitive!
			var localUrl = "/intel/" + tableName;

			providerHelper.setupHeaders([
				{ urlKey: localUrl + "?", headerValue: "none" }
			]);

			var url = sc.Contact.baseUrl + localUrl;

			providerHelper.initProvider(this.DownloadedPdfFilesDataProvider, tableName, url, this.DownloadedPdfFilesTabMessageBar);
			providerHelper.subscribeSorting(this.DownloadedPdfFilesDataProvider, this.DownloadedPdfFiles);
			providerHelper.getListData(this.DownloadedPdfFilesDataProvider);									
		}
	});
	return app;
});