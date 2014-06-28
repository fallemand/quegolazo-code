using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quegolazo_code.admin
{
    public partial class registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
            bool registroExitoso = true;
            if (registroExitoso)
                panExito.Visible = true;
            else
                panFracaso.Visible = true;
        }
    }
}