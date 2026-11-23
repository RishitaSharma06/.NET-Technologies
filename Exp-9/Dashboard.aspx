<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="PlacementDashboard.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Campus Placement Dashboard</title>
    <style>
        * { box-sizing: border-box; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
        body { background-color: #0f172a; color: #f8fafc; margin: 0; padding: 24px; }
        .container { max-width: 1320px; margin: 0 auto; }
        .header { display: flex; justify-content: space-between; align-items: center; margin-bottom: 24px; border-bottom: 1px solid #334155; padding-bottom: 16px; }
        .header h1 { margin: 0; font-size: 26px; color: #38bdf8; }
        .header p { margin: 4px 0 0; color: #94a3b8; font-size: 14px; }

        .kpi-grid { display: grid; grid-template-columns: repeat(4, 1fr); gap: 16px; margin-bottom: 24px; }
        .kpi-card { background: #1e293b; border: 1px solid #334155; border-radius: 10px; padding: 20px; }
        .kpi-title { font-size: 13px; color: #94a3b8; text-transform: uppercase; letter-spacing: 0.5px; }
        .kpi-value { font-size: 28px; font-weight: 700; color: #f8fafc; margin-top: 8px; }
        .kpi-accent { color: #34d399; }

        .filter-bar { background: #1e293b; padding: 14px 20px; border-radius: 10px; border: 1px solid #334155; display: flex; gap: 16px; align-items: center; margin-bottom: 24px; }
        .filter-bar select, .btn { padding: 8px 14px; border-radius: 6px; border: 1px solid #475569; background: #0f172a; color: #f8fafc; font-size: 14px; }
        .btn-primary { background: #0284c7; border-color: #0284c7; cursor: pointer; font-weight: 600; }
        .btn-primary:hover { background: #0369a1; }

        .main-layout { display: grid; grid-template-columns: 2.3fr 1fr; gap: 24px; }
        .panel { background: #1e293b; border: 1px solid #334155; border-radius: 10px; padding: 20px; margin-bottom: 24px; }
        .panel h2 { margin-top: 0; font-size: 18px; color: #e2e8f0; border-bottom: 1px solid #334155; padding-bottom: 10px; }

        .grid-table { width: 100%; border-collapse: collapse; font-size: 14px; }
        .grid-table th { background: #0f172a; color: #38bdf8; text-align: left; padding: 12px; border-bottom: 2px solid #334155; }
        .grid-table td { padding: 10px 12px; border-bottom: 1px solid #334155; }
        .grid-table tr:hover { background: #263348; }
        .grid-table a { color: #38bdf8; text-decoration: none; margin-right: 8px; font-weight: 500; }
        .badge-selected { background: #065f46; color: #6ee7b7; padding: 3px 8px; border-radius: 12px; font-size: 12px; }
        .badge-shortlisted { background: #78350f; color: #fcd34d; padding: 3px 8px; border-radius: 12px; font-size: 12px; }

        .company-item { display: flex; justify-content: space-between; align-items: center; padding: 10px 0; border-bottom: 1px solid #334155; }
        .company-item:last-child { border-bottom: none; }

        .details-table { width: 100%; border-collapse: collapse; font-size: 14px; }
        .details-table td { padding: 8px; border-bottom: 1px solid #334155; }
        .details-table input[type="text"], .details-table select { width: 100%; padding: 6px; background: #0f172a; border: 1px solid #475569; color: #fff; border-radius: 4px; }
        .details-table a { display: inline-block; padding: 6px 14px; margin-right: 8px; margin-top: 6px; border-radius: 6px; text-decoration: none; font-weight: 600; background: #0284c7; color: #fff; }
        .details-table a:last-child { background: #475569; }
        .msg-error { color: #f87171; font-size: 13px; display: block; margin-bottom: 8px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <div>
                    <h1>Training & Placement Cell Dashboard</h1>
                    <p>Academic Year 2026–2027 | Live Recruitment Analytics</p>
                </div>
                <asp:Button ID="btnNewRecord" runat="server" Text="+ Add Placement Record" CssClass="btn btn-primary" OnClick="btnNewRecord_Click" CausesValidation="false" />
            </div>

            <asp:FormView ID="fvSummary" runat="server" Width="100%">
                <ItemTemplate>
                    <div class="kpi-grid">
                        <div class="kpi-card">
                            <div class="kpi-title">Total Applications</div>
                            <div class="kpi-value"><%# Eval("TotalDrives") %></div>
                        </div>
                        <div class="kpi-card">
                            <div class="kpi-title">Students Placed</div>
                            <div class="kpi-value kpi-accent"><%# Eval("TotalPlaced") %></div>
                        </div>
                        <div class="kpi-card">
                            <div class="kpi-title">Highest Package</div>
                            <div class="kpi-value"><%# Eval("HighestPackage") %> LPA</div>
                        </div>
                        <div class="kpi-card">
                            <div class="kpi-title">Average Package</div>
                            <div class="kpi-value"><%# Eval("AvgPackage") %> LPA</div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:FormView>

            <div class="filter-bar">
                <span>Filter by Branch:</span>
                <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterChanged">
                    <asp:ListItem Text="All Branches" Value="All" />
                    <asp:ListItem Text="Computer Engineering" Value="Computer Engineering" />
                    <asp:ListItem Text="IT" Value="IT" />
                    <asp:ListItem Text="Mechanical" Value="Mechanical" />
                </asp:DropDownList>

                <span>Status:</span>
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterChanged">
                    <asp:ListItem Text="All Statuses" Value="All" />
                    <asp:ListItem Text="Selected" Value="Selected" />
                    <asp:ListItem Text="Shortlisted" Value="Shortlisted" />
                </asp:DropDownList>
            </div>

            <div class="main-layout">
                <div class="panel">
                    <h2>Student Placement Records</h2>
                    <asp:GridView ID="gvPlacements" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="PlacementId" CssClass="grid-table" AllowPaging="True" PageSize="6"
                        OnPageIndexChanging="gvPlacements_PageIndexChanging"
                        OnRowEditing="gvPlacements_RowEditing"
                        OnRowCancelingEdit="gvPlacements_RowCancelingEdit"
                        OnRowUpdating="gvPlacements_RowUpdating"
                        OnRowDeleting="gvPlacements_RowDeleting"
                        OnSelectedIndexChanged="gvPlacements_SelectedIndexChanged"
                        EmptyDataText="No placement records match the selected filter.">
                        <Columns>
                            <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                            <asp:BoundField DataField="Branch" HeaderText="Branch" ReadOnly="True" />
                            <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                            <asp:BoundField DataField="Role" HeaderText="Role" />
                            <asp:BoundField DataField="PackageLPA" HeaderText="CTC (LPA)" DataFormatString="{0:N2}" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <span class='<%# Eval("Status").ToString() == "Selected" ? "badge-selected" : "badge-shortlisted" %>'>
                                        <%# Eval("Status") %>
                                    </span>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEditStatus" runat="server" SelectedValue='<%# Bind("Status") %>'>
                                        <asp:ListItem Text="Selected" Value="Selected" />
                                        <asp:ListItem Text="Shortlisted" Value="Shortlisted" />
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" ShowEditButton="True" ShowDeleteButton="True" HeaderText="Actions" />
                        </Columns>
                    </asp:GridView>
                </div>

                <div>
                    <div class="panel">
                        <h2>Record Inspector / Add Entry</h2>
                        <asp:Label ID="lblMessage" runat="server" CssClass="msg-error" />
                        <asp:DetailsView ID="dvPlacement" runat="server" AutoGenerateRows="False"
                            DataKeyNames="PlacementId" CssClass="details-table"
                            OnModeChanging="dvPlacement_ModeChanging"
                            OnItemInserting="dvPlacement_ItemInserting"
                            EmptyDataText="Click 'Select' on any row to view details, or click '+ Add Placement Record'.">
                            <Fields>
                                <asp:BoundField DataField="PlacementId" HeaderText="ID" ReadOnly="True" InsertVisible="False" />
                                <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                                <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
                                <asp:TemplateField HeaderText="Branch">
                                    <ItemTemplate><%# Eval("Branch") %></ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlInsertBranch" runat="server" SelectedValue='<%# Bind("Branch") %>'>
                                            <asp:ListItem Text="Computer Engineering" Value="Computer Engineering" />
                                            <asp:ListItem Text="IT" Value="IT" />
                                            <asp:ListItem Text="Mechanical" Value="Mechanical" />
                                        </asp:DropDownList>
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                <asp:BoundField DataField="Role" HeaderText="Job Role" />
                                <asp:BoundField DataField="PackageLPA" HeaderText="Package (LPA)" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate><%# Eval("Status") %></ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlInsertStatus" runat="server" SelectedValue='<%# Bind("Status") %>'>
                                            <asp:ListItem Text="Selected" Value="Selected" />
                                            <asp:ListItem Text="Shortlisted" Value="Shortlisted" />
                                        </asp:DropDownList>
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DriveDate" HeaderText="Drive Date (YYYY-MM-DD)" DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:CommandField ShowInsertButton="True" InsertText="Insert" CancelText="Cancel" />
                            </Fields>
                        </asp:DetailsView>
                    </div>

                    <div class="panel">
                        <h2>Top Recruiting Companies</h2>
                        <asp:Repeater ID="rptCompanies" runat="server">
                            <ItemTemplate>
                                <div class="company-item">
                                    <div>
                                        <strong><%# Eval("CompanyName") %></strong><br />
                                        <small style="color:#94a3b8;">Hires: <%# Eval("Hires") %></small>
                                    </div>
                                    <span style="color:#38bdf8; font-weight:700;"><%# Eval("MaxLPA") %> LPA</span>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
