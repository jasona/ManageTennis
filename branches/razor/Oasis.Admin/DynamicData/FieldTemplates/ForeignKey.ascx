<%@ Control Language="C#" CodeBehind="ForeignKey.ascx.cs" Inherits="Oasis.Admin.ForeignKeyField" %>

<asp:HyperLink ID="HyperLink1" runat="server"
    Text="<%# GetDisplayString() %>"
    NavigateUrl="<%# GetNavigateUrl() %>"  />

