using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditCase : System.Web.UI.Page
{
    XmlDocument myDoc = new XmlDocument();

    protected void Page_Init(object sender, EventArgs e)
    {
        //הטענת העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));


        //שליפת והדפסת שמות הבדיקות הרפואיות הקיימות
        XmlNodeList myNodes2 = myDoc.SelectNodes("/game/medicalTests/mdTest");


        foreach (XmlNode myNode in myNodes2)
        {
            ListItem li = new ListItem(); //צור פריט חדש לרשימת דרופ דאוון
            li.Text = Server.UrlDecode(myNode.InnerXml);
            li.Value = Server.UrlDecode(myNode.Attributes["mdTestId"].Value);
            testNamesDDL.Items.Add(li);
        }

        if (Session["newTestID"] != null) //אם המשתמש לחץ על עריכה של סיפור מקרה שקיים
        {

            //שליפה והדפסה של שם הבדיקה
            XmlNode caseId = myDoc.SelectSingleNode("//caseSudies/caseCounter");

            //הדפסת מספר סיפור מקרה
            caseNum.InnerText = "סיפור מקרה #" + caseId.InnerXml;

            //בחירת קטגורית בדיקה
            testNamesDDL.SelectedValue = (string)Session["newTestID"];

            //סימון בדיקה כחיונית באופן דיפוטיבי
            answerRBL.SelectedValue = "true";

            numofCasesShownHF.Value = "1";

        }


        if (Session["editCaseID"] != null) //אם המשתמש לחץ על עריכה של סיפור מקרה שקיים
        {
            //--------------הדפסת תוכן סיפור המקרה---------------//

            //הדפסת מספר סיפור מקרה
            caseNum.InnerText = "סיפור מקרה #" + (string)Session["editCaseID"];

            //שליפה והדפסה של שם הבדיקה
            XmlNode caseId = myDoc.SelectSingleNode("//case[@caseid='" + (string)Session["editCaseID"] + "']/caseName");

            if (caseId != null)
            {
                testName.Text = Server.UrlDecode(caseId.InnerXml).ToString();
            }

            //שליפת סיפור המקרה הנוכחי
            XmlNode testId = myDoc.SelectSingleNode("//case[@caseid='" + (string)Session["editCaseID"] + "']");

            //בחירת קטגורית בדיקה
            testNamesDDL.SelectedValue = testId.Attributes["mdTestId"].Value;

            //האם הבדיקה חיונית או לא
            answerRBL.SelectedValue = testId.Attributes["answer"].Value;

            //הדפסת הסבר למשתמש במקרה של אי הצלחה
            XmlNode exp = myDoc.SelectSingleNode("//case[@caseid='" + (string)Session["editCaseID"] + "']/answerExp");

            expTB.Text = Server.UrlDecode(exp.InnerXml);


        }



        testNamesDDL.DataBind();


    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack) //טעינת העמוד בפעם הראשונה
        {

            int casesCount = 1;


            if (Session["editCaseID"] != null) //אם המשתמש לחץ על עריכה של סיפור מקרה שקיים
            {

                XmlNodeList myNodes = myDoc.SelectNodes("//caseSudies//case[@caseid='" + (string)Session["editCaseID"] + "']/patientDetails");
                numofCasesShownHF.Value = ((myNodes.Count) + 1).ToString();



                foreach (XmlNode myNode in myNodes)  //שליפת תוכן פרטי המטופל
                {

                    var title = Server.UrlDecode(myNode["title"].InnerXml);
                    var content = Server.UrlDecode(myNode["content"].InnerXml);


                    var myTitle = (TextBox)FindControl("titleTB" + casesCount);
                    myTitle.Text = title;
                    myTitle.CssClass = myTitle.CssClass.Replace("grayborderbottom", "CharacterCount");
                    myTitle.ReadOnly = false;


                    var mycontent = (TextBox)FindControl("contentTB" + casesCount);
                    mycontent.Text = content;
                    mycontent.ReadOnly = false;
                    mycontent.CssClass = mycontent.CssClass.Replace("cursornotallowed", "CharacterCount");


                    var myPanel = ((Panel)FindControl("patientPanel" + casesCount));
                    myPanel.CssClass = myPanel.CssClass.Replace("patientPanel", "").Trim();
                    myPanel.CssClass = "activepatientPanel";


                    var myLabel = ((Label)FindControl("numberLabel" + casesCount));
                    myLabel.CssClass = myLabel.CssClass.Replace("grayfont", "numbersLabel");



                    casesCount++;
                }


            }


                ((Button)FindControl("saveBtn")).Attributes.Add("disabled", "true");


            EnableEmptyPatientPanel(casesCount);

        }



    }


    private void EnableEmptyPatientPanel(int id)
    {
        //השארת פאנל אחד במצב פעיל להוספת פרטי המטופל
        var myNewTitle = (TextBox)FindControl("titleTB" + id);
        myNewTitle.CssClass = myNewTitle.CssClass.Replace("grayborderbottom", "CharacterCount");
        myNewTitle.ReadOnly = false;


        var myNewcontent = (TextBox)FindControl("contentTB" + id);
        myNewcontent.ReadOnly = false;
        myNewcontent.CssClass = myNewcontent.CssClass.Replace("cursornotallowed", "CharacterCount");


        var myNewPanel = ((Panel)FindControl("patientPanel" + id));
        myNewPanel.CssClass = myNewPanel.CssClass.Replace("patientPanel", "").Trim();
        myNewPanel.CssClass = "activepatientPanel";

        var myNewLabel = ((Label)FindControl("numberLabel" + id));
        myNewLabel.CssClass = myNewLabel.CssClass.Replace("grayfont", "numbersLabel");
    }



    protected void saveBtn_Click(object sender, EventArgs e)
    {

        //הטענת העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));


        if (Session["editCaseID"] != null) //אם המשתמש לחץ על עריכה של סיפור מקרה שקיים
        {
            //עדכון שם הסיפור מקרה
            XmlNode caseName = myDoc.SelectSingleNode("//case[@caseid='" + (string)Session["editCaseID"] + "']/caseName");
            caseName.InnerXml = Server.UrlEncode(testName.Text);

            //שליפת הסיפור מקרה הנוכחי
            XmlNode currnetCase = myDoc.SelectSingleNode("//case[@caseid='" + (string)Session["editCaseID"] + "']");

            //עדכון קטגוריה של סיפור המקרה
            currnetCase.Attributes["mdTestId"].Value = testNamesDDL.SelectedValue;

            //עדכון תשובה (נכון או לא נכון) של סיפור המקרה
            currnetCase.Attributes["answer"].Value = answerRBL.SelectedValue;

            //עדכון הסבר לסיפור מקרה למקרה של אי-הצלחה 
            XmlNode exp = myDoc.SelectSingleNode("//case[@caseid='" + (string)Session["editCaseID"] + "']/answerExp");
            exp.InnerXml = Server.UrlEncode(expTB.Text);


            //מחיקת נתוני המטופל הקיימים כרגע בעץ
            XmlNodeList patientDetails = myDoc.SelectNodes("//case[@caseid='" + (string)Session["editCaseID"] + "']/patientDetails");

            foreach (XmlNode patientDetail in patientDetails)
            {
                patientDetail.ParentNode.RemoveChild(patientDetail);
            }


            for (int t = 1; t < 6; t++)
            {
                if (((Panel)FindControl("patientPanel" + t)) != null)
                {
                    if (((TextBox)FindControl("contentTB" + t)).Text != "") //רק אם יש תוכן בתוך הפאנלים
                    {
                        XmlElement patientDetailsNode = myDoc.CreateElement("patientDetails");

                        XmlElement titleNode = myDoc.CreateElement("title");
                        titleNode.InnerXml = Server.UrlEncode(((TextBox)FindControl("titleTB" + t)).Text);
                        patientDetailsNode.AppendChild(titleNode);

                        XmlElement contentNode = myDoc.CreateElement("content");
                        contentNode.InnerXml = Server.UrlEncode(((TextBox)FindControl("contentTB" + t)).Text);
                        patientDetailsNode.AppendChild(contentNode);

                        //הוספת הפריט לעץ
                        currnetCase.AppendChild(patientDetailsNode);
                    }
                }
            }
        }

        if (Session["newTestID"] != null) //אם המשתמש לחץ על הוספת סיפור מקרה חדש
        {

            // שליפה של מספר סיפורי המקרה
            XmlNode caseId = myDoc.SelectSingleNode("//caseSudies/caseCounter");

            //יצירת אלמנט של סיפור מקרה חדש
            XmlElement caseNode = myDoc.CreateElement("case");
            caseNode.SetAttribute("caseid", caseId.InnerXml.ToString());
            caseNode.SetAttribute("mdTestId", testNamesDDL.SelectedValue);
            caseNode.SetAttribute("answer", answerRBL.SelectedValue);

            //יצירת אלמנט לשם הסיפור מקרה
            XmlElement caseNameNode = myDoc.CreateElement("caseName");
            caseNameNode.InnerXml = Server.UrlEncode(testName.Text);
            caseNode.AppendChild(caseNameNode);

            //יצירת אלמנט להסבר במקרה של אי הצלחה
            XmlElement expNode = myDoc.CreateElement("answerExp");
            expNode.InnerXml = Server.UrlEncode(expTB.Text);
            caseNode.AppendChild(expNode);

            //עדכון מספר סיפורי מקרה
            int numofCases = Convert.ToInt16(caseId.InnerXml);
            numofCases++;
            caseId.InnerXml = numofCases.ToString();


            for (int t = 1; t < 6; t++)
            {
                if (((Panel)FindControl("patientPanel" + t)) != null)
                {
                    if (((TextBox)FindControl("contentTB" + t)).Text != "") //רק אם יש תוכן בתוך הפאנלים
                    {
                        XmlElement patientDetailsNode = myDoc.CreateElement("patientDetails");

                        XmlElement titleNode = myDoc.CreateElement("title");
                        titleNode.InnerXml = Server.UrlEncode(((TextBox)FindControl("titleTB" + t)).Text);
                        patientDetailsNode.AppendChild(titleNode);

                        XmlElement contentNode = myDoc.CreateElement("content");
                        contentNode.InnerXml = Server.UrlEncode(((TextBox)FindControl("contentTB" + t)).Text);
                        patientDetailsNode.AppendChild(contentNode);

                        //הוספת הפריט לעץ
                        caseNode.AppendChild(patientDetailsNode);

                    }
                }
            }

            //הוספת פריט חדש לעץ
            XmlNode firstcase = myDoc.SelectNodes("//caseSudies/case").Item(0);

            myDoc.SelectSingleNode("//caseSudies").InsertBefore(caseNode, firstcase);


        }

        //שמירת שינויים בעץ
        myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        //ניתוה בעמוד לעמוד הקודם
        Response.Redirect("Cases.aspx");

    }

    protected void cancelBtn_Click(object sender, EventArgs e)
    {
        //ניתוה בעמוד לעמוד הקודם
        Response.Redirect("Cases.aspx");

    }



    protected void confYesDelete_Click(object sender, EventArgs e)
    {
        int btnid = Convert.ToInt16(ClickedDeleteBtnHF.Value);

    }
}