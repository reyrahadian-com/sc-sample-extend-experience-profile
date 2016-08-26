using System;
using System.Text;
using System.Web.UI;
using Sitecore.Analytics;
using Sitecore.Analytics.Model;
using Sitecore.Analytics.Model.Entities;
using Sitecore.TC.ExperienceProfile.ContactFacets;

namespace Sitecore.TC.ExperienceProfile
{
	public partial class UpdateContact : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			PrintCurrentContactInfo();
		}

		private void PrintCurrentContactInfo()
		{
			txtResult.Text = string.Empty;
			if (Tracker.Current != null && Tracker.Current.Contact != null)
			{
				var sb = new StringBuilder();
				var contact = Tracker.Current.Contact;
				if (contact.Identifiers.IdentificationLevel == ContactIdentificationLevel.Known)
				{
					sb.AppendLine(string.Format("Identified contact: {0}", contact.Identifiers.Identifier));
				}
				else
				{
					sb.AppendLine("Anonymous contact");
				}

				var personalInfo = contact.GetFacet<IContactPersonalInfo>("Personal");
				sb.AppendLine(string.Format("First name: {0}", personalInfo.FirstName));
				sb.AppendLine(string.Format("Last name: {0}", personalInfo.Surname));

				var customFieldsFacet = contact.GetFacet<ICustomFieldsFacet>(CustomFieldsFacet.FACET_NAME);
				sb.AppendLine(string.Format("Hospital name: {0}", customFieldsFacet.HospitalName));
				sb.AppendLine(string.Format("Profession name: {0}", customFieldsFacet.ProfessionName));

				var newsletterSubscriptionFacet =
					contact.GetFacet<INewsletterSubscriptionFacet>(NewsletterSubscriptionFacet.FACET_NAME);
				foreach (var newsletterSubcription in newsletterSubscriptionFacet.Newsletters)
				{
					sb.AppendLine(string.Format("Newsletter: {0}", newsletterSubcription.NewsletterName));
				}


				txtResult.Text = sb.ToString();
			}
			else
			{
				txtResult.Text = "Tracker is not available!";
			}
		}

		protected void btnUpdateContact_OnClick(object sender, EventArgs e)
		{
			if (Tracker.Current != null && Tracker.Current.Contact != null)
			{
				var contact = Tracker.Current.Contact;
				var personalInfo = contact.GetFacet<IContactPersonalInfo>("Personal");
				personalInfo.FirstName = txtFirstName.Text;
				personalInfo.Surname = txtLastName.Text;

				var customFacet = contact.GetFacet<ICustomFieldsFacet>(CustomFieldsFacet.FACET_NAME);
				customFacet.HospitalName = txtHospitalName.Text;
				customFacet.ProfessionName = txtProfession.Text;

				var newsletterSubscriptionFacet =
					contact.GetFacet<INewsletterSubscriptionFacet>(NewsletterSubscriptionFacet.FACET_NAME);
				newsletterSubscriptionFacet.Reset();

				if (!string.IsNullOrWhiteSpace(txtNewsletterSubscription1.Text))
				{
					var newsletterSubscription = newsletterSubscriptionFacet.Newsletters.Create();
					newsletterSubscription.NewsletterName = txtNewsletterSubscription1.Text;
				}
				if (!string.IsNullOrWhiteSpace(txtNewsletterSubscription2.Text))
				{
					var newsletterSubscription = newsletterSubscriptionFacet.Newsletters.Create();
					newsletterSubscription.NewsletterName = txtNewsletterSubscription2.Text;
				}

				PrintCurrentContactInfo();
			}
			else
			{
				txtResult.Text = "Tracker is not available!";
			}
		}

		protected void btnIdentifyContact_OnClick(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtContactIdentifier.Text) && Tracker.Current != null && Tracker.Current.Contact != null)
			{
				Tracker.Current.Session.Identify(txtContactIdentifier.Text);
				PrintCurrentContactInfo();
			}
		}

		protected void btnFlushSession_OnClick(object sender, EventArgs e)
		{
			Session.Abandon();
		}
	}
}