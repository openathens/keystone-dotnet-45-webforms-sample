using Microsoft.Owin.Security;
using System;
using System.Configuration;
using System.Web;

namespace KeystoneDotNet45WebFormsSample
{
    /// <summary>
    /// Implements third-party-initiated OIDC login:
    /// https://openid.net/specs/openid-connect-core-1_0.html#ThirdPartyInitiatedLogin
    /// Enter the URL of this page in the 'Login URL' field on the OpenAthens dashboard.
    /// </summary>
    public partial class OaLogin : System.Web.UI.Page
    {
        private const string ISSUER_PARAM = "iss";
        private const string TARGET_PARAM = "target_link_uri";

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateIssuer();
            string target = GetValidatedTarget();
            HttpContext.Current.GetOwinContext().Authentication.Challenge(new AuthenticationProperties
            {
                RedirectUri = target
            });
        }

        private void ValidateIssuer()
        {
            string[] issuerValues = Request.QueryString.GetValues(ISSUER_PARAM);

            // Ensure the request is for the issuer we trust.
            if (issuerValues == null ||
                issuerValues.Length != 1 ||
                issuerValues[0] != ConfigurationManager.AppSettings["oidc:Authority"])
            {
                throw new HttpException(400, "Invalid 'iss' parameter");
            }
        }

        private string GetValidatedTarget()
        {
            string[] targetValues = Request.QueryString.GetValues(TARGET_PARAM);

            // Target is optional.
            if (targetValues == null || targetValues.Length == 0)
            {
                return null;
            }

            // Ensure the target, if supplied, is within our site.
            // - baseUrl may need to be derived differently if a load balancer is terminating TLS.
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            if (targetValues.Length != 1 || !targetValues[0].StartsWith(baseUrl))
            {
                throw new HttpException(400, "Invalid 'target_link_uri' parameter");
            }

            return targetValues[0];
        }
    }
}
