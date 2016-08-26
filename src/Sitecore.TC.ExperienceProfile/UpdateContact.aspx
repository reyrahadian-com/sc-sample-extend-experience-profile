<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateContact.aspx.cs" Inherits="Sitecore.TC.ExperienceProfile.UpdateContact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<h1>Update current contact example</h1>
	<form id="form1" runat="server">
		<fieldset>
			<legend>Flush Session</legend>
			<asp:Button runat="server" ID="btnFlushSession" Text="Flush Sesssion" OnClick="btnFlushSession_OnClick"/>
		</fieldset>
		<fieldset>
			<legend>Identify User</legend>
			<div>
				<label>Contact Identifier</label>
				<asp:TextBox runat="server" ID="txtContactIdentifier"></asp:TextBox>
			</div>
			<asp:Button runat="server" ID="btnIdentifyContact" Text="Identify Contact" OnClick="btnIdentifyContact_OnClick"/>			
		</fieldset>
		<fieldset>
			<legend>Personal Info</legend>
			<div>
				<label>First Name</label>:
			<asp:TextBox runat="server" ID="txtFirstName"></asp:TextBox>
			</div>
			<div>
				<label>Last Name</label>:
			<asp:TextBox runat="server" ID="txtLastName"></asp:TextBox>
			</div>
		</fieldset>
		<fieldset>
			<legend>Custom Facets</legend>
			<div>
				<label>Hospital Name</label>:
				<asp:TextBox runat="server" ID="txtHospitalName"></asp:TextBox>
			</div>
			<div>
				<label>Profession</label>
				<asp:TextBox runat="server" ID="txtProfession"></asp:TextBox>
			</div>
			<div>
				<label>Newsletter Subscription 1</label>
				<asp:TextBox runat="server" ID="txtNewsletterSubscription1"></asp:TextBox>
			</div>
			<div>
				<label>Newsletter Subscription 2</label>
				<asp:TextBox runat="server" ID="txtNewsletterSubscription2"></asp:TextBox>
			</div>
			<asp:Button runat="server" ID="btnUpdateContact" Text="Update current contact" OnClick="btnUpdateContact_OnClick" />
		</fieldset>
		<br />
		<br />
		<div>
			Current contact info:
			<br />
			<br />
			<asp:TextBox runat="server" ID="txtResult" Rows="40" Columns="100" TextMode="MultiLine"></asp:TextBox>
		</div>
	</form>
</body>
</html>
