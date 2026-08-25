<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Registration_Form.aspx.cs"
    Inherits="PRACTICAL4.Registration_Form"
    UnobtrusiveValidationMode="None" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Event Registration Form</h3>
        </div>
        <p>
            &nbsp;<asp:Label ID="Label1" runat="server" Text="NAME"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" CausesValidation="True" style="margin-left: 170px" Width="232px"></asp:TextBox>
&nbsp;
<asp:RequiredFieldValidator ID="rfvName"
    runat="server"
    ControlToValidate="TextBox1"
    ErrorMessage="* Name is Required"
    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>        
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="*Enter Alphabets Only" ForeColor="Red" ValidationExpression="^[A-Za-z ]+$"></asp:RegularExpressionValidator>
        </p>
        <asp:Label ID="Label7" runat="server" Text="GENDER"></asp:Label>
        <asp:RadioButtonList ID="RadioButtonList3" runat="server" Height="16px" OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow" style="margin-left: 159px" Width="236px">
            <asp:ListItem>MALE</asp:ListItem>
            <asp:ListItem>FEMALE</asp:ListItem>
        </asp:RadioButtonList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="RadioButtonList3" Display="Dynamic" ErrorMessage="*Please Select Gender" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="ENROLLMENT NO"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" style="margin-left: 86px" Width="228px" CausesValidation="True"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox2" Display="Dynamic" ErrorMessage="*Enrollment No is Required" ForeColor="Red"></asp:RequiredFieldValidator>
&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox2" Display="Dynamic" ErrorMessage="*Enter Numbers Only" ForeColor="Red" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
&nbsp;<p>
            <asp:Label ID="Label3" runat="server" Text="GR NO."></asp:Label>
            <asp:TextBox ID="TextBox3" runat="server" style="margin-left: 165px" Width="231px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox3" Display="Dynamic" ErrorMessage="*GR No. is required" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox3" Display="Dynamic" ErrorMessage="*Enter Numbers Only" ForeColor="Red" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
        </p>
        <p>
            <asp:Label ID="Label5" runat="server" Text="COURSE"></asp:Label>
            <asp:TextBox ID="TextBox5" runat="server" OnTextChanged="TextBox5_TextChanged" style="margin-left: 156px" Width="231px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox5" Display="Dynamic" ErrorMessage="*Valid Course is required" ForeColor="Red"></asp:RequiredFieldValidator>
        </p>
        <asp:Label ID="Label4" runat="server" Text="DEPARTMENT"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server" Height="25px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" style="margin-left: 0px">
            <asp:ListItem Value="0">Choose Your Branch</asp:ListItem>
            <asp:ListItem Value="CE">Computer Engineering</asp:ListItem>
            <asp:ListItem Value="IT">Information Technology</asp:ListItem>
            <asp:ListItem>Mechanical Engineering</asp:ListItem>
            <asp:ListItem>Civil Engineering</asp:ListItem>
            <asp:ListItem>Electrical Engineering</asp:ListItem>
            <asp:ListItem>Computer Science &amp; Engineering</asp:ListItem>
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DropDownList1" Display="Dynamic" ErrorMessage="*Select your Branch" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="E-MAIL"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox6" runat="server" OnTextChanged="TextBox5_TextChanged" style="margin-left: 164px" Width="227px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox6" Display="Dynamic" ErrorMessage="*Valid Email is required" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox6" ErrorMessage="*Enter Valid Email" ForeColor="Red" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"></asp:RegularExpressionValidator>
        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Button" />
        </p>
    </form>
</body>
</html>
