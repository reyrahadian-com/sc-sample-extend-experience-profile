define(["sitecore", "/-/speak/v1/experienceprofile/DataProviderHelper.js", "/-/speak/v1/experienceprofile/CintelUtl.js"], function (sc, providerHelper, cintelUtil) {
	var app = sc.Definitions.App.extend({
		initialized: function () {
			var localUrl = "/newslettersubscriptions/";
			var tableName = "newsletters";
			providerHelper.setupHeaders([
				{ urlKey: localUrl }
			]);

			var url = sc.Contact.baseUrl + localUrl;			

			providerHelper.initProvider(this.NewsletterSubscriptionsDataProvider, tableName, url, this.NewsletterSubscriptionsMessageBar);
			providerHelper.subscribeSorting(this.NewsletterSubscriptionsDataProvider, this.NewsletterSubscriptions);			
			providerHelper.getListData(this.NewsletterSubscriptionsDataProvider);			
		}
	});
	return app;
});