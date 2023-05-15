using NetCore.Cryptography;
using ResourceManager.Models;
using System.Collections.Generic;

namespace ResourceManager.Configuration
{
    internal class DesignData
    {
        public static DesignData Instance = new DesignData();
        private DesignData() { }
        public IList<User> Users => new User[]
            {
                new User(){
                    ID = 1,
                    Name = "Administrator",
                    AccountName = "admin",
                    Password = EncryptionManager.Instance.EncryptString("admin", EncryptionManager.KEY)
                },
                new User(){
                    ID = 2,
                    Name = "User",
                    AccountName = "user",
                    Password = EncryptionManager.Instance.EncryptString("user", EncryptionManager.KEY)
                }
            };

        public IList<UserRole> UserRoles => new UserRole[]
            {
                new UserRole()
                {
                    UserID=1,
                    RoleID=1
                }
            };

        public IList<Role> Roles => new Role[]
            {
                new Role()
                {
                    ID = 1,
                    Name = "Admin",
                    Description = "Role as Administrator"
                },
                new Role()
                {
                    ID = 2,
                    Name = "User",
                    Description = "Role as User"
                }
            };

        public IList<Permission> Permissions => new Permission[]
            {
                new Permission()
                {
                    ID = 1,
                    Name = Constants.VIEW_USER_PERMISSION,
                    Description = "Permission for view user menu"
                },
                new Permission()
                {
                    ID = 2,
                    Name = Constants.VIEW_PASSWORD_PERMISSION,
                    Description = "Permission for view Password menu"
                },
                new Permission()
                {
                    ID = 3,
                    Name = Constants.VIEW_REPOSITORY_PERMISSION,
                    Description = "Permission for view repository menu"
                },
                new Permission()
                {
                    ID = 4,
                    Name = Constants.VIEW_EBOOK_PERMISSION,
                    Description = "Permission for view ebook menu"
                },
                new Permission()
                {
                    ID = 5,
                    Name = Constants.VIEW_ARTICLE_PERMISSION,
                    Description = "Permission for view article menu"
                },
                new Permission()
                {
                    ID = 6,
                    Name = Constants.VIEW_ACTIVITY_PERMISSION,
                    Description = "Permission for view activity menu"
                },
                new Permission()
                {
                    ID = 7,
                    Name = Constants.VIEW_NOTE_PERMISSION,
                    Description = "Permission for view note menu"
                },
                new Permission()
                {
                    ID = 8,
                    Name = Constants.VIEW_TOOLS_PERMISSION,
                    Description = "Permission for view tools menu"
                },
                new Permission()
                {
                    ID = 9,
                    Name = Constants.ACTION_ADD_USER,
                    Description = "Permission for add user"
                },
                new Permission()
                {
                    ID = 10,
                    Name = Constants.ACTION_EDIT_USER,
                    Description = "Permission for edit user"
                },
                new Permission()
                {
                    ID = 11,
                    Name = Constants.ACTION_DEL_USER,
                    Description = "Permission for delete user"
                },
                new Permission()
                {
                    ID = 12,
                    Name = Constants.ACTION_ADD_ROLE,
                    Description = "Permission for add role"
                },
                new Permission()
                {
                    ID = 13,
                    Name = Constants.ACTION_EDIT_ROLE,
                    Description = "Permission for edit role"
                },
                new Permission()
                {
                    ID = 14,
                    Name = Constants.ACTION_DEL_ROLE,
                    Description = "Permission for delete role"
                },
                new Permission()
                {
                    ID = 15,
                    Name = Constants.ACTION_ADD_PERMISSION,
                    Description = "Permission for add permission"
                },
                new Permission()
                {
                    ID = 16,
                    Name = Constants.ACTION_EDIT_PERMISSION,
                    Description = "Permission for edit permission"
                },
                new Permission()
                {
                    ID = 17,
                    Name = Constants.ACTION_DEL_PERMISSION,
                    Description = "Permission for delete permission"
                }
            };

        public IList<RolePermission> RolePermissions => new RolePermission[]
            {
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=1
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=2
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=3
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=4
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=5
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=6
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=7
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=8
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=9
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=10
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=11
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=12
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=13
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=14
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=15
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=16
                },
                new RolePermission()
                {
                    RoleID=1,
                    PermissionID=17
                }
            };
    }
}
