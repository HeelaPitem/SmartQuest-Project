using System;
using System.Web.UI.WebControls;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing;
using System.Web.UI;
using Image = System.Drawing.Image;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;



public partial class Cases : System.Web.UI.Page
{


    public class CreateItemTemplate : ITemplate
    {

        //Field to store the ListItemType value
        private ListItemType myListItemType;
        private List<string> newList;
        private int counter = 0;
        private string labelID;

        //Parameterrised constructor
        public CreateItemTemplate(ListItemType Item, List<string> contentList, string label)
        {
            myListItemType = Item;
            newList = contentList;
            labelID = label;
        }

        //Overwrite the InstantiateIn() function of the ITemplate interface.
        public void InstantiateIn(System.Web.UI.Control container)
        {
            //Code to create the ItemTemplate and its field.
            if (myListItemType == ListItemType.Item)
            {
                if (counter < newList.Count)
                {

                    Label gvLabel = new Label();
                    gvLabel.Text = newList[counter];


                    if (labelID == "caseID")
                    {
                        gvLabel.ID = "caseID" + counter;
                    }

                    container.Controls.Add(gvLabel);

                    counter++;
                }
            }
        }

    }


    XmlDocument myDoc = new XmlDocument();

    protected void Page_Init(object sender, EventArgs e)
    {
        //טעינת העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));


        XmlNodeList tests = myDoc.SelectNodes("//medicalTests/mdTest");    //שליפת שמות המחלקות הקיימות


        foreach (XmlNode myNode in tests)
        {

            ///---------------------יצירת אקורדיון--------------------//

            string testId = myNode.Attributes["mdTestId"].Value;

            //יצירת כפתור לאקורדיון שמות הבדיקות
            Panel testBtn = new Panel();
            testBtn.CssClass = "testBtns";
            testBtn.ID = "testBtn" + testId;
            testsPanel.Controls.Add(testBtn);

            //הדפסת שם הבדיקה
            HyperLink testName = new HyperLink();
            testName.CssClass = "testLinks";
            testName.ID = "testLink" + testId;
            testName.Text = Server.UrlDecode(myNode.InnerXml);
            testBtn.Controls.Add(testName);

            //יצירת תיבת טקסט לעריכת שם הבדיקה
            TextBox editTestName = new TextBox();
            editTestName.CssClass = "editTestName";
            editTestName.ID = "editTestName" + testId;
            editTestName.Text = Server.UrlDecode(myNode.InnerXml);
            testBtn.Controls.Add(editTestName);

            //יצירת כפתור עריכת שם הבדיקה
            ImageButton editImg = new ImageButton();
            editImg.ImageUrl = "~/images/edit.svg";
            editImg.CssClass = "editIcons";
            editImg.ID = "editImg" + testId;
            testBtn.Controls.Add(editImg);

            //יצירת כפתור לשמירת שינויים בשם הבדיקה
            ImageButton saveNameImg = new ImageButton();
            saveNameImg.ImageUrl = "~/images/save.svg";
            saveNameImg.ImageUrl = "~/images/save.svg";
            saveNameImg.CssClass = "saveNameImg";
            saveNameImg.ID = "saveNameImg" + testId;
            saveNameImg.Click += new ImageClickEventHandler(saveNameImg_Click);
            testBtn.Controls.Add(saveNameImg);

            //יצירת כפתור מחיקת בדיקה
            ImageButton deleteTestImg = new ImageButton();
            deleteTestImg.ImageUrl = "~/images/trash.svg";
            deleteTestImg.CssClass = "testIcons";
            deleteTestImg.ID = "deleteTestImg" + testId;
            deleteTestImg.Click += new ImageClickEventHandler(deleteTestImg_Click);
            testBtn.Controls.Add(deleteTestImg);

            //יצירת כפתור הוספת סיפור מקרה
            ImageButton addCaseImg = new ImageButton();
            addCaseImg.ImageUrl = "~/images/add.svg";
            addCaseImg.ID = "addCaseImg" + testId;
            addCaseImg.CssClass = "testIcons";
            addCaseImg.Click += new ImageClickEventHandler(addCaseImg_Click);
            testBtn.Controls.Add(addCaseImg);

            ///---------------------יצירת לייבל למקרה ואין סיפורי מקרה--------------------//

            Label noCasesLabel = new Label();
            noCasesLabel.Text = "לא נמצאו סיפורי מקרה השייכים לבדיקה זו.";
            noCasesLabel.CssClass = "noCasesLabel";
            noCasesLabel.ID = "noCasesLabel" + testId;
            testsPanel.Controls.Add(noCasesLabel);


            ///---------------------יצירת גריד-ויויים--------------------//

            ///יצירת גריד ויו באופן דינאמי
            GridView caseGridView = new GridView();
            caseGridView.ID = "caseGridView" + testId;
            caseGridView.CssClass = "caseGridView";
            caseGridView.DataSourceID = "newSource" + testId;
            caseGridView.AutoGenerateColumns = false;
            caseGridView.RowCommand += new GridViewCommandEventHandler(caseGridView_RowCommand);
            caseGridView.BackColor = System.Drawing.Color.White;
            caseGridView.BorderColor = System.Drawing.Color.Black;
            caseGridView.BorderWidth = 1;
            caseGridView.CellPadding = 3;


            testsPanel.Controls.Add(caseGridView);


            //יצירת מקור מידע לגריד ויו
            XmlDataSource newSource = new XmlDataSource();
            newSource.DataFile = "~/trees/XMLFile.xml";
            newSource.XPath = "//caseSudies/case[@mdTestId='" + testId + "']";
            newSource.ID = "newSource" + testId;
            testsPanel.Controls.Add(newSource);




            List<string> caseIDsList = new List<string>();

            XmlNodeList caseNodes = myDoc.SelectNodes("//caseSudies/case[@mdTestId='" + testId + "']");
            foreach (XmlNode caseNode in caseNodes)
            {
                string caseID = caseNode.Attributes["caseid"].Value;
                caseIDsList.Add(caseID);

            }


            //הוספת כותרת וערכים לעמודה
            TemplateField codefield = new TemplateField();
            codefield.HeaderText = "מספר מקרה";
            codefield.ControlStyle.Width = 60;
            codefield.ItemTemplate = new CreateItemTemplate(ListItemType.Item, caseIDsList, "caseID");


            List<string> caseNameList = new List<string>();

            XmlNodeList caseNameNodes = myDoc.SelectNodes("//caseSudies/case[@mdTestId='" + testId + "']/caseName");
            foreach (XmlNode caseNameNode in caseNameNodes)
            {
                string casename = Server.UrlDecode(caseNameNode.InnerXml);
                caseNameList.Add(casename);
            }

            //הוספת כותרת לעמודה
            TemplateField caseField = new TemplateField();
            caseField.HeaderText = "שם סיפור המקרה (אופציונלי)";
            caseField.ControlStyle.Width = 400;
            caseField.ItemTemplate = new CreateItemTemplate(ListItemType.Item, caseNameList, "caseName");


            List<string> catNameList = new List<string>();

            XmlNodeList catCasesNodes = myDoc.SelectNodes("//caseSudies/case[@mdTestId='" + testId + "']");

            foreach (XmlNode catCasesNode in catCasesNodes)
            {
                XmlNode catNameNode = myDoc.SelectSingleNode("//medicalTests/mdTest[@mdTestId='" + testId + "']");

                catNameList.Add(Server.UrlDecode(catNameNode.InnerXml));
            }


            TemplateField catFeild = new TemplateField();
            catFeild.HeaderText = "שם הבדיקה";
            catFeild.ControlStyle.Width = 250;
            catFeild.ItemTemplate = new CreateItemTemplate(ListItemType.Item, catNameList, "testName");



            //הוספת תמונת כפתור לעריכה של הסיפור מקרה
            ButtonField editCaseimg = new ButtonField();
            editCaseimg.ButtonType = ButtonType.Image;
            editCaseimg.ImageUrl = "~/images/edit.svg";
            editCaseimg.CommandName = "eRow";
            editCaseimg.HeaderText = "עריכה";
            editCaseimg.ControlStyle.CssClass = "caseIcons";


            //הוספת תמונת כפתור למחיקה של הסיפור מקרה
            ButtonField deleteCaseimg = new ButtonField();
            deleteCaseimg.ButtonType = ButtonType.Image;
            deleteCaseimg.ImageUrl = "~/images/trash.svg";
            deleteCaseimg.HeaderText = "מחיקה";
            deleteCaseimg.CommandName = "dRow";
            deleteCaseimg.ControlStyle.CssClass = "caseIcons";



            caseGridView.Columns.Add(codefield);
            caseGridView.Columns.Add(caseField);
            caseGridView.Columns.Add(catFeild);
            caseGridView.Columns.Add(editCaseimg);
            caseGridView.Columns.Add(deleteCaseimg);




        }

        string SelectedTest = SelectedTestHF.Value;

    }

