<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Smart-Quest | כניסה לצד עורך</title>

    <meta name="description" content="צד עורך של המשחק Smart-Quest" />
    <meta name="keywords" content="משחק, למידה, תרגול, עריכה, צד עורך, הכנסת תוכן, סיפור מקרה, בדיקות, בדיקות רפואיות, מתמחים, מתמחה, רופאים, רופא, בית חולים, רמבם" />
    <meta name="author" content="הילה פיטם ושירה פיקר" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=yes" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

    <%--CSS--%>
    <link href="Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="Styles/myStyle.css" rel="stylesheet" type="text/css" />
    <link rel="icon" type="image/png" href="/images/favicon.png" />

    <%--Scripts--%>
    <script src='http://code.jquery.com/jquery-3.3.1.slim.min.js'></script>
    <script src="jscripts/JavaScript_Editor.js"></script>
</head>
<body>
    <form id="form5" runat="server">

        <asp:Panel runat="server" CssClass="topNavPanel">
          <asp:Image CssClass="editorGameLogo" runat="server" ImageUrl="~/images/longlogo.png" />
        </asp:Panel>



            <nav>

                <asp:HyperLink class="navBtns" Enabled="false" runat="server" NavigateUrl="~/Cases.aspx">
                    <asp:Image CssClass="navImageBtn" runat="server" ImageUrl="~/images/casesd.svg" />
                    <figcaption>בדיקות רפואיות</figcaption>
                </asp:HyperLink>
                
                <asp:HyperLink class="navBtns" Enabled="false" runat="server" NavigateUrl="~/Participants.aspx">
                <asp:Image CssClass="navImageBtn" runat="server" ImageUrl="~/images/groupd.svg"/>
                    <figcaption>מתמחים</figcaption>
                </asp:HyperLink>



            </nav>

        
        <div runat="server" id="loginInnerDiv">


            <h1>משחק Smart-Quest</h1>
            <p>הזן שם משתמש וסיסמה על מנת לערוך, להוסיף ו/או למחוק תוכן אל המשחק Smart-Quest.</p>

            <asp:Label runat="server" CssClass="loginLabels" Text="שם משתמש" />
            <asp:TextBox ID="userNameTB" CssClass="CharacterCount" connectBtn="loginBtn" runat="server"></asp:TextBox>

            <br />

            <asp:Label runat="server" CssClass="loginLabels" Text="סיסמה" />
            <asp:TextBox ID="passwordTB" TextMode="Password" CssClass="CharacterCount" connectBtn="loginBtn" runat="server"></asp:TextBox>
            
            <asp:Label runat="server" ID="loginLabel" />
            
            <asp:Button runat="server" ID="loginBtn" Text="כניסה" OnClick="loginBtn_Click" />

        </div>



        <div>
        </div>
    </form>
</body>
</html>
