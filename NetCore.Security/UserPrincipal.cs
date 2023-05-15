using System.Linq;
using System.Security.Principal;

namespace NetCore.Security
{
    public class UserPrincipal : IPrincipal
    {
        private UserIdentity _identity;
        public UserIdentity Identity
        {
            get => _identity ?? (_identity = new AnonymousIdentity());
            set => _identity = value;
        }
        IIdentity? IPrincipal.Identity => Identity;

        public bool IsInRole(string role)
        {
            return _identity.Roles.Contains(role);
        }
        public bool IsInPermission(string permission)
        {
            return _identity.Permissions.Contains(permission);
        }
    }
}
