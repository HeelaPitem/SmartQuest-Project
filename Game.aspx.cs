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

public partial class _Default : System.Web.UI.Page
{

    XmlDocument myDoc = new XmlDocument();
    bool rnd = false;
    int rndNum = 1;
    bool usersuccess; //משתנה השומר האם המשתמש הצליח או לא

    //הגדרת נתיב לשמירת הקובץ
    string imagesLibPath = "uploadedFiles/";
    string imageNewName;


    protected void Page_Load(object sender, EventArgs e)
    {

        //הטענת העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));


        if (!IsPostBack) // אם המשתמש נכנס לאתר בפעם הראשונה
        {

            //---------------------הדפסת שמות המחלקות אל דף הפרופיל----------------//

            XmlNodeList myNodes3 = myDoc.SelectNodes("//departments/department");    //שליפת שמות המחלקות הקיימות

            foreach (XmlNode myNode in myNodes3)
            {
                ListItem DepartmentItems = new ListItem(); //צור פריט חדש לרשימת דרופ דאוון
                DepartmentItems.Text = Server.UrlDecode(myNode.InnerXml);
                DepartmentItems.Value = Server.UrlDecode(myNode.Attributes["depID"].Value);
                departmentDD.Items.Add(DepartmentItems);
            }

            departmentDD.DataBind();

            ViewState["selCategory"] = "0";

            NavHiddenField.Value = "01";

        }
        else
        {

            if (NavHiddenField.Value == "2")
            {
                catFinishedLabel.Text = "";
                MedicalTestsDDL();

            }

            //------------------- הדפסת גרף ניקוד לפי מחלקות לעמוד משוב ולעמוד תוצאות--------------//

            if (NavHiddenField.Value == "3" || NavHiddenField.Value == "GameFeedback") //במידה והמשתמש בעמוד תוצאות או משוב
            {

                XmlNodeList myNodes4 = myDoc.SelectNodes("//departments/department"); ////שליפת שמות המחלקות הקיימות

                double MaxNum = 0;

                foreach (XmlNode myNode in myNodes4)
                {
                    if (Convert.ToInt16(Server.UrlDecode(myNode.Attributes["depScore"].Value)) > MaxNum)
                    {
                        MaxNum = Convert.ToInt16(Server.UrlDecode(myNode.Attributes["depScore"].Value));
                    }
                }


                int a = 0;

                for (int i = Convert.ToInt16(MaxNum); i >= 0; i--)
                {
                    foreach (XmlNode myNode in myNodes4)
                    {

                        if (Convert.ToInt16(Server.UrlDecode(myNode.Attributes["depScore"].Value)) == i)
                        {
                            double currentScore = Convert.ToDouble(Server.UrlDecode(myNode.Attributes["depScore"].Value));

                            double WidthPercentage = (currentScore / MaxNum) * 95;

                            if (WidthPercentage < 33)
                            {
                                WidthPercentage = 33;
                            }


                            string[] colors = { "lightskyblue", "cornflowerblue", "mediumslateblue", "blueviolet", "darkslateblue" };

                            Label DepartmentScoreGraph = new Label(); //צור פריט חדש לרשימת דרופ דאוון
                            DepartmentScoreGraph.Text = Server.UrlDecode(myNode.InnerXml) + " (" + Server.UrlDecode(myNode.Attributes["depScore"].Value) + ")";
                            DepartmentScoreGraph.BackColor = Color.FromName(colors[a]);
                            DepartmentScoreGraph.CssClass = "depDiv";
                            DepartmentScoreGraph.Width = Unit.Percentage(WidthPercentage);

                            if (NavHiddenField.Value == "GameFeedback")
                            {
                                FindControl("departmentsDiv").Controls.Add(DepartmentScoreGraph);
                            }
                            if (NavHiddenField.Value == "3")
                            {
                                FindControl("depScoresDiv").Controls.Add(DepartmentScoreGraph);
                            }

                            a++;
                        }


                    }
                }

                //-------------------שמירת נתוני הסיפורי מקרה ונתוני המשתמש להצגת הגרפים בעמוד תוצאות---------------//

                //שליפת כמות הסיפורי מקרה שקיימים במערכת
                XmlNodeList myNodes5 = myDoc.SelectNodes("//caseSudies/case");

                int sumCases = myNodes5.Count; //משתנה השומר את מספר סיפורי המקרה במשחק

                CasesCountHiddenField.Value = sumCases.ToString(); // שמירת הנתון אל שדה מוחבר


                //שליפת כמות הסיפורי מקרה שהמשתמש ענה עליהם בצורה נכונה
                XmlNodeList CorrectCasesAnswered = myDoc.SelectNodes("//users/user[@userid='" + (string)Session["selUserID"] + "']/caseID[@answeredCorrect='True']");

                int CorrectCases = CorrectCasesAnswered.Count;

                CorrectCasesHiddenField.Value = CorrectCases.ToString();


                //שליפת כמות הסיפורי מקרה שהמשתמש ענה עליהם בצורה לא נכונה
                XmlNodeList IncorrectCasesAnswered = myDoc.SelectNodes("//users/user[@userid='" + (string)Session["selUserID"] + "']/caseID[@answeredCorrect='False']");

                int IncorrectCases = IncorrectCasesAnswered.Count;

                IncorrectCasesHiddenField.Value = IncorrectCases.ToString();

            }



        }

        //----------עדכון עמוד הפרופיל לאחר כניסה למערכת------------//
        if (NavHiddenField.Value == "0") //אם המשתמש נרשם/ נכנס למערכת בהצלחה
        {
            if (UserLoggedInHiddenField.Value == "true") //אם המשתמש נכנס למערכת
            {
                fullNameTB.AutoPostBack = true;
                emailTB.AutoPostBack = true;
                departmentDD.AutoPostBack = true;
            }
            else
            {
                fullNameTB.AutoPostBack = false;
                emailTB.AutoPostBack = false;
                departmentDD.AutoPostBack = false;
            }

            infoLabel.Text = "*לצורך זיהוי בלבד"; //עדכון הודעה למשתמש
            infoLabel1.Text = "*לצורך זיהוי בלבד"; //עדכון הודעה למשתמש


            if (SignInLink.CssClass == "bounce-4")
            {
                SignInLink.CssClass = SignInLink.CssClass.Replace("bounce-4", "").Trim();
            }
            if (infoLabel.CssClass == "red")
            {
                infoLabel.CssClass = infoLabel.CssClass.Replace("red", "").Trim();
            }
            if (SignUpLink.CssClass == "bounce-4")
            {
                SignUpLink.CssClass = SignUpLink.CssClass.Replace("bounce-4", "").Trim();
                infoLabel1.CssClass = infoLabel1.CssClass.Replace("red", "").Trim();
            }

        }

        if (UserLoggedInHiddenField.Value == "true" && NavHiddenField.Value == "0") //אם המשתמש נרשם/ נכנס למערכת בהצלחה ונמצא בעמוד פרופיל
        {

            UpdateProfilePic();

        }
        else //אם המשתמש עוד לא נכנס למערכת בהצלחה
        {
            SavedLabel.Attributes.Add("style", "display:none");
        }



    }

