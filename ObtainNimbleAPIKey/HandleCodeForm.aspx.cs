using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ObtainNimbleAPIKey
{
    public partial class HandleCodeForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current != null && !String.IsNullOrEmpty(HttpContext.Current.Request["code"]))
            {
                MainForm.Authorization = NimbleAPIClient.RequestAccessToken(HttpContext.Current.Request["code"]);
                Response.Redirect("MainForm.aspx");
            }
        }
    }
}