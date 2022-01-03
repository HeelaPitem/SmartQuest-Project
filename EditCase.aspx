<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditCase.aspx.cs" Inherits="EditCase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Smart-Quest | עריכת סיפור מקרה</title>

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


    <form id="form4" runat="server">

        <asp:Panel runat="server" CssClass="topNavPanel">
          <asp:Image CssClass="editorGameLogo" runat="server" ImageUrl="~/images/longlogo.png" />
        </asp:Panel>

            <nav>
                <asp:HyperLink class="navBtns selectedNav" runat="server" NavigateUrl="~/Cases.aspx">
                    <asp:Image CssClass="navImageBtn" runat="server" ImageUrl="~/images/cases.svg" />
                    <figcaption>בדיקות רפואיות</figcaption>
                </asp:HyperLink>
                
                <asp:HyperLink class="navBtns" runat="server" NavigateUrl="~/Participants.aspx">
                <asp:Image CssClass="navImageBtn" runat="server" ImageUrl="~/images/group.svg"/>
                    <figcaption>מתמחים</figcaption>
                </asp:HyperLink>
            </nav>


        <asp:HiddenField runat="server" ID="panelIDSaver" />
        <asp:HiddenField runat="server" ID="numofCasesShownHF" />
        <asp:HiddenField runat="server" ID="casesCountHF" />
        <asp:HiddenField runat="server" ID="PostBackHF" />
        <asp:HiddenField runat="server" ID="ClickedDeleteBtnHF" />
        <asp:HiddenField runat="server" ID="addPanelClicked" />
        <asp:HiddenField runat="server" ID="changesMadeHF" />
        <asp:HiddenField runat="server" ID="fullTBcounterHT" />

        



                <asp:Panel ID="grayWindows" CssClass="grayWindow" runat="server">
                <!-- פופ-אפ למחיקת משחק -->
                <asp:Panel ID="DeleteConfPopUp0" CssClass="bounceInDown PopUp" runat="server">
                    <asp:Label ID="Label1" runat="server" Text="האם בטוח תרצה למחוק פריט זה?"></asp:Label>
                    <br />
                    <asp:Button ID="confYesDelete" runat="server" Text="מחק" OnClick="confYesDelete_Click" />
                    <asp:Button ID="confnNoDelete" runat="server" Text="בטל"/>
                </asp:Panel>
            </asp:Panel>





        <div id="form4container">

        <h1 id="caseNum" runat="server"></h1>

        <asp:Label runat="server">שם סיפור המקרה (אופציונלי):</asp:Label>
        <asp:TextBox ID="testName" CssClass="CharacterCount" item="2" CharacterLimit="40" connectBtn="saveBtn" runat="server"></asp:TextBox>
        <asp:Label ID="LabelCounter2" CssClass="LabelCounter" runat="server" Text="0/40"></asp:Label>

        <br />

        <asp:Label runat="server">שם הבדיקה</asp:Label>
        <asp:DropDownList ID="testNamesDDL" runat="server"></asp:DropDownList>

        <br />


        <h2>פרטי המטופל</h2>
            <asp:Label ID="patientDetailsInsLabel" runat="server" Text="הזן בלפחות 2 שקופיות: כותרת עד 20 תווים (אופציונלי), ותוכן עד 100 תווים (חובה)."></asp:Label>


            <div id="patientDiv">

            
        <asp:Panel runat="server" ID="patientDetailsPanel">

            <asp:Panel ID="patientPanel1" CssClass="activepatientPanel" runat="server">
                <asp:TextBox ID="titleTB1" CssClass="titleTB CharacterCount" CharacterLimit="20" runat="server"></asp:TextBox>
                <asp:Label ID="numberLabel1" CssClass="numbersLabel" runat="server" Text="1"></asp:Label>
                <asp:TextBox ID="contentTB1" CssClass="contentTB CharacterCount" connectBtn="saveBtn" CharacterLimit="100" TextMode="MultiLine" runat="server"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="patientPanel2" CssClass="patientPanel" runat="server">
                <asp:TextBox ID="titleTB2" ReadOnly="true" CssClass="titleTB grayborderbottom" CharacterLimit="20" runat="server"></asp:TextBox>
                <asp:Label ID="numberLabel2" CssClass="grayfont" runat="server" Text="2"></asp:Label>
                <asp:TextBox ID="contentTB2" ReadOnly="true" CssClass="contentTB cursornotallowed CharacterCount" connectBtn="saveBtn" CharacterLimit="100" TextMode="MultiLine" runat="server"></asp:TextBox>
            </asp:Panel>

            <asp:Panel ID="patientPanel3" CssClass="patientPanel" runat="server">
                <asp:TextBox ID="titleTB3" ReadOnly="true" CssClass="titleTB grayborderbottom" CharacterLimit="20" runat="server"></asp:TextBox>
                <asp:Label ID="numberLabel3" CssClass="grayfont" runat="server" Text="3"></asp:Label>
                <asp:TextBox ID="contentTB3" ReadOnly="true" CssClass="contentTB cursornotallowed CharacterCount" connectBtn="saveBtn" CharacterLimit="100" TextMode="MultiLine" runat="server"></asp:TextBox>
            </asp:Panel>

            <asp:Panel ID="patientPanel4" CssClass="patientPanel" runat="server">
                <asp:TextBox ID="titleTB4" ReadOnly="true" CssClass="titleTB grayborderbottom" CharacterLimit="20" runat="server"></asp:TextBox>
                <asp:Label ID="numberLabel4" CssClass="grayfont" runat="server" Text="4"></asp:Label>
                <asp:TextBox ID="contentTB4" ReadOnly="true" CssClass="contentTB cursornotallowed CharacterCount" connectBtn="saveBtn" CharacterLimit="100" TextMode="MultiLine" runat="server"></asp:TextBox>
            </asp:Panel>

           <asp:Panel ID="patientPanel5" CssClass="patientPanel" runat="server">
                <asp:TextBox ID="titleTB5" ReadOnly="true" CssClass="titleTB grayborderbottom" CharacterLimit="20" runat="server"></asp:TextBox>
                <asp:Label ID="numberLabel5" CssClass="grayfont" runat="server" Text="5"></asp:Label>
                <asp:TextBox ID="contentTB5" ReadOnly="true" CssClass="contentTB cursornotallowed CharacterCount" connectBtn="saveBtn" CharacterLimit="100" TextMode="MultiLine" runat="server"></asp:TextBox>
            </asp:Panel>

        </asp:Panel>


