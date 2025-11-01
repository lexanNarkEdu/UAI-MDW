using System;

public partial class MenuPrincipal : System.Web.UI.Page
{
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("EjercicioB.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("EjercicioC.aspx");
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("EjercicioD.aspx");
    }
}
