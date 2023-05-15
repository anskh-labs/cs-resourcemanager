using System.Security.Principal;

namespace NetCore.Security
{
    public class UserIdentity : IIdentity
    {
        public UserIdentity(int id, string name, string[] roles, string[] permissions)
        {
            ID= id;
            Name = name;
            Roles = roles;
            Permissions = permissions;
        }
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string[] Permissions { get; private set; }
        public string[] Roles { get; private set; }

        #region IIdentity Members
        public string AuthenticationType { get { return "User Authentication"; } }

        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }
        #endregion
    }
}
