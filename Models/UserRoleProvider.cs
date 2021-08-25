using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
namespace Practice4.Models
{
    public class UserRoleProvider: RoleProvider //In RoleProvider right click=goto definition
    {
        //overriding the abstract  methods
        public override string ApplicationName { get; set; }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }
        public override string[] GetRolesForUser(string username)
        {
            //we are fetching the userroles as array and returning the array
            PracticeaspDBEntities1 db = new PracticeaspDBEntities1();
            //using LINQ join
            var x = (from Account in db.Accounts join UserRoleMap in db.UserRoleMaps on Account.Id equals UserRoleMap.UserID join Roles in db.Roles on UserRoleMap.RoleID equals Roles.RId where Account.Username == username select Roles.RoleName).ToArray();
            return x;

        }
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }
     
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
       
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}