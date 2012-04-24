﻿<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="ListDetails.aspx.cs" Inherits="Oasis.Admin.ListDetails" %>

<%@ Register src="~/DynamicData/Content/GridViewPager.ascx" tagname="GridViewPager" tagprefix="asp" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
            <asp:DataControlReference ControlID="GridView1" />
        </DataControls>
    </asp:DynamicDataManager>

    <h2 class="DDSubHeader"><%= table.DisplayName %></h2>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="DD">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                    HeaderText="List of validation errors" CssClass="DDValidator" />
                <asp:DynamicValidator runat="server" ID="GridViewValidator" ControlToValidate="GridView1" Display="None" CssClass="DDValidator" />
                <asp:DynamicValidator runat="server" ID="FormViewValidator" ControlToValidate="FormView1" Display="None" CssClass="DDValidator" />

                <asp:QueryableFilterRepeater runat="server" ID="FilterRepeater">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="Label_PreRender" />
                        <asp:DynamicFilter runat="server" ID="DynamicFilter" OnFilterChanged="DynamicFilter_FilterChanged" /><br />
                    </ItemTemplate>
                </asp:QueryableFilterRepeater>
                <br />
            </div>

            <asp:GridView ID="GridView1" runat="server" DataSourceID="GridDataSource" EnablePersistedSelection="true"
                AutoGenerateSelectButton="True" AutoGenerateEditButton="True" AutoGenerateDeleteButton="true"
                AllowPaging="True" AllowSorting="True" OnDataBound="GridView1_DataBound"
                OnRowEditing="GridView1_RowEditing" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
                OnRowDeleted="GridView1_RowDeleted" OnRowUpdated="GridView1_RowUpdated"
                OnRowCreated="GridView1_RowCreated" CssClass="DDGridView"
                RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6">

                <PagerStyle CssClass="DDFooter" />        
                <SelectedRowStyle CssClass="DDSelected" />
                <PagerTemplate>
                    <asp:GridViewPager runat="server" />
                </PagerTemplate>
                <EmptyDataTemplate>
                    There are currently no items in this table.
                </EmptyDataTemplate>
            </asp:GridView>

            <asp:EntityDataSource ID="GridDataSource" runat="server" EnableDelete="true" EnableUpdate="true" />
            
            <asp:QueryExtender ID="GridQueryExtender" TargetControlID="GridDataSource" runat="server">
                <asp:DynamicFilterExpression ControlID="FilterRepeater" />
            </asp:QueryExtender>

            <asp:Panel ID="DetailsPanel" runat="server">
                <br /><br />

                <asp:FormView ID="FormView1" runat="server" DataSourceID="DetailsDataSource" RenderOuterTable="false"
                    OnPreRender="FormView1_PreRender" OnModeChanging="FormView1_ModeChanging" OnItemUpdated="FormView1_ItemUpdated"
                    OnItemInserted="FormView1_ItemInserted" OnItemDeleted="FormView1_ItemDeleted">
                    <HeaderTemplate>
                        <table id="detailsTable" class="DDDetailsTable" cellpadding="6">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:DynamicEntity runat="server" />
                        <tr class="td">
                            <td colspan="2">
                                <asp:LinkButton runat="server" CommandName="Edit" Text="Edit" />
                                <asp:LinkButton runat="server" CommandName="Delete" Text="Delete"
                                    OnClientClick='return confirm("Are you sure you want to delete this item?");' />
                                <asp:LinkButton runat="server" CommandName="New" Text="New" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DynamicEntity runat="server" Mode="Edit" />
                        <tr class="td">
                            <td colspan="2">
                                <asp:LinkButton runat="server" CommandName="Update" Text="Update" />
                                <asp:LinkButton runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="false" />
                            </td>
                        </tr>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:DynamicEntity runat="server" Mode="Insert" />
                        <tr class="td">
                            <td colspan="2">
                                <asp:LinkButton runat="server" CommandName="Insert" Text="Insert" />
                                <asp:LinkButton runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="false" />
                            </td>
                        </tr>
                    </InsertItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:FormView>

                <asp:EntityDataSource ID="DetailsDataSource" runat="server" EnableDelete="true" EnableInsert="true" EnableUpdate="true" />

                <asp:QueryExtender TargetControlID="DetailsDataSource" runat="server">
                    <asp:ControlFilterExpression ControlID="GridView1" />
                </asp:QueryExtender>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

