<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cases.aspx.cs" Inherits="Cases" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />

    <title>Smart-Quest | בדיקות וסיפורי מקרה</title>

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
    <form id="form2" runat="server">

        <asp:Panel runat="server" CssClass="topNavPanel">
          <asp:Image CssClass="editorGameLogo" runat="server" ImageUrl="~/images/longlogo.png" />
        </asp:Panel>

            <nav>
                <asp:HyperLink ID="casesNavBtn" class="navBtns selectedNav" runat="server" NavigateUrl="~/Cases.aspx">
                    <asp:Image CssClass="navImageBtn" runat="server" ImageUrl="~/images/cases.svg" />
                    <figcaption>בדיקות רפואיות</figcaption>
                </asp:HyperLink>
                
                <asp:HyperLink ID="ParticipantsNavBtn" class="navBtns" runat="server" NavigateUrl="~/Participants.aspx">
                <asp:Image CssClass="navImageBtn" runat="server" ImageUrl="~/images/group.svg"/>
                    <figcaption>מתמחים</figcaption>
                </asp:HyperLink>
            </nav>

        <div id="form2container">

            <p>הוספת בדיקה רפואית חדשה: </p>

            <asp:TextBox ID="newTestTB" CssClass="CharacterCount" item="1" CharacterLimit="30" connectBtn="addTestBtn" runat="server"></asp:TextBox>

            <asp:Button ID="addTestBtn" runat="server" Text="הוספת בדיקה" Enabled="false" OnClick="addTestBtn_Click" />
            
            <asp:Label ID="LabelCounter1" CssClass="LabelCounter" runat="server" Text="0/30"></asp:Label>

            
            <h2>סיפורי מקרה</h2>
            <p>לחץ על בדיקה כדי לחשוף את סיפורי המקרה.</p>

            <asp:Panel ID="testsPanel" runat="server">
                 <asp:Literal ID="DynamicTable" runat="server"></asp:Literal>

            </asp:Panel>



            <asp:Panel ID="grayWindows" CssClass="grayWindow" runat="server">
                <!-- פופ-אפ למחיקת משחק -->
                <asp:Panel ID="DeleteConfPopUp" CssClass="bounceInDown PopUp" runat="server">
                    <asp:Label runat="server" Text="האם אתה בטוח שתרצה למחוק סיפור מקרה זה?"></asp:Label>
                    <br />
                    <asp:Button ID="confYesDelete" CssClass="confYesDelete" runat="server" Text="מחק" OnClick="YesDeleteCase_Click" />
                    <asp:Button ID="confnNoDelete" CssClass="confNoDelete" runat="server" Text="בטל" OnClick="NoDelete_Click" />
                </asp:Panel>
            </asp:Panel>


             <asp:Panel ID="grayWindows1" CssClass="grayWindow" runat="server">
                <!-- פופ-אפ למחיקת משחק -->
                <asp:Panel ID="DeleteConfPopUp1" CssClass="bounceInDown PopUp" runat="server">
                    <asp:Label runat="server" Text="האם אתה בטוח שתרצה למחוק בדיקה זו יחד עם כל סיפורי המקרה בתוכה?"></asp:Label>
                    <br />
                    <asp:Button runat="server" CssClass="confYesDelete" Text="מחק" OnClick="YesDeleteTest_Click" />
                    <asp:Button runat="server" CssClass="confNoDelete" Text="בטל" OnClick="NoDelete_Click" />
                </asp:Panel>
            </asp:Panel>

            <asp:HiddenField ID="SelectedTestHF" runat="server" />


        </div>
    </form>
</body>
</html>