    private void UpdateProfilePic()
    {


        XmlNode userID = myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["selUserID"] + "']/userImage");

        if (userID == null)
        {
            profilePicBtn.ImageUrl = "~/images/user.svg";

            addPicHyperLink.Text = "הוספת תמונת פרופיל";

        }
        else
        {
            string UserPic = Server.UrlDecode(userID.InnerXml);
            profilePicBtn.ImageUrl = UserPic;

            addPicHyperLink.Text = "עדכון תמונת פרופיל";

        }


    }


    private void MedicalTestsDDL()
    {
        ViewState["selCategory"] = chooseCatDDL.SelectedValue;


        //----------------הוצאת מספרי סיפורי המקרה שהמשתמש כבר ענה עליהם בצורה נכונה-----------------//

        XmlNodeList myCases = myDoc.SelectNodes("//users/user[@userid='" + (string)Session["selUserID"] + "']/caseID[@answeredCorrect='True']");

        List<int> ex = new List<int>(); //רשימת מספרי סיפורי המקרה שהמשתמש ענה עליהם או לא רלוונטים לקטגוריה שהמשתמש בחר

        foreach (XmlNode myNode in myCases)
        {
            if (myNode != null) //אם יש סיפורי מקרה שהוא ענה עליהם בצורה נכונה
            {
                ex.Add(Convert.ToInt32(myNode.InnerXml));
            }

        }


        //---------------------הדפסת שמות הבדיקות אל עמוד המשחק----------------//

        chooseCatDDL.Items.Clear();//איפוס הפריטים שבתוך כפתור הדרופ-דאוון

        ListItem listItem = new ListItem(); //צור פריט חדש לרשימת דרופ דאוון
        listItem.Text = ("כל הבדיקות");        //הוספת אופציה ראשונה להיבחן על כל הבדיקות
        listItem.Value = "0";
        chooseCatDDL.Items.Add(listItem);



        ////שליפת שמות הבדיקות הרפואיות הקיימות
        XmlNodeList myNodes2 = myDoc.SelectNodes("/game/medicalTests/mdTest");


        foreach (XmlNode myNode in myNodes2)
        {

            int catNum = Convert.ToInt16(myNode.Attributes["mdTestId"].Value);

            int caseCounter = 0;

            XmlNodeList myNodes3 = myDoc.SelectNodes("/game//caseSudies/case[@mdTestId='" + catNum + "']");

            foreach (XmlNode myNode1 in myNodes3)
            {
                if (ex.Contains(Convert.ToInt32(myNode1.Attributes["caseid"].Value)))
                {

                }
                else
                {
                    caseCounter++;
                }
            }

            string caseString = caseCounter.ToString();
            ListItem li = new ListItem(); //צור פריט חדש לרשימת דרופ דאוון
            li.Text = Server.UrlDecode(myNode.InnerXml) + " (" + caseString + ")";
            if (caseCounter == 0)
            {
                li.Attributes.Add("disabled", "true");
            }
            li.Value = Server.UrlDecode(myNode.Attributes["mdTestId"].Value);
            chooseCatDDL.Items.Add(li);
        }

        chooseCatDDL.DataBind();

    }

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void chooseCat_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        NavHiddenField.Value = "2";
    }

    protected void startGameButton_Click(object sender, EventArgs e)
    {

        ViewState["selCategory"] = chooseCatDDL.SelectedValue;

        //הגדרת ניווט אל עמוד המשחק
        NavHiddenField.Value = "GameStarted";

        //הטענת העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));

        //הגדרת משתנה עם מספר הקטגוריה שנבחרה
        int selectedCtg = Convert.ToInt32(ViewState["selCategory"]);

        //----------------הוצאת מספרי סיפורי המקרה שהמשתמש כבר ענה עליהם בצורה נכונה-----------------//

        XmlNodeList myCases = myDoc.SelectNodes("//users/user[@userid='" + (string)Session["selUserID"] + "']/caseID[@answeredCorrect='True']");

        List<int> ex = new List<int>(); //רשימת מספרי סיפורי המקרה שהמשתמש ענה עליהם או לא רלוונטים לקטגוריה שהמשתמש בחר

        foreach (XmlNode myNode in myCases)
        {
            if (myNode != null) //אם יש סיפורי מקרה שהוא ענה עליהם בצורה נכונה
            {
                ex.Add(Convert.ToInt32(myNode.InnerXml));
            }
        }



        //-------------------מציאת מספרי סיפורי המקרה הרלוונטים למשתמש והכנסת לרשימה-----------------------//

        List<int> caseIdOptions = new List<int>(); //רשימת מספרי סיפורי המקרה שרלוונטים

        bool IdOkay = true;

        if (selectedCtg != 0) //אם המשתמש בחר קטגוריה ספציפית לתרגל
        {
            XmlNodeList casesByCategory = myDoc.SelectNodes("//caseSudies/case[@mdTestId='" + selectedCtg + "']");

            foreach (XmlNode myNode in casesByCategory)
            {

                if (ex.Contains(Convert.ToInt32(myNode.Attributes["caseid"].Value)))
                {
                    IdOkay = false;
                }

                if (IdOkay == true)
                {
                    caseIdOptions.Add(Convert.ToInt32(myNode.Attributes["caseid"].Value)); //הוספת מספר מקרה

                }
            }

            allCatLabel.Text = "";

            categoryDiv.Attributes.Add("class", "margin-top-bottom");


        }
        else //אם המשתמש בחר בחר לתרגל סיפורי מקרה מכל הקטגוריות
        {
            XmlNodeList allCases = myDoc.SelectNodes("//caseSudies/case");
            foreach (XmlNode myNode in allCases)
            {
                if (ex.Contains(Convert.ToInt32(myNode.Attributes["caseid"].Value)))
                {
                    IdOkay = false;
                }
                else
                {
                    IdOkay = true;
                }

                if (IdOkay == true)
                {
                    caseIdOptions.Add(Convert.ToInt32(myNode.Attributes["caseid"].Value)); //הוספת מספר מקרה
                }
            }

            allCatLabel.Text = "מתוך מאגר: כל הבדיקות";


            if (categoryDiv.Attributes["class"] == "margin-top-bottom")
            {
                categoryDiv.Attributes["class"] = categoryDiv.Attributes["class"].Replace("margin-top-bottom", "").Trim();
            }



        }


        if (caseIdOptions.Count < 1)
        {
            catFinishedLabel.Text = "נגמרו סיפורי המקרה בקטגוריה זו.";

            MedicalTestsDDL();

            chooseCatDDL.SelectedValue = "0";

            NavHiddenField.Value = "2";

        }
        else
        {

            Random objRand = new Random();        // Class for getting the random number
            int rndID = objRand.Next(caseIdOptions.Count); //הגרלה ועדכון מספר של המקרה מטופל שנבחר

            Session["selCaseID"] = (caseIdOptions[rndID]).ToString();

            //הוצאת מספר הקטוריה של סיפור המקרה והדפסה אל תוך המשחק
            XmlNode getCatID = myDoc.SelectSingleNode("//caseSudies/case[@caseid='" + Session["selCaseID"] + "']");
            string currentCat = getCatID.Attributes["mdTestId"].Value;

            XmlNode myTestName = myDoc.SelectSingleNode("//medicalTests/mdTest[@mdTestId='" + currentCat + "']"); //הדפסת שם הקטגוריה
            CategoryName.InnerHtml = Server.UrlDecode(myTestName.InnerXml);



            //--------------------------הדפסת תוכן סיפור המקרה אל תוך המשחק------------------------//

            XmlNodeList myNodes = myDoc.SelectNodes("//caseSudies//case[@caseid='" + Session["selCaseID"] + "']/patientDetails");


            foreach (XmlNode myNode in myNodes)  //שליפת תוכן פרטי המטופל
            {

                var title = Server.UrlDecode(myNode["title"].InnerXml);
                var content = Server.UrlDecode(myNode["content"].InnerXml);

                //יצירת לייבל להדפסת כותרת פרטי המטופל
                Label caseLabel = new Label();
                caseLabel.Text = "<h3>" + title + "</h3>" + "<p>" + content + "</p>";


                //יצירת פאנל חדש להכנסת תוכן פרטי המטופל
                Panel casePanel = new Panel();
                casePanel.CssClass = "swiper-slide";

                //הוספת הלייבלים אל תוך הפאנל והטמעתם בדף
                casePanel.Controls.Add(caseLabel);
                FindControl("swipperwrapperid").Controls.Add(casePanel);
                FindControl("swipperwrapperid").DataBind();


            }


        }

    }

    protected void GameBtns_Click(object sender, EventArgs e)
    {
        //משתנה לזיהוי הכפתור משחק שהפעיל את הפונקציה
        Button button = (Button)sender;
        string buttonId = button.ID;

        //הטענת העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));


        //שליפת תשובה לפי המקרה סיפור
        XmlNode myCase = myDoc.SelectSingleNode("/game//case[@caseid='" + Session["selCaseID"] + "']");
        string answer = myCase.Attributes["answer"].Value;


        //הדפסה האם המשתמש טעה או צדק
        if (answer == "true")
        {
            if (buttonId == "dontSendBtn") //אם המשתמש לחץ על כפתור ה"לא חיוני"
            {
                usersuccess = false;
            }
            else//אם המשתמש לחץ על כפתור ה"חיוני"
            {
                usersuccess = true;
            }

        }
        if (answer == "false")
        {
            if (buttonId == "dontSendBtn") //אם המשתמש לחץ על כפתור ה"לא חיוני"
            {
                usersuccess = true;
            }
            else//אם המשתמש לחץ על כפתור ה"חיוני"
            {
                usersuccess = false;
            }
        }

        if (usersuccess == true)//אם המשתמש הצליח
        {
            feedbackTitle.Text = "<h3> תשובה נכונה! </h3>"; //הדפסת משוב 

            //שליפת ניקוד המחלקה 
            XmlNode depScore = myDoc.SelectSingleNode("//departments/department[@depID = '" + (string)Session["selUserDepartment"] + "']");

            //משתנה ששומר את הניקוד לאחר כל שאלה
            int currentPoints;

            //חישוב נקודות למחלקה בהתאם למספר שניות מענה על שאלה
            if (Convert.ToInt16(SecondsHiddenField.Value) < 11)
            {
                currentPoints = 3; //עדכון מספר הניקוד
            }
            else if (11 <= Convert.ToInt16(SecondsHiddenField.Value) && Convert.ToInt16(SecondsHiddenField.Value) < 21)
            {
                currentPoints = 2; //עדכון מספר הניקוד
            }
            else
            {
                currentPoints = 1; //עדכון מספר הניקוד
            }

            //עדכון ניקוד המחלקה 
            depScore.Attributes["depScore"].Value = (Convert.ToInt16(depScore.Attributes["depScore"].Value) + 1).ToString();

            feedbackExp.Text = "";

            TimeLabel.Text = "ענית על השאלה תוך " + SecondsHiddenField.Value + " שניות <br/> הוספת למחלקתך " + currentPoints + " נקודות";

        }
        else//אם המשתמש לא הצליח
        {
            feedbackTitle.Text = "<h3> תשובה לא נכונה </h3>";
            TimeLabel.Text = "";

            //הדפסת הסבר התשובה למשתמש
            XmlNode answerExplanation = myDoc.SelectSingleNode("/game//case[@caseid='" + Session["selCaseID"] + "']/answerExp");
            string answerExp = Server.UrlDecode(answerExplanation.InnerXml);
            feedbackExp.Text = answerExp;

        }


        //יצירת תגית עם מספר הסיפור מקרה עליו ענה המשתמש ומספר השניות שלקח לו לענות
        XmlElement caseIDNode = myDoc.CreateElement("caseID");
        caseIDNode.InnerXml = (Session["selCaseID"]).ToString(); //הכנסת ערך של מספר הסיפור מקרה
        caseIDNode.SetAttribute("seconds", SecondsHiddenField.Value);
        caseIDNode.SetAttribute("answeredCorrect", usersuccess.ToString());

        //הוספת הפריט לעץ
        myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["selUserID"] + "']").AppendChild(caseIDNode);

        //שמירה ךעץ
        myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

    }

    protected void SignUpBtn_Click(object sender, EventArgs e)
    {
        // gameTopLogo.Attributes.Add("style", "display:block");

        bool newUser = true;
        ////חיפוש משתמש לפי המייל שהוזן
        XmlNodeList myNodes = myDoc.SelectNodes("//useremail");

        foreach (XmlNode myNode in myNodes)
        {
            if (emailTB.Text.ToLower() == Server.UrlDecode(myNode.InnerXml))
            {
                newUser = false;
                break;
            }
        }

        if (newUser == false)
        {
            infoLabel.Text = "מייל זה כבר רשום במערכת."; //עדכון הודעה למשתמש
            infoLabel.CssClass = "red";

            emailTB.Text = "";//איפוס המייל שהמשתמש הזין

            SignInLink.CssClass = "bounce-4"; //הוספת אנימציה ללינק למשתמשים רשומים

            NavHiddenField.Value = "0";
        }
        else
        {
            //שליפת מספר מחלקה שנבחרה
            Session["selUserDepartment"] = departmentDD.SelectedValue;
            Session["selUserID"] = myDoc.SelectSingleNode("//users/usercounter").InnerXml;


            //שליפת מספר המשתמשים 
            int userCounter = Convert.ToInt16(myDoc.SelectSingleNode("//users/usercounter").InnerXml);

            //  יצירת משתמש חדש
            XmlElement NewUserNode = myDoc.CreateElement("user");
            //   myItemNode.InnerXml = Server.UrlEncode(imagesLibPath + imageNewName); //הוספת שם ופרטי התמונה
            NewUserNode.SetAttribute("userid", userCounter.ToString()); //הוספת מספר המשתמש
            NewUserNode.SetAttribute("userDepartment", (string)Session["selUserDepartment"]); //הוספת מספר מחלקה

            //יצירת תגית חדשה לשם מלא של המשתמש
            XmlElement UserNameNode = myDoc.CreateElement("username");
            UserNameNode.InnerXml = Server.UrlEncode(fullNameTB.Text); //הוספת שם מלא של המשתמש
            NewUserNode.AppendChild(UserNameNode);

            //יצירת תגית חדשה למייל של המשתמש
            XmlElement UserEmailNode = myDoc.CreateElement("useremail");
            UserEmailNode.InnerXml = Server.UrlEncode(emailTB.Text.ToLower()); //הוספת שם מייל של המשתמש
            NewUserNode.AppendChild(UserEmailNode);


            //הוספת הפריט לעץ
            myDoc.SelectSingleNode("//users").AppendChild(NewUserNode);

            //עדכון מספר המשתמשים
            userCounter++;
            myDoc.SelectSingleNode("//users/usercounter").InnerXml = userCounter.ToString();

            //שמירה של הפריטים החדשים בעץ
            myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

            UserLoggedInHiddenField.Value = "true";

            NavHiddenField.Value = "1";

        }



    }

    protected void SignInBtn_Click(object sender, EventArgs e)
    {

        bool oldUser = false;

        ////חיפוש משתמש לפי המייל שהוזן
        XmlNodeList myNodes = myDoc.SelectNodes("//useremail");

        foreach (XmlNode myNode in myNodes)
        {
            if (emailTB1.Text.ToLower() == Server.UrlDecode(myNode.InnerXml))
            {
                oldUser = true;
            }
        }

        if (oldUser == true) //אם המייל קיים במערכת
        {

            ////חיפוש משתמש לפי המייל שהוזן
            XmlNodeList myNodes1 = myDoc.SelectNodes("//useremail");


            foreach (XmlNode myNode in myNodes1)
            {
                if (Server.UrlDecode(myNode.InnerXml) == emailTB1.Text.ToLower())
                {
                    Session["selUserID"] = myNode.ParentNode.Attributes["userid"].Value;
                    Session["selUserDepartment"] = myNode.ParentNode.Attributes["userDepartment"].Value;

                }
            }



            NavHiddenField.Value = "1";

            UserLoggedInHiddenField.Value = "true"; //משתנה שמעדכן כי המשתמש הצליח לבצע הרשמה/ כניסה למערכת


            //עדכון ומילוי תיבות הטקסט בדף פרופיל

            fullNameTB.Text = Server.UrlDecode(myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["selUserID"] + "']/username").InnerXml);

            emailTB.Text = Server.UrlDecode(myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["selUserID"] + "']/useremail").InnerXml);

            departmentDD.SelectedValue = Session["selUserDepartment"].ToString();


        }
        else //אם המייל לא נמצא במערכת
        {

            infoLabel1.Text = "מייל זה לא נמצא במערכת."; //עדכון הודעה למשתמש
            infoLabel1.CssClass = "red";

            emailTB1.Text = "";//איפוס המייל שהמשתמש הזין

            SignUpLink.CssClass = "bounce-4"; //הוספת אנימציה ללינק למשתמשים רשומים

            NavHiddenField.Value = "01";
        }
    }

    protected void CategorybackBtn_Click(object sender, ImageClickEventArgs e)
    {
        catFinishedLabel.Text = "";

        MedicalTestsDDL();

        NavHiddenField.Value = "2";

    }

    protected void Results_Click(object sender, EventArgs e)
    {


        //---------------------הדפסת ממוצע שניות למשתמש------------------//

        NavHiddenField.Value = "3";

        int totalPoints = 0;

        List<int> secondsList = new List<int>(); //מערך ששומר את מספר השניות למענה של המשתמש

        XmlNodeList casesAswered = myDoc.SelectNodes("//users/user[@userid='" + (string)Session["selUserID"] + "']/caseID");

        foreach (XmlNode myNode in casesAswered)
        {

            if (myNode != null) //אם יש סיפורי מקרה שהמשתמש ענה עליהם
            {
                secondsList.Add(Convert.ToInt32(myNode.Attributes["seconds"].Value));
            }

        }

        int myAvg; //משתנה השומר את הערך של ממוצע המשתמש

        if (secondsList.Count != 0) //אם יש סיפורי מקרה שהמשתמש ענה עליהם
        {
            myAvg = (secondsList.Sum() / secondsList.Count);
        }
        else
        {
            myAvg = 0;
        }

        avgSecLabel2.InnerText = myAvg.ToString() + " שניות";


        //----------הדפסת שמות ותמונות הרופאים המובילים------------//



        string topUser1Id = null;
        string topUser2Id = null;
        string topUser3Id = null;

        int topUser1points = 0;
        int topUser2points = 0;
        int topUser3points = 0;



        ///לולאה שרצה על כל המשתמשים המערכת
        XmlNodeList users = myDoc.SelectNodes("//users/user");

        foreach (XmlNode myNode in users)
        {
            string userId = myNode.Attributes["userid"].Value;

            //משתנה ששומר את הניקוד לאחר כל שאלה
            int currentPoints = 0;

            XmlNodeList casesAsweredCorr = myDoc.SelectNodes("//users/user[@userid='" + userId + "']/caseID[@answeredCorrect='True']");

            //לולאה שרצה וסופרת כמה ניקוד הוסיף כל משתמש לקבוצה שלו
            foreach (XmlNode myNode1 in casesAsweredCorr)
            {
                if (myNode1 != null) // אם קיימים סיפורי מקרה שהמשתמש ענה עליהם בצורה נכונה
                {

                    int userSecs = Convert.ToInt16(myNode1.Attributes["seconds"].Value);

                    //חישוב נקודות למחלקה בהתאם למספר שניות מענה על שאלה
                    if (userSecs < 11)
                    {
                        currentPoints += 3; //עדכון מספר הניקוד
                    }
                    else if (11 <= userSecs && userSecs < 21)
                    {
                        currentPoints += 2; //עדכון מספר הניקוד
                    }
                    else
                    {
                        currentPoints += 1; //עדכון מספר הניקוד
                    }

                }

            }

            if ((string)Session["selUserID"] == userId)
            {
                totalPoints = currentPoints;
            }


            if (currentPoints > topUser1points)
            {
                topUser3Id = topUser2Id;
                topUser2Id = topUser1Id;
                topUser1Id = userId;

                topUser3points = topUser2points;
                topUser2points = topUser1points;
                topUser1points = currentPoints;
            }
            else if (topUser1points == currentPoints && currentPoints != 0)
            {
                //בדיקת ממוצע השניות למענה על שאלות נכונות  - משתמש ראשון
                List<double> user1secList = new List<double>(); //מערך ששומר את מספר השניות למענה של המשתמש

                XmlNodeList User1SecondsSum = myDoc.SelectNodes("//users/user[@userid='" + topUser1Id + "']/caseID[@answeredCorrect='True']");
                foreach (XmlNode myNode1 in User1SecondsSum)
                {
                    user1secList.Add(Convert.ToDouble(myNode1.Attributes["seconds"].Value));
                }
                double user1Avg = (user1secList.Sum() / user1secList.Count);



                //בדיקת ממוצע השניות למענה על שאלות נכונות  - משתמש נוכחי
                List<double> usersecList = new List<double>(); //מערך ששומר את מספר השניות למענה של המשתמש

                XmlNodeList UserSecondsSum = myDoc.SelectNodes("//users/user[@userid='" + userId + "']/caseID[@answeredCorrect='True']");
                foreach (XmlNode myNode1 in UserSecondsSum)
                {
                    usersecList.Add(Convert.ToDouble(myNode1.Attributes["seconds"].Value));
                }
                double userAvg = (usersecList.Sum() / usersecList.Count);

                if (user1Avg > userAvg) //אם ממוצע המשתמש הנוכחי קטן יותר - אז הוא במקום בראשון
                {
                    topUser3Id = topUser2Id;
                    topUser2Id = topUser1Id;
                    topUser1Id = userId;

                    topUser3points = topUser2points;
                    topUser2points = topUser1points;
                    topUser1points = currentPoints;
                }
                else
                {
                    topUser3Id = topUser2Id;
                    topUser2Id = userId;

                    topUser3points = topUser2points;
                    topUser2points = currentPoints;
                }
            }
            else if (topUser1points > currentPoints && currentPoints > topUser2points)
            {
                topUser3Id = topUser2Id;
                topUser2Id = userId;

                topUser3points = topUser2points;
                topUser2points = currentPoints;
            }
            else if (topUser2points == currentPoints && currentPoints != 0) //במקרה ו2 משתמשים מתחרים על המקום השני (הוסיפו ניקוד שווה למחלרות שלהם)
            {
                //בדיקת ממוצע השניות למענה על שאלות נכונות  - משתמש ראשון
                List<double> user2secList = new List<double>(); //מערך ששומר את מספר השניות למענה של המשתמש

                XmlNodeList User2SecondsSum = myDoc.SelectNodes("//users/user[@userid='" + topUser2Id + "']/caseID[@answeredCorrect='True']");
                foreach (XmlNode myNode1 in User2SecondsSum)
                {
                    user2secList.Add(Convert.ToDouble(myNode1.Attributes["seconds"].Value));
                }
                double user2Avg = (user2secList.Sum() / user2secList.Count);



                //בדיקת ממוצע השניות למענה על שאלות נכונות  - משתמש נוכחי
                List<double> usersecList = new List<double>(); //מערך ששומר את מספר השניות למענה של המשתמש

                XmlNodeList UserSecondsSum = myDoc.SelectNodes("//users/user[@userid='" + userId + "']/caseID[@answeredCorrect='True']");
                foreach (XmlNode myNode1 in UserSecondsSum)
                {
                    usersecList.Add(Convert.ToDouble(myNode1.Attributes["seconds"].Value));
                }
                double userAvg = (usersecList.Sum() / usersecList.Count);

                if (user2Avg > userAvg) //אם ממוצע המשתמש הנוכחי קטן יותר - אז הוא במקום בראשון
                {
                    topUser3Id = topUser2Id;
                    topUser2Id = userId;

                    topUser3points = topUser2points;
                    topUser2points = currentPoints;
                }
                else
                {
                    topUser3Id = userId;

                    topUser3points = currentPoints;
                }
            }
            else if (topUser3points == currentPoints && currentPoints != 0)
            {
                //בדיקת ממוצע השניות למענה על שאלות נכונות  - משתמש ראשון
                List<double> user3secList = new List<double>(); //מערך ששומר את מספר השניות למענה של המשתמש

                XmlNodeList User3SecondsSum = myDoc.SelectNodes("//users/user[@userid='" + topUser3Id + "']/caseID[@answeredCorrect='True']");
                foreach (XmlNode myNode1 in User3SecondsSum)
                {
                    user3secList.Add(Convert.ToDouble(myNode1.Attributes["seconds"].Value));
                }
                double user3Avg = (user3secList.Sum() / user3secList.Count);



                //בדיקת ממוצע השניות למענה על שאלות נכונות  - משתמש נוכחי
                List<double> usersecList = new List<double>(); //מערך ששומר את מספר השניות למענה של המשתמש

                XmlNodeList UserSecondsSum = myDoc.SelectNodes("//users/user[@userid='" + userId + "']/caseID[@answeredCorrect='True']");
                foreach (XmlNode myNode1 in UserSecondsSum)
                {
                    usersecList.Add(Convert.ToDouble(myNode1.Attributes["seconds"].Value));
                }
                double userAvg = (usersecList.Sum() / usersecList.Count);

                if (user3Avg > userAvg) //אם ממוצע המשתמש הנוכחי קטן יותר - אז הוא במקום בראשון
                {
                    topUser3Id = userId;

                    topUser3points = currentPoints;
                }
                else
                {
                }
            }
            else if (topUser2points > currentPoints && currentPoints > topUser3points)
            {
                topUser3Id = userId;

                topUser3points = currentPoints;

            }

        }

        topDoc1Label.Text = "ד\"ר " + Server.UrlDecode(myDoc.SelectSingleNode("//users/user[@userid='" + topUser1Id + "']/username").InnerXml);
        if (topUser2Id != null)
        {
            topDoc2Label.Text = "ד\"ר " + Server.UrlDecode(myDoc.SelectSingleNode("//users/user[@userid='" + topUser2Id + "']/username").InnerXml);
        }
        else { topDoc2Label.Text = "--"; }
        if (topUser3Id != null)
        {
            topDoc3Label.Text = "ד\"ר " + Server.UrlDecode(myDoc.SelectSingleNode("//users/user[@userid='" + topUser3Id + "']/username").InnerXml);
        }
        else { topDoc3Label.Text = "--"; }



        //----------------------עדכון תמונות הרופאים המובילים---------------------//

        XmlNode topUser1 = myDoc.SelectSingleNode("//users/user[@userid='" + topUser1Id + "']/userImage");

        if (topUser1 == null)
        {
            topDocImage1.ImageUrl = "~/images/user.svg";
        }
        else
        {
            string UserPic = Server.UrlDecode(topUser1.InnerXml);
            topDocImage1.ImageUrl = UserPic;
        }

        XmlNode topUser2 = myDoc.SelectSingleNode("//users/user[@userid='" + topUser2Id + "']/userImage");

        if (topUser2 == null)
        {
            topDocImage2.ImageUrl = "~/images/user.svg";
        }
        else
        {
            string UserPic = Server.UrlDecode(topUser2.InnerXml);
            topDocImage2.ImageUrl = UserPic;
        }


        XmlNode topUser3 = myDoc.SelectSingleNode("//users/user[@userid='" + topUser3Id + "']/userImage");

        if (topUser3 == null)
        {
            topDocImage3.ImageUrl = "~/images/user.svg";
        }
        else
        {
            string UserPic = Server.UrlDecode(topUser3.InnerXml);
            topDocImage3.ImageUrl = UserPic;
        }





        pointsLabel2.InnerText = totalPoints.ToString() + " נקודות";


    }

    protected void profileBottomDiv_Click(object sender, EventArgs e)
    {

        fullNameTB.Text = "";
        emailTB.Text = "";
        departmentDD.SelectedValue = "0";

        emailTB1.Text = "";

        Session["selUserID"] = null;
    }

    protected void SavePic1_Click(object sender, EventArgs e)
    {
        if ((fileUpload.PostedFile != null) && (fileUpload.PostedFile.ContentLength > 0))
        {
            string filetype = fileUpload.PostedFile.ContentType;//איתור סוג הקובץ 

            //בדיקה האם הקובץ הנקלט הוא מסוג תמונה
            if (filetype.Contains("image"))
            {
                //איתור שם הקובץ
                string fileName = fileUpload.PostedFile.FileName;
                //איתור סיומת הקובץ
                string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                //איתור זמן העלת הקובץ
                string myTime = DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");
                //הגדרת שם חדש לקובץ
                imageNewName = myTime + endOfFileName;


                // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(fileUpload.PostedFile.InputStream);

                //קריאה לפונקציה המקטינה את התמונה
                //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                System.Drawing.Image objImage = FixedSize(bmpPostedImage, 100, 100);


                //שמירת הקובץ בגודלו החדש בתיקייה
                objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);


                XmlNode userImg = myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["selUserID"] + "']/userImage");

                if (userImg != null) //אם למשתמש כבר קיימת תמונה במערכת
                {
                    //משתנה לשם התמונה
                    string oldFileName = Server.UrlDecode(userImg.InnerXml);

                    var filePath = Server.MapPath(oldFileName); //מחיקת התמונה מקובץ ההעלאות

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    //מחיקת התמונה מהעץ
                    userImg.ParentNode.RemoveChild(userImg); //מחיקת הפריט

                }

                XmlElement userImage = myDoc.CreateElement("userImage");
                userImage.InnerXml = Server.UrlEncode(imagesLibPath + imageNewName);

                //הוספת הפריט לעץ
                myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["selUserID"] + "']").AppendChild(userImage);

                //שמירה ךעץ
                myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

                UserLoggedInHiddenField.Value = "true";


                ImageButton profilepic = (ImageButton)FindControl("profileTopDiv").FindControl("profilePicBtn" + Session["selUserID"].ToString());

            }

            else
            {
                //// הקובץ אינו תמונה ולכן לא ניתן להעלות אותו
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('הקובץ אינו תמונה ולכן לא ניתן להעלות אותו.')", true);
            }
        }

        UpdateProfilePic();

    }

    //פונקציה המקבלת את התמונה שהועלתה , האורך והרוחב שאנו רוצים לתמונה ומחזירה את התמונה המוקטנת
    static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = Convert.ToInt32(imgPhoto.Width);
        int sourceHeight = Convert.ToInt32(imgPhoto.Height);

        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height,
                          PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
        grPhoto.Clear(System.Drawing.Color.White);
        grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
            new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            System.Drawing.GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }

    protected void fullNameTB_TextChanged(object sender, EventArgs e)
    {
        if (UserLoggedInHiddenField.Value == "true") //אם המשתמש נכנס למערכת
        {
            TextBox fullName = (TextBox)sender;
            string txtvalue = fullName.Text;

            XmlNode userFullName = myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["selUserID"] + "']/username");
            userFullName.InnerXml = Server.UrlEncode(fullNameTB.Text);

            myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

            SavedLabel.Attributes.Add("style", "display:block");
        }
    }

    protected void emailTB_TextChanged(object sender, EventArgs e)
    {
        if (UserLoggedInHiddenField.Value == "true") //אם המשתמש נכנס למערכת
        {

            bool newUser = true;
            string userEmailstring = Server.UrlDecode(myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["selUserID"] + "']/useremail").InnerXml);

            ////חיפוש משתמש לפי המייל שהוזן
            XmlNodeList myNodes = myDoc.SelectNodes("//users//useremail");

            foreach (XmlNode myNode in myNodes)
            {
                if (emailTB.Text.ToLower() == Server.UrlDecode(myNode.InnerXml) && emailTB.Text != userEmailstring)
                {
                    newUser = false;
                    break;
                }
            }

            if (newUser == false)
            {
                infoLabel.Text = "מייל זה רשום במערכת. התנתק וכנס מחדש עם מייל זה או הזן מייל אחר."; //עדכון הודעה למשתמש
                infoLabel.CssClass = "red";

                NavHiddenField.Value = "0";
            }
            else
            {
                XmlNode userEmail = myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["selUserID"] + "']/useremail");
                userEmail.InnerXml = Server.UrlEncode(emailTB.Text.ToLower());

                myDoc.Save(Server.MapPath("trees/XMLFile.xml"));
            }

            SavedLabel.Attributes.Add("style", "display:block");

        }
    }

    protected void departmentDD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (UserLoggedInHiddenField.Value == "true") //אם המשתמש נכנס למערכת
        {
            XmlNode userDepartment = myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["selUserID"] + "']");
            userDepartment.Attributes["userDepartment"].Value = departmentDD.SelectedValue;

            myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

            SavedLabel.Attributes.Add("style", "display:block");
        }

    }
}