<%--        <asp:LinkButton ID="addNewPatientPanel" runat="server" OnClick="addNewPatientPanel_Click">
            <asp:Image runat="server" ImageUrl= "~/images/add.svg" CssClass="addimg" Width= "50px" />
            <asp:Label ID= "addLabel" runat="server" Text = "הוספת פריט מידע חדש"/>
            </asp:LinkButton>--%>

 </div>

        <h2>תגובה ומשוב</h2>

        <asp:Label runat="server" Text="האם הבדיקה חיונית במקרה זה?"></asp:Label>
                <asp:RadioButtonList ID="answerRBL" runat="server">
                    <asp:ListItem Value="true">חיונית</asp:ListItem>
                    <asp:ListItem Value="false">לא חיונית</asp:ListItem>
                </asp:RadioButtonList>

        <br />

        <asp:Label runat="server" Text="משוב והסבר למתמחה במקרה של תשובה לא נכונה (אופציונלי): "></asp:Label>
            <br />
        <asp:TextBox ID="expTB" CssClass="CharacterCount" item="3" CharacterLimit="100" connectBtn="saveBtn" runat="server"></asp:TextBox>

         <asp:Label ID="LabelCounter3" CssClass="LabelCounter" runat="server" Text="0/100"></asp:Label>

 
        <br />

<%--<input id="saveBtn" type="button" value="שמור וחזור" onclick="saveBtn_Click" /> --%>

        <asp:Button ID="saveBtn" runat="server" Text="שמור וחזור" OnClick="saveBtn_Click"/>
        <asp:Button ID="cancelBtn" runat="server" Text="בטל וחזור" OnClick="cancelBtn_Click"/>
        
        </div>
    </form>
</body>
</html>
