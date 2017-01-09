using System;
using System.ServiceModel;
using System.Threading;
using System.Web;
using TheIdentityHub;

namespace BasicAuthWcfService
{
    public class BasicAuthWcfServiceService : IBasicAuthWcfServiceService
    {
        public string GetData(int value)
        {
            if (ValidateUser())
            {
                // HttpContext will be null if aspNetCompatibilityEnabled="false".
                return "WCF Thread Principal Identity Name: " + Thread.CurrentPrincipal.Identity.Name + " & WCF User Principal Identity Name: " +
                    (HttpContext.Current != null ? HttpContext.Current.User.Identity.Name : OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name) +
                    " Roles:" + string.Join(" ", Thread.CurrentPrincipal.Roles()) + " DisplayName:" + Thread.CurrentPrincipal.DisplayName();
            }

            return null;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        private static bool ValidateUser()
        {
            if (Thread.CurrentPrincipal == null || Thread.CurrentPrincipal.Identity == null || !Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException();
            }

            return true;
        }
    }
}