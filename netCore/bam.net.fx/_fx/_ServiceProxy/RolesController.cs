/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bam.Net;
using System.Web.Security;
using System.Reflection;

namespace Bam.Net.ServiceProxy
{
    public class RolesController : BaseController
    {
        private ActionResult Try<T>(Func<T> func, string methodName = "Unspecified")
        {
            try
            {
                return Json(GetSuccessWrapper(func(), methodName));
            }
            catch (Exception ex)
            {
                return Json(GetErrorWrapper(ex, true, methodName));
            }
        }

        //
        // GET: /Roles/
        [RoleRequired("~/", "Admin", "Developer")]
        public ActionResult AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            return Try<bool>(() =>
            {
                Roles.AddUsersToRoles(usernames, roleNames);
                return true;
            }, MethodBase.GetCurrentMethod().Name);
        }

        [RoleRequired("~/", "Admin", "Developer")]
        public ActionResult CreateRole(string roleName)
        {
            return Try<bool>(() =>
            {
                Roles.CreateRole(roleName);
                return true;
            }, MethodBase.GetCurrentMethod().Name);
        }

        [RoleRequired("~/", "Admin", "Developer")]
        public ActionResult DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return Try<bool>(() =>
            {
                return Roles.DeleteRole(roleName, throwOnPopulatedRole);
            }, MethodBase.GetCurrentMethod().Name);
        }

        [RoleRequired("~/", "Admin", "Developer")]
        public ActionResult FindUsersInRole(string roleName, string usernameToMatch)
        {
            return Try<string[]>(() =>
            {
                return Roles.FindUsersInRole(roleName, usernameToMatch);
            }, MethodBase.GetCurrentMethod().Name);
        }

        [RoleRequired("~/", "Admin", "Developer")]
        public ActionResult GetAllRoles()
        {
            return Try<string[]>(() =>
            {
                return Roles.GetAllRoles();
            }, MethodBase.GetCurrentMethod().Name);
        }

        [RoleRequired("~/", "Admin", "Developer")]
        public ActionResult GetRolesForUser(string username)
        {
            return Try<string[]>(() =>
            {
                return Roles.GetRolesForUser(username);
            }, MethodBase.GetCurrentMethod().Name);
        }

        [RoleRequired("~/", "Admin", "Developer")]
        public ActionResult GetUsersInRole(string roleName)
        {
            return Try<string[]>(() =>
            {
                return Roles.GetUsersInRole(roleName);
            }, MethodBase.GetCurrentMethod().Name);
        }

        [RoleRequired("~/", "Admin", "Developer")]
        public ActionResult IsUserInRole(string username, string roleName)
        {
            return Try<bool>(() =>
            {
                return Roles.IsUserInRole(username, roleName);
            }, MethodBase.GetCurrentMethod().Name);
        }

        [RoleRequired("~/", "Admin", "Developer")]
        public ActionResult RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            return Try<bool>(() =>
            {
                Roles.RemoveUsersFromRoles(usernames, roleNames);
                return true;
            }, MethodBase.GetCurrentMethod().Name);
        }

        [RoleRequired("~/", "Admin", "Developer")]
        public ActionResult RoleExists(string roleName)
        {
            return Try<bool>(() =>
            {
                return Roles.RoleExists(roleName);
            }, MethodBase.GetCurrentMethod().Name);
        }

    }
}
