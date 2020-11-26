<%@ Page Title="" Language="C#" MasterPageFile="~/UniversalSite1.Master" AutoEventWireup="true" CodeBehind="ListForm.aspx.cs" Inherits="curs3laba4.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            height: 762px;
        }
    .auto-style3 {
        height: 132px;
        width: 221px;
    }
    .auto-style4 {
        height: 70%;
        overflow: scroll;
        margin-right: 18px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auto-style2" style="padding: 20px">
        <div class="auto-style3" style="position: fixed; width: 25%; height: 25%;">
        <asp:Button ID="ButtonGetDate" runat="server" Text="Вывести все записи" BackColor="Lime" BorderStyle="Inset" OnClick="ButtonGetDate_Click1" />
            <br />
        <asp:Button ID="ButtonGetDate0" runat="server" Text="Вывести фильтр. записи" BackColor="Lime" BorderStyle="Inset" OnClick="ButtonGetDate_Click" />
            <br />
        От:
            <asp:TextBox ID="TextBoxFrom" runat="server" Visible="False"></asp:TextBox>
            <asp:Calendar ID="CalendarFrom" runat="server" OnSelectionChanged="CalendarFrom_SelectionChanged"></asp:Calendar>
            <br />
        До:
            <asp:TextBox ID="TextBoxTo" runat="server" Visible="False"></asp:TextBox>
            <br />
            <asp:Calendar ID="CalendarTo" runat="server" OnSelectionChanged="CalendarTo_SelectionChanged"></asp:Calendar>
        <asp:Button ID="ButtonDelete" runat="server" Text="Удалить выбранные" BackColor="Lime" BorderStyle="Inset" OnClick="ButtonDelete_Click" />
            <br />
            <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="auto-style4" style="margin-left: 40%; ">
            <asp:GridView ID="GridView2" runat="server" Height="100%" Width="100%" BackColor="#FFFF66" CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="#FFFF66" />
                <Columns>
                    <asp:TemplateField HeaderText="X">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#33CC33" Font-Bold="True" ForeColor="Black" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#66FF66" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetDataToAndFrom" TypeName="curs3laba4.RefLibrary1.ServiceForLibrarySoapClient">
                <SelectParameters>
                    <asp:ControlParameter ControlID="TextBoxFrom" DefaultValue="01.01.0000" Name="from" PropertyName="Text" Type="DateTime" />
                    <asp:ControlParameter ControlID="TextBoxTo" DefaultValue="12.12.2050" Name="to" PropertyName="Text" Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllData" TypeName="curs3laba4.RefLibrary1.ServiceForLibrarySoapClient"></asp:ObjectDataSource>
        </div>
</div>
</asp:Content>
