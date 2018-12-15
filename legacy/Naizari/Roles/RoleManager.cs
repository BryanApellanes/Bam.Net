/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using Naizari.Data.Access;
using Naizari.Helpers;

namespace Naizari.Roles
{
    public class RoleManager: DaoContextConsumer
    {
        static RoleManager current;
        Dictionary<string, List<Role>> applicationRoles;

        public RoleManager()
        {
            ContextName = Application.ContextName;
        }

        #region moved to abstract DaoContextConsumer class
        //string serverName;
        //public string ServerName
        //{
        //    get { return serverName; }
        //    set
        //    {
        //        serverName = value;
        //        if (!string.IsNullOrEmpty(databaseName))
        //            DaoContext.Init(Application.ContextName, GetConnectionString());                        
        //    }
        //}

        //string databaseName;
        //public string DatabaseName
        //{
        //    get { return databaseName; }
        //    set
        //    {
        //        databaseName = value;
        //        if (!string.IsNullOrEmpty(serverName))
        //            DaoContext.Init(Application.ContextName, GetConnectionString());
        //    }
        //}

        //private string GetConnectionString()
        //{
        //    SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
        //    conn.DataSource = serverName;
        //    conn.InitialCatalog = databaseName;
        //    conn.IntegratedSecurity = true;
        //    return conn.ConnectionString;
        //}
        #endregion


        public RoleManagementResult LastResult { get; set; }
        public bool AllowDelete { get; set; }
        public string ApplicationName { get; set; }

        public static RoleManager Current
        {
            get
            {
                if (current == null)
                {
                    if (HttpContext.Current != null
                        && HttpContext.Current.Session != null
                        && HttpContext.Current.Session["RoleManager"] != null)
                    {
                        current = (RoleManager)HttpContext.Current.Session["RoleManager"];
                    }
                    else
                    {
                        current = new RoleManager();
                    }
                }

                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Session != null && HttpContext.Current.Session["RoleManager"] == null)
                        HttpContext.Current.Session["RoleManager"] = current;
                }

