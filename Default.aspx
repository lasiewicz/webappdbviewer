<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>
            <asp:SqlDataSource ID="csod_nolo" runat="server" ConnectionString="<%$ ConnectionStrings:csod_nolioConnectionString %>" SelectCommand="SELECT * FROM [server_sqlservers]"></asp:SqlDataSource>
            DB Deployments Status</h1>
    </div>
    
    
    <asp:GridView ID="GridView1" runat="server" DataSourceID="csod_nolo" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1">
    </asp:GridView>
    
    

</asp:Content>
