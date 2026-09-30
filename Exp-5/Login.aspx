<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs"
    Inherits="AcademicLeaveSystem.Login" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Student Login</title>

    <style>
        body {
            font-family: Arial;
            background-color: #f2f2f2;
        }

        .box {
            width: 400px;
            margin: 100px auto;
            padding: 30px;
            background-color: white;
            border: 1px solid #ccc;
            text-align: center;
        }

        h2 {
            color: #0066cc;
        }

        .button {
            background-color: #0066cc;
            color: white;
            padding: 8px 25px;
            border: none;
        }
    </style>
</head>

<body>

<form id="form1" runat="server">

    <div class="box">

        <h2>Academic Leave System</h2>

        <br />

        Student Name:

        <br />

        <asp:TextBox ID="txtName"
            runat="server">
        </asp:TextBox>

        <br /><br />

        <asp:Button ID="btnLogin"
            runat="server"
            Text="Login"
            CssClass="button"
            OnClick="btnLogin_Click" />

        <br /><br />

        <asp:Label ID="lblMessage"
            runat="server"
            ForeColor="Red">
        </asp:Label>

    </div>

</form>

</body>
</html>