                return current;
            }
        }

        public static void Init()
        {
            if (string.IsNullOrEmpty(current.ApplicationName))
                throw new InvalidOperationException("ApplicationName not set");

            current.LoadRoles();
        }

        bool loaded;
        public void LoadRoles()
        {
            LoadRoles(false);
        }

        public void LoadRoles(bool reload)
        {
            try
            {
                if (!loaded || reload)
                {
                    if (applicationRoles == null)
                        applicationRoles = new Dictionary<string, List<Role>>();

                    applicationRoles.Clear();
                    Application[] applicationInfo = Application.SelectListWhere(new SqlSelectParameter(ApplicationFields.ApplicationName, ApplicationName));
                    LastResult = new RoleManagementResult();

                    if (applicationInfo.Length == 0)
                    {
                        LastResult.Message = string.Format("Roles for application {0} are not registered", ApplicationName);
                        LastResult.ModifyResult = RoleModifyResult.Error;
                        return;
                    }
                    if (applicationInfo.Length > 1)
                    {
                        LastResult.Message = string.Format("Multiple applications with the name of {0} were found.  This is an indication that the RoleManager configuration is corrupt", ApplicationName);
                        LastResult.ModifyResult = RoleModifyResult.Error;
                    }

                    if (applicationInfo.Length == 1)
                    {
                        Application app = applicationInfo[0];
                        applicationRoles.Add(app.ApplicationName, new List<Role>());
                        applicationRoles[app.ApplicationName].AddRange(app.RoleList);

                        LastResult.ModifyResult = RoleModifyResult.Successs;
                    }
                    loaded = true;
                }
            }
            catch (Exception ex)
            {
                LastResult = new RoleManagementResult();
                LastResult.Message = ex.Message;
                LastResult.ModifyResult = RoleModifyResult.Error;
                LastResult.Exception = ex;
            }
        }

        public string GetCurrentUser()
        {
            return UserUtil.GetCurrentUser();
        }

        public string GetCurrentUserDomain()
        {
            return UserUtil.GetCurrentUserDomain();
        }

        public bool CurrentUserIsInRole(string roleName)
        {
            return CurrentUserIsInRole(roleName, false);
        }

        public bool CurrentUserIsInRole(string roleName, bool reload)
        {
            this.LoadRoles(reload);
            SetUserRoles(reload);
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.User.IsInRole(roleName);
            }
            else
            {
                return currentRoles.Contains(roleName);
            }
        }

        bool userRolesSet;
        List<string> currentRoles;
        public void SetUserRoles()
        {
            SetUserRoles(false);
        }

        public void SetUserRoles(bool reload)
        {
            LastResult = new RoleManagementResult();
            if (userRolesSet && !reload)
            {
                LastResult.ModifyResult = RoleModifyResult.NoActionNecessary;
                LastResult.Message = string.Empty;
                return;
            }

            try
            {
                if (currentRoles == null)
                    currentRoles = new List<string>();
                string userName = UserUtil.GetCurrentUser();
                string domainName = UserUtil.GetCurrentUserDomain(); 
                List<string> roles = new List<string>();
                foreach (Role role in applicationRoles[ApplicationName])
                {
                    foreach (User user in role.UserList)
                    {
                        if (user.UserId.ToLower().Equals(userName.ToLower()) &&
                            user.Domain.ToLower().Equals(domainName.ToLower()))
                        {
                            roles.Add(role.RoleName);
                        }
                    }
                }
                currentRoles.Clear();
                currentRoles.AddRange(roles);
                if (HttpContext.Current != null)
                {
                    IPrincipal userPrincipal = HttpContext.Current.User;
                    GenericPrincipal newPrincipal = new GenericPrincipal(userPrincipal.Identity, currentRoles.ToArray());
                    HttpContext.Current.User = newPrincipal;
                    LastResult.ModifyResult = RoleModifyResult.Successs;
                    LastResult.Message = string.Empty;
                }
                userRolesSet = true;
            }
            catch (Exception ex)
            {
                LastResult.Message = ex.Message;
                LastResult.ModifyResult = RoleModifyResult.Error;
                LastResult.Exception = ex;
            }
        }

        public string RoleServer
        {
            get
            {
                return this.ServerName;
            }

            set
            {
                this.ServerName = value;
            }
        }

        public string RoleDatabase
        {
            get { return this.DatabaseName; }
            set { this.DatabaseName = value; }
        }

        public void AddApplication(string applicationName)
        {
            AddApplication(applicationName, string.Empty);
        }

        public void AddApplication(string applicationName, string applicationDescription)
        {
            LastResult = new RoleManagementResult();

            try
            {
                // check if an application with the same name is already registered
                Application[] existing = Application.SelectListWhere(new SqlSelectParameter(ApplicationFields.ApplicationName, applicationName));
                if (existing.Length > 0)
                {
                    if (existing.Length == 1)
                    {
                        if (!existing[0].Description.Equals(applicationDescription))
                        {
                            existing[0].Description = applicationDescription;
                            if (existing[0].Update() != UpdateResult.Success)
                            {
                                throw existing[0].LastException;
                            }
                            else
                            {
                                LastResult.Message = "Updated description";
                                LastResult.ModifyResult = RoleModifyResult.Successs;
                            }
                        }
                    }
                    else
                    {
                        LastResult.ModifyResult = RoleModifyResult.Error;
                        LastResult.Message = "Multiple applications with the same name";
                    }
                    return;
                }

                Application newApp = Application.New();
                newApp.ApplicationName = applicationName;
                newApp.Description = applicationDescription;
                if (newApp.Insert() == -1)
                    throw newApp.LastException;

                LastResult.Message = "Added application " + applicationName;
                LastResult.ModifyResult = RoleModifyResult.Successs;
            }
            catch (Exception ex)
            {
                LastResult.Message = ex.Message;
                LastResult.ModifyResult = RoleModifyResult.Error;
                LastResult.Exception = ex;
            }
        }

        /// <summary>
        /// Deletes all applicatioins from the RoleManager with the specified name.
        /// </summary>
        /// <param name="applicationName">The name of the application to delete</param>
        public void DeleteApplication(string applicationName)
        {
            LastResult = new RoleManagementResult();
            try
            {
                Application[] appsToDelete = Application.SelectListWhere(new SqlSelectParameter(ApplicationFields.ApplicationName, applicationName));
                foreach (Application appToDelete in appsToDelete)
                {
                    appToDelete.AllowDelete = true;
                    foreach (Role role in appToDelete.RoleList)
                    {
                        role.AllowDelete = true;
                        foreach (User user in role.UserList)
                        {
                            user.AllowDelete = true;
                            user.Delete();
                        }

                        role.Delete();
                    }

                    appToDelete.Delete();
                }
            }
            catch (Exception ex)
            {
                LastResult.Exception = ex;
                LastResult.Message = ex.Message;
                LastResult.ModifyResult = RoleModifyResult.Error;
            }
        }

        public bool AddRole(string applicationName, string roleName)
        {
            return AddRole(applicationName, roleName, string.Empty);
        }

        public bool AddRole(string applicationName, string roleName, string description)
        {
            LastResult = new RoleManagementResult();
            try
            {
                Application app = GetApplication(applicationName);
                Role existing = GetRole(app.ApplicationId, roleName);
                if (existing == null)
                {
                    if (app != null)
                    {
                        Role role = Role.New();
                        role.ApplicationId = app.ApplicationId;
                        role.Description = description;
                        role.RoleName = roleName;
                        if (role.Insert() == -1)
                        {
                            throw role.LastException;
                        }
                        else
                        {
                            LastResult.Message = string.Format("Role {0} successfully added for application {1}", roleName, applicationName);
                            LastResult.ModifyResult = RoleModifyResult.Successs;
                            return true;
                        }
                    }
                    else
                    {
                        LastResult.Message = string.Format("Application {0} was not found", applicationName);
                        LastResult.ModifyResult = RoleModifyResult.Error;
                        return false;
                    }
                }
                else
                {
                    LastResult.Message = string.Format("Role {0} already exists for application {1}", roleName, applicationName);
                    LastResult.ModifyResult = RoleModifyResult.Error;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LastResult.ModifyResult = RoleModifyResult.Error;
                LastResult.Exception = ex;
                LastResult.Message = ex.Message;
                return false;
            }
        }

        public Application[] GetApplications()
        {
            return Application.SelectAll();
        }

        private Application GetApplication(string applicationName)
        {
            Application app = null;

            Application[] apps = Application.SelectListWhere(new SqlSelectParameter(ApplicationFields.ApplicationName, applicationName));

            if (apps.Length == 1)
            {
                app = apps[0];

            }

            if (apps.Length > 1)
            {
                LastResult.Message = "Multiple applications found";
                LastResult.ModifyResult = RoleModifyResult.Error;
            }

            if (apps.Length == 0)
            {
                LastResult.Message = "Application not registered";
                LastResult.ModifyResult = RoleModifyResult.Error;
            }
            return app;
        }

        public bool RemoveUserFromRole(string applicationName, string roleName, string userName, string domain)
        {
            try
            {
                Application app = GetApplication(applicationName);
                Role role = GetRole(app.ApplicationId, roleName);
                if (LastResult.ModifyResult == RoleModifyResult.Successs)
                {
                    User[] users = User.SelectListWhere(new SqlSelectParameter(UserFields.UserId, userName),
                        new SqlSelectParameter(UserFields.Domain, domain),
                        new SqlSelectParameter(UserFields.ApplicationId, app.ApplicationId),
                        new SqlSelectParameter(UserFields.RoleId, role.RoleId));

                    if (users.Length == 1)
                    {
                        User user = users[0];
                        user.AllowDelete = true;
                        user.Delete();
                        LastResult.Message = string.Format("Removed user {0} from role {1} for application {2}", userName, roleName, applicationName);
                        LastResult.ModifyResult = RoleModifyResult.Successs;
                        return true;
                    }
                    else if (users.Length > 1)
                    {
                        LastResult.Message = string.Format("Multiple records found for {0}\\{1} in role {2} for application {3}",
                            domain,
                            userName,
                            roleName,
                            applicationName);
                        LastResult.ModifyResult = RoleModifyResult.Error;
                        return false;
                    }
                    else
                    {
                        LastResult.Message = string.Format("User {0} not found in role {1}", userName, roleName);
                        LastResult.ModifyResult = RoleModifyResult.Error;
                        return false;
                    }
                }
                else
                {
                    LastResult.Message = string.Format("Unable to finde role {0}", roleName);
                    LastResult.ModifyResult = RoleModifyResult.Error;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LastResult.Message = ex.Message;
                LastResult.ModifyResult = RoleModifyResult.Error;
                return false;
            }
        }

        private Role GetRole(int applicationId, string roleName)
        {
            LastResult = new RoleManagementResult();
            Application app = Application.SelectById(applicationId);
            Role[] roles = Role.SelectListWhere(new SqlSelectParameter(RoleFields.RoleName, roleName),
                new SqlSelectParameter(RoleFields.ApplicationId, applicationId));

            if (roles.Length == 1)
            {
                LastResult.Message = string.Empty;
                LastResult.ModifyResult = RoleModifyResult.Successs;
                return roles[0];
            }else if( roles.Length > 1 )
            {
                LastResult.Message = string.Format("Multiple roles by the name of {0} found for application {1}", roleName, app.ApplicationName);
                LastResult.ModifyResult = RoleModifyResult.Error;
                return null;
            }
            else 
            {
                LastResult.Message = string.Format("Role {0} was not found for application {1}", roleName, app.ApplicationName);
                LastResult.ModifyResult = RoleModifyResult.Error;
                return null;
            }
        }

        public bool AddUserToRole(string applicationName, string roleName, string userName, string domain)
        {
            Application app = GetApplication(applicationName);
            LastResult = new RoleManagementResult();
            LastResult.ModifyResult = RoleModifyResult.Error;
            LastResult.Message = string.Format("Role {0} for application {1} was not found", roleName, applicationName);

            try
            {
                if (app != null)
                {
                    foreach (Role role in app.RoleList)
                    {
                        if (role.RoleName.ToLower().Equals(roleName.ToLower()))
                        {
                            bool doAdd = true;
                            foreach (User user in role.UserList)
                            {
                                if (user.UserId.ToLower().Equals(userName.ToLower()) &&
                                    user.Domain.ToLower().Equals(domain.ToLower()))
                                    doAdd = false;
                            }

                            if (doAdd)
                            {
                                User userToAdd = User.New();
                                userToAdd.ApplicationId = app.ApplicationId;
                                userToAdd.RoleId = role.RoleId;
                                userToAdd.UserId = userName;
                                userToAdd.Domain = domain;
                                if (userToAdd.Insert() == -1)
                                    throw userToAdd.LastException;
                                else
                                {
                                    LastResult.ModifyResult = RoleModifyResult.Successs;
                                    LastResult.Message = string.Empty;
                                    return true;
                                }
                            }
                            else
                            {
                                LastResult.ModifyResult = RoleModifyResult.Error;
                                LastResult.Message = string.Format("User {0}\\{1} is already in role {2} for application {3}", userName, domain, roleName, applicationName);
                                return false;
                            }
                        }
                    }

                    LastResult.ModifyResult = RoleModifyResult.Error;
                    LastResult.Message = string.Format("Role {0} was not found", roleName);
                    return false;
                }
                else
                {
                    LastResult.Message = "Application not found";
                    LastResult.ModifyResult = RoleModifyResult.Error;
                    return false;
                }

            }
            catch (Exception ex)
            {
                LastResult.Message = ex.Message;
                LastResult.ModifyResult = RoleModifyResult.Error;
                LastResult.Exception = ex;
                return false;
            }
            //return false;
        }

        public bool DeleteRole(string applicationName, string roleName)
        {
            LastResult = new RoleManagementResult();
            try
            {
                Application app = GetApplication(applicationName);
                Role role = GetRole(app.ApplicationId, roleName);
                if (role != null)
                {
                    role.AllowDelete = true;
                    role.Delete();
                    LastResult.Message = string.Empty;
                    LastResult.ModifyResult = RoleModifyResult.Successs;
                    return true;
                }
                else
                {
                    // GetRole will modify the LastResult indicating why the role wasn't returned.
                    return false;
                }
            }
            catch (Exception ex)
            {
                LastResult.Message = ex.Message;
                LastResult.Exception = ex;
                LastResult.ModifyResult = RoleModifyResult.Error;
                return false;
            }
        }
    }
}
