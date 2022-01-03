using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ((Button)FindControl("loginBtn")).Attributes.Add("disabled", "true");

    }



    protected void loginBtn_Click(object sender, EventArgs e)
    {
        if(userNameTB.Text.ToLower() == "smartquest" && passwordTB.Text.ToLower() == "smartquest2020")
        {
            Response.Redirect("Cases.aspx");

        }
        else
        {
            loginLabel.Text = "שם משתמש / סיסמה שגויים.";
            loginLabel.ForeColor = System.Drawing.Color.Red;
            userNameTB.Text = "";

        }
    }
}