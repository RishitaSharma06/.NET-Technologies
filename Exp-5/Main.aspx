<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Main.aspx.cs"
    Inherits="AcademicLeaveSystem.Main" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Academic Calendar and Leave</title>

    <style>

        body {
            font-family: Arial;
            background-color: #f2f2f2;
        }

        .container {
            width: 900px;
            margin: 30px auto;
            background-color: white;
            padding: 25px;
        }

        h1 {
            color: #0066cc;
        }

        .section {
            border: 1px solid #ccc;
            padding: 20px;
            margin-top: 20px;
        }

        .button {
            background-color: #0066cc;
            color: white;
            padding: 7px 20px;
            border: none;
        }

    </style>
</head>

<body>

<form id="form1" runat="server">

<div class="container">

    <h1>Academic Calendar & Leave Management</h1>

    <asp:Label ID="lblWelcome"
        runat="server"
        Font-Bold="true">
    </asp:Label>

    <br /><br />

    <!-- Academic Calendar -->

    <div class="section">

        <h2>Academic Calendar</h2>

        <asp:Calendar ID="calAcademic"
            runat="server"
            OnSelectionChanged="calAcademic_SelectionChanged">

            <TitleStyle
                BackColor="#0066cc"
                ForeColor="White" />

            <SelectedDayStyle
                BackColor="Green"
                ForeColor="White" />

        </asp:Calendar>

        <br />

        <asp:Label ID="lblDate"
            runat="server"
            Font-Bold="true">
        </asp:Label>

    </div>


    <!-- Leave Application -->

    <div class="section">

        <h2>Apply for Leave</h2>

        Leave Date:

        <asp:TextBox ID="txtLeaveDate"
            runat="server"
            TextMode="Date">
        </asp:TextBox>

        <br /><br />

        Reason:

        <br />

        <asp:TextBox ID="txtReason"
            runat="server"
            TextMode="MultiLine"
            Rows="3"
            Columns="40">
        </asp:TextBox>

        <br /><br />

        Leave Type:

        <asp:RadioButtonList ID="rblLeaveType"
            runat="server">

            <asp:ListItem
                Text="Full Day"
                Value="Full Day"
                Selected="True">
            </asp:ListItem>

            <asp:ListItem
                Text="Half Day"
                Value="Half Day">
            </asp:ListItem>

        </asp:RadioButtonList>

        <br />

        <asp:Button ID="btnApply"
            runat="server"
            Text="Apply Leave"
            CssClass="button"
            OnClick="btnApply_Click" />

        <br /><br />

        <asp:Label ID="lblMessage"
            runat="server"
            ForeColor="Green">
        </asp:Label>

    </div>


    <!-- Leave Records -->

    <div class="section">

        <h2>My Leave Applications</h2>

        <asp:GridView ID="gvLeave"
            runat="server"
            AutoGenerateColumns="true"
            BorderWidth="1"
            CellPadding="8">
        </asp:GridView>

    </div>


    <br />

    <asp:Button ID="btnLogout"
        runat="server"
        Text="Logout"
        CssClass="button"
        OnClick="btnLogout_Click" />

</div>

</form>

</body>
</html>
