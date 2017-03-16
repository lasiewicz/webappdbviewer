<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

  
      <asp:SqlDataSource ID="csod_nolo" runat="server" ConnectionString="<%$ ConnectionStrings:csod_nolioConnectionString %>" SelectCommand="SELECT  [name] AS [TableName] FROM sys.tables where create_date &gt; '2017-03-13 00:00:00.000'" OnSelecting="csod_nolo_Selecting"></asp:SqlDataSource>
    
Data as of
<asp:Label ID="time" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Table ID="MyTable" runat="server" BorderStyle="Ridge" BorderWidth="1px" CellPadding="1" CellSpacing="1" GridLines="Both" CssClass="prettyprint">
    </asp:Table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="csod_nolo" Height="1px" Width="63px">
        <Columns>
            <asp:BoundField DataField="TableName" HeaderText="TableName" SortExpression="TableName" />
        </Columns>
    </asp:GridView>
    
    

<br />
<br />
<br />
<br />
    
    

</asp:Content>
