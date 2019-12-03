using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KeystoneDotNet45WebFormsSample
{
    public partial class Claims : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                HttpContext.Current.GetOwinContext().Authentication.Challenge();
            }

            var claims = ClaimsPrincipal.Current.Claims;
            dlClaims.DataSource = claims;
            dlClaims.DataBind();
        }
    }
}