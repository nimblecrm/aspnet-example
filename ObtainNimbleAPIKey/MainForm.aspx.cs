using System;
using System.Web;


namespace ObtainNimbleAPIKey
{
    public partial class MainForm : System.Web.UI.Page
    {
        private NimbleAPIClient nimbleClient = new NimbleAPIClient();

        public static AccessToken Authorization
        {
            get { return (AccessToken)HttpContext.Current.Session["Authorization"]; }
            set { HttpContext.Current.Session["Authorization"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorization != null)
            {
                this.labelStatus.Text = "Authorization passed succesfully!";
                this.ButtonRun.Visible = false;
                this.labelTokenDescription.Visible = this.labelToken.Visible = true;
                this.labelToken.Text = Authorization.access_token;
            }
        }

        protected void ButtonRun_Click(object sender, EventArgs e)
        {
            Response.Redirect(NimbleAPIClient.AuthorizationGrantCodeUri());
        }
    }
}