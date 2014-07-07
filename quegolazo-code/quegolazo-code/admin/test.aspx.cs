using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utils;

namespace quegolazo_code.admin
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile.ContentLength > 0)
            {
                GestorImagen.guardarImagenTorneo(FileUpload1.PostedFile, 2);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            GestorImagen.borrrarImagenTorneo(2);
        }
    }
}