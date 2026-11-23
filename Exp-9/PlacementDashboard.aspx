
<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="PlacementDashboard.aspx.cs"
    Inherits="PlacementDashboard.PlacementDashboard" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Placement Dashboard</title>
    <link href="CSS/Style.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">

<div class="sidebar">
    <h2>PlaceTrack</h2>
    <p>Placement Management</p>
    <a href="PlacementDashboard.aspx">Dashboard</a>
    <a href="Students.aspx">Students</a>
    <a href="Companies.aspx">Companies</a>
</div>

<div class="main">
    <h1>Placement Dashboard</h1>
    <p>Welcome to Placement Management System</p>

    <div class="cards">
        <div class="card">
            <h3>Total Students</h3>
            <asp:Label ID="lblTotal" runat="server" />
        </div>

        <div class="card">
            <h3>Placed Students</h3>
            <asp:Label ID="lblPlaced" runat="server" />
        </div>

        <div class="card">
            <h3>Companies</h3>
            <asp:Label ID="lblCompanies" runat="server" />
        </div>

        <div class="card">
            <h3>Placement Rate</h3>
            <asp:Label ID="lblRate" runat="server" />
        </div>
    </div>

    <h2>Recent Student Records</h2>

    <asp:GridView ID="gvStudents" runat="server"
        AutoGenerateColumns="true"
        CssClass="student-table"
        GridLines="None">
    </asp:GridView>

</div>
</form>
</body>
</html>
