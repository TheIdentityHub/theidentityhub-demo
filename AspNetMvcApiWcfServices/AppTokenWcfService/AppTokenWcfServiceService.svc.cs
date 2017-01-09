using System;
using System.ServiceModel;
using System.Threading;
using System.Web;
using TheIdentityHub;

namespace AppTokenWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AppTokenWcfServiceService : IAppTokenWcfServiceService
    {
        public string GetData(int value)
        {
            if (ValidateUser())
            {
                // HttpContext will be null if aspNetCompatibilityEnabled="false".
                return "WCF Thread Principal Identity Name: " + Thread.CurrentPrincipal.Identity.Name + " & WCF User Principal Identity Name: " +
                    (HttpContext.Current != null ? HttpContext.Current.User.Identity.Name : OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name) +
                    " Roles:" + string.Join(" ", Thread.CurrentPrincipal.Scopes()) + " Is Application:" + Thread.CurrentPrincipal.IsApp().ToString();
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