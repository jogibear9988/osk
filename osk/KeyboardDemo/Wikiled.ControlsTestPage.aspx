<%@ Page Language="c#" AutoEventWireup="true" %>

<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls"
    TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head runat="server">
    <title>Wikiled.Controls</title>
    <script type="text/javascript" src="jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="Keyboard.js"></script>
</head>
<body style="height: 100%; margin: 0;">
    <form id="form1" runat="server" style="height: 100%;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="width: 100; height: 50px; padding-top:20px; text-align: center;">
        <asp:Label ID="Label1" runat="server" Text="Text:" Font-Bold="true"></asp:Label>
        <asp:TextBox ID="textBoxInput" runat="server"></asp:TextBox>
        <asp:CheckBox ID="checkBoxShowToobar" Text="Show Control Bar" runat="server" />
        <asp:CheckBox ID="checkBoxEachKey" runat="server" Text="Subscribe to each key" />
    </div>
    <div id="keyboardControlFloating" style="height: 100%;">
        <asp:Silverlight ID="Xaml1" runat="server" Source="~/ClientBin/Wikiled.Controls.xap"
            OnPluginLoaded="PluginLoaded" MinimumVersion="2.0.31005.0" Width="100%" Height="100%" />
    </div>
    </form>
</body>
</html>