    protected void Page_Load(object sender, EventArgs e)
    {


    }


    protected void caseGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        GridView gv = (GridView)sender;
        int rowNum = int.Parse(e.CommandArgument.ToString());
        string caseID = ((Label)gv.Rows[rowNum].Cells[0].FindControl("caseID" + rowNum + "")).Text;
        Session["editCaseID"] = caseID;


        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {

            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "dRow":
                deleteConf();
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "eRow":
                Response.Redirect("EditCase.aspx");
                break;

        }
    }



    void deleteConf()
    {
        //הצגה של המסך האפור
        grayWindows.Style.Add("display", "block");
        //הצגת הפופ-אפ של המחיקה
        DeleteConfPopUp.Style.Add("display", "block");
    }



    protected void YesDeleteCase_Click(object sender, EventArgs e)
    {
        //הסתרה של המסך האפור
        grayWindows.Style.Add("display", "none");

        XmlNode node = myDoc.SelectSingleNode("//caseSudies/case[@caseid='" + (string)Session["editCaseID"] + "']");
        node.ParentNode.RemoveChild(node);

        //שמירת שינויים בעץ
        myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        Response.Redirect("Cases.aspx");

    }


    protected void YesDeleteTest_Click(object sender, EventArgs e)
    {
        //הסתרה של המסך האפור
        grayWindows.Style.Add("display", "none");

        //מחיקת כל סיפורי המקרה הקשורים לבדיקה שנמחקה
        XmlNodeList myCases = myDoc.SelectNodes("//caseSudies/case[@mdTestId='" + (string)Session["selTestID"] + "']");

        foreach (XmlNode myCase in myCases)
        {
            myCase.ParentNode.RemoveChild(myCase);
        }

        //מחיקת הבדיקה מהעץ
        XmlNode myTest = myDoc.SelectSingleNode("//medicalTests/mdTest[@mdTestId='" + (string)Session["selTestID"] + "']");
        myTest.ParentNode.RemoveChild(myTest);


        //שמירת שינויים בעץ
        myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        Response.Redirect("Cases.aspx");

    }

    protected void NoDelete_Click(object sender, EventArgs e)
    {
        //הסתרה של המסך האפור
        grayWindows.Style.Add("display", "none");

        grayWindows1.Style.Add("display", "none");


    }

    protected void deleteTestImg_Click(object sender, EventArgs e)
    {
        //יביא את האלמנט שהפעיל את הפונקציה הזאת
        ImageButton i = (ImageButton)sender;
        var testName = i.ID;
        var testID = testName.Substring(13);
        Session["selTestID"] = testID;
        SelectedTestHF.Value = testID;


        //הצגה של המסך האפור
        grayWindows1.Style.Add("display", "block");

    }


    protected void addCaseImg_Click(object sender, EventArgs e)
    {

        ImageButton i = (ImageButton)sender;
        var testName = i.ID;
        var testID = testName.Substring(10);

        Session["editCaseID"] = null; //עדכון משתנה השומר את האי-די של סיפור מקרה למצב עריכה
        Session["newTestID"] = testID; //משתנה ששומר את מספר הבדיקה על מנת להוסיף אליה סיפוק מקרה חדש

        SelectedTestHF.Value = testID;

        Response.Redirect("EditCase.aspx");

    }


    protected void saveNameImg_Click(object sender, EventArgs e)
    {
        ImageButton i = (ImageButton)sender;
        var testName = i.ID;
        var testID = testName.Substring(11);

        var myNameTB = (TextBox)FindControl("editTestName" + testID);

        XmlNode myTest = myDoc.SelectSingleNode("//medicalTests/mdTest[@mdTestId='" + testID + "']");
        myTest.InnerXml = Server.UrlEncode(myNameTB.Text);

        //שמירת שינויים בעץ
        myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        Response.Redirect("Cases.aspx");

    }




    protected void addTestBtn_Click(object sender, EventArgs e)
    {
        //שליפה של מספר הבדיקות הקיימות
        XmlNode testCounter = myDoc.SelectSingleNode("//medicalTests/testsCounter");

        //יצירת אלמנט של בדיקה חדשה
        XmlElement testNode = myDoc.CreateElement("mdTest");
        testNode.SetAttribute("mdTestId", testCounter.InnerXml);
        testNode.InnerXml = Server.UrlEncode(newTestTB.Text);


        //עדכון מספר בדיקות
        int numofTests = Convert.ToInt16(testCounter.InnerXml);
        numofTests++;
        testCounter.InnerXml = numofTests.ToString();


        //הוספת בדיקה חדשה לעץ
        XmlNode firstcase = myDoc.SelectNodes("//medicalTests/mdTest").Item(0);
        myDoc.SelectSingleNode("//medicalTests").InsertBefore(testNode, firstcase);


        //שמירת שינויים בעץ
        myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        Response.Redirect("Cases.aspx");

    }
}