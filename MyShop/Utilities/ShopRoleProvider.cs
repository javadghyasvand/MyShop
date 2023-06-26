using System;
using System.Linq;
using System.Web.Security;
using DataLayer;

namespace MyShop.Utilities
{
    public class ShopRoleProvider : RoleProvider
    {
        private readonly MyEShop_DBEntities _cms = new MyEShop_DBEntities();
        public override bool IsUserInRole(string username, string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (MyEShop_DBEntities dbEntities =new MyEShop_DBEntities())
            {
                var result= dbEntities.Users.Where(u => u.UserName == username).Select(u => u.Roles.RoleID).ToArray();

                 string[] resultstr = result.Select(i => i.ToString()).ToArray();

                return resultstr;
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new System.NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}