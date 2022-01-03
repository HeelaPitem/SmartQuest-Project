<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Participants.aspx.cs" Inherits="Participants" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Smart-Quest | משתתפים</title>

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


    <form id="form3" runat="server">

        <asp:Panel runat="server" CssClass="topNavPanel">
            <asp:Image CssClass="editorGameLogo" runat="server" ImageUrl="~/images/longlogo.png" />
        </asp:Panel>


        <nav>
            <asp:HyperLink ID="casesNavBtn" class="navBtns" runat="server" NavigateUrl="~/Cases.aspx">
                    <asp:Image CssClass="navImageBtn" runat="server" ImageUrl="~/images/cases.svg" />
                    <figcaption>סיפורי מקרה</figcaption>
            </asp:HyperLink>

            <asp:HyperLink ID="ParticipantsNavBtn" class="navBtns selectedNav" runat="server" NavigateUrl="~/Participants.aspx">
                <asp:Image CssClass="navImageBtn" runat="server" ImageUrl="~/images/group.svg"/>
                    <figcaption>מתמחים</figcaption>
            </asp:HyperLink>
        </nav>


        <%-- גריד-ויו להדפסת פרטי משחקים מהעץ --%>
        <asp:GridView ID="ParticipantsGridView" runat="server" DataSourceID="XmlDataSource2" OnRowCommand="ParticipantsGridView_RowCommand" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Style="margin-left: 0px" Width="700px">

            <Columns>



                <asp:TemplateField HeaderText="קוד משתתף">
                    <ItemTemplate>
                        <asp:Label CssClass="Code" ToolTip="קוד אישי" Width="100px" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@userid")%>'> </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="שם מלא">
                    <ItemTemplate>
                        <asp:Label Width="280px" runat="server" Text='<%# Server.UrlDecode( XPathBinder.Eval(Container.DataItem, "username").ToString())%>'> </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="מחלקה">
                    <ItemTemplate>
                        <asp:Label ID="departmentLabel" Width="100px" runat="server" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@userid")%>' Text='<%#XPathBinder.Eval(Container.DataItem, "@userDepartment")%>'> </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="מייל">
                    <ItemTemplate>
                        <asp:Label CssClass="mailGV" ToolTip="קוד אישי" Width="340px" runat="server" Text='<%# Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "useremail").ToString())%>'> </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="מחק">
                    <ItemTemplate>
                        <asp:ImageButton ID="deleteImageButton" CssClass="deleteParBtn" Width="25px" CommandName="deleteRow" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@userid")%>' runat="server" ImageUrl="~/images/trash.svg" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>


            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle Height="10px" ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />

        </asp:GridView>
        <br />
        <asp:XmlDataSource ID="XmlDataSource2" runat="server" DataFile="~/trees/XMLFile.xml" XPath="//users/user"></asp:XmlDataSource>
        <br />

        <asp:Panel ID="grayWindows0" CssClass="grayWindow" runat="server">
            <!-- פופ-אפ למחיקת משחק -->
            <asp:Panel ID="DeleteConfPopUp0" CssClass="bounceInDown PopUp" runat="server">
                <asp:Label ID="Label1" runat="server" Text="האם אתה בטוח שתרצה למחוק משתמש זה?"></asp:Label>
                <br />
                <asp:Button ID="confYesDelete0" CssClass="confYesDelete" runat="server" Text="מחק" OnClick="confYesDelete_Click" />
                <asp:Button ID="confnNoDelete0" CssClass="confNoDelete" runat="server" Text="בטל" OnClick="confnNoDelete_Click" />
            </asp:Panel>
        </asp:Panel>

        <div>
        </div>
    </form>
</body>
</html>
