using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Participants : System.Web.UI.Page
{
    XmlDocument myDoc = new XmlDocument();

    protected void Page_Load(object sender, EventArgs e)
    {

        //לולאה שעוברת על כל השורות בגריד-ויו
        for (int j = 0; j < ParticipantsGridView.Rows.Count; j++)
        {
            string CurrentDepName = null;
            string currentDepNum = ((Label)ParticipantsGridView.Rows[j].FindControl("departmentLabel")).Text;

            //המרת מספר המחלקה לשם המחלקה
            XmlNodeList depNodes = myDoc.SelectNodes("//departments/department");

            foreach (XmlNode depNode in depNodes)
            {
                if (depNode.Attributes["depID"].Value == currentDepNum)
                {
                    CurrentDepName = depNode.InnerXml.ToString();
                }

            }


            ((Label)ParticipantsGridView.Rows[j].FindControl("departmentLabel")).Text = CurrentDepName;

        }


    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //הטענת העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));
    }

    protected void ParticipantsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //יביא את האלמנט שהפעיל את הפונקציה הזאת
        ImageButton i = (ImageButton)e.CommandSource;
        string theId = i.Attributes["theItemId"];
        Session["userID"] = i.Attributes["theItemId"];


        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":
                deleteConf();
                break;

        }

    }

    void deleteConf()
    {
        //הצגה של המסך האפור
        grayWindows0.Style.Add("display", "block");
        //הצגת הפופ-אפ של המחיקה
        DeleteConfPopUp0.Style.Add("display", "block");
    }



    protected void confYesDelete_Click(object sender, EventArgs e)
    {
        //הסתרה של המסך האפור
        grayWindows0.Style.Add("display", "none");

        //הסרת ענף של משחק קיים באמצעות זיהוי האיי דיי שניתן לו על ידי לחיצה עליו מתוך הטבלה
        //שמירה ועדכון לתוך העץ ולגריד ויו
        XmlNode node = myDoc.SelectSingleNode("//users/user[@userid='" + (string)Session["userID"] + "']");
        node.ParentNode.RemoveChild(node);

        //שמירת שינויים בעץ
        myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        // XmlDataSource2.Save();
        ParticipantsGridView.DataBind();

        Response.Redirect("Participants.aspx");

    }

    protected void confnNoDelete_Click(object sender, EventArgs e)
    {
        //הסתרה של המסך האפור
        grayWindows0.Style.Add("display", "none");

    }

}