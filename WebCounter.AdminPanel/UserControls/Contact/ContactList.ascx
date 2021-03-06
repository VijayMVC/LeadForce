﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactList.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Contact.ContactList" %>

<labitec:Grid ID="gridContacts" TableName="tbl_Contact" Fields="tbl_Priorities.Image, tbl_ReadyToSell.Image" AccessCheck="true"  ClassName="WebCounter.AdminPanel.Contacts" OnItemDataBound="gridContacts_OnItemDataBound" Export="true" runat="server">
    <Columns>
        <labitec:GridColumn ID="GridColumn1" DataField="CreatedAt" HeaderText="Дата создания" DataType="DateTime" runat="server"/>
        <labitec:GridColumn ID="GridColumn2" DataField="LastActivityAt" HeaderText="Последняя активность" DataType="DateTime" runat="server"/>
        <labitec:GridColumn ID="GridColumn3" DataField="UserFullName" HeaderText="Ф.И.О." runat="server"/>
        <labitec:GridColumn ID="GridColumn4" DataField="Email" HeaderText="E-mail" runat="server"/>
        <labitec:GridColumn ID="GridColumn5" DataField="tbl_Company.Name" HeaderText="Компания" runat="server"/>
        <labitec:GridColumn ID="GridColumn6" DataField="tbl_ReadyToSell.Title" HeaderText="Готовность к продаже" runat="server">
            <ItemTemplate>
                <asp:Literal ID="litReadyToSell" runat="server" />
                <asp:Image ID="imgReadyToSell" Visible="False" runat="server"/>
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn11" DataField="tbl_Priorities.Title" HeaderText="Важность" runat="server">
            <ItemTemplate>
                <asp:Literal ID="litPriority" runat="server" />
                <asp:Image ID="imgPriority" Visible="False" runat="server"/>
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn8" DataField="Score" HeaderText="Общий балл" DataType="Int32" runat="server"/>
        <labitec:GridColumn ID="GridColumn12" DataField="ID" HeaderText="Действия" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" HorizontalAlign="Center" runat="server">
            <ItemTemplate>
                <asp:HyperLink ID="hlEdit" ImageUrl="~/App_Themes/Default/images/icoView.png" ToolTip="Карточка контакта" runat="server" />
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn9" DataField="IsNameChecked" HeaderText="Корректность имени" DataType="Boolean" Visible="false" runat="server"/>
        <labitec:GridColumn ID="GridColumn10" DataField="EmailStatusID" HeaderText="Статус Email" Visible="false" runat="server"/>
    </Columns>
    <Joins>
        <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_ReadyToSell" JoinTableKey="ID" TableKey="ReadyToSellID" runat="server" />
        <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Priorities" JoinTableKey="ID" TableKey="PriorityID" runat="server" />
        <labitec:GridJoin ID="GridJoin4" JoinTableName="tbl_Company" JoinTableKey="ID" TableKey="CompanyID" runat="server" />
    </Joins>
</labitec:Grid>