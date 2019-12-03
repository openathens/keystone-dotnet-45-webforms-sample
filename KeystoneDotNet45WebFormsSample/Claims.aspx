<%@ Page Title="Claims" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Claims.aspx.cs" Inherits="KeystoneDotNet45WebFormsSample.Claims" %>
<%@ Import Namespace="System.Security.Claims" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>OpenID Connect Claims</h2>
    <dl>
        <asp:DataList runat="server" ID="dlClaims">
            <ItemTemplate>
                <dt><%# ((Claim) Container.DataItem).Type %></dt>
                <dd><%# ((Claim) Container.DataItem).Value %></dd>
            </ItemTemplate>
        </asp:DataList>
    </dl>
</asp:Content>
