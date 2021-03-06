﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteAction.aspx.cs" Inherits="WebCounter.AdminPanel.SiteAction" %>
<%@ Register TagPrefix="uc" TagName="SiteActionList" Src="~/UserControls/SiteActionList.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">    
    <asp:Panel ID="pUserInfo" Visible="false" runat="server">
        <div class="row">
            <label>Ф.И.О.:</label>
            <asp:Literal ID="lUserFullName" runat="server"></asp:Literal>
        </div>
        <div class="row">
            <label>Общий балл:</label>
            <asp:Literal ID="lScore" runat="server"></asp:Literal>
        </div>
        <div class="row">
            <label>Балл по поведению:</label>
            <asp:Literal ID="lBehaviorScore" runat="server"></asp:Literal>
        </div>
        <div class="row">
            <label>Балл по характеристикам:</label>
            <asp:Literal ID="lCharacteristicsScore" runat="server"></asp:Literal>
        </div>
        <br />
    </asp:Panel>
    <uc:SiteActionList runat="server" />
</asp:Content>