using System;
using System.Security.Principal;
using System.Threading;

namespace NetCore.Security
{
    public class AuthManager
    {
        public static void SetCurrentPrincipal(IPrincipal? _principal = null)
        {
            AppDomain.CurrentDomain.SetThreadPrincipal(_principal ?? new UserPrincipal());
        }
        public static UserPrincipal User
        {
            get
            {
                var principal = Thread.CurrentPrincipal as UserPrincipal;
                if (principal == null)
                    throw new ArgumentException("The application's default thread principal must be set to a UserPrincipal object on startup.");

                return principal;
            }
        }
    }
}
