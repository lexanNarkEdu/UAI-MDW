using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class control : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cc_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Response.Redirect("controlResultado.aspx");
            }
        }
    }
}