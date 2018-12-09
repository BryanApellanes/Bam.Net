using System;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Server;
using Bam.Net.Testing;
using Bam.Net.Services.Clients;
using Bam.Net.Logging;
using Bam.Net.UserAccounts;
using Bam.Net.Automation;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Bam.Net.CoreServices;
using Bam.Net.UserAccounts.Data;
using Bam.Net.Data;
using System.Linq;
using Bam.Net.Messaging;
using Bam.Net.Testing.Integration;

namespace Bam.Net.Application
{
    /// <summary>
    /// User administrative actions
    /// </summary>
    /// <seealso cref="Bam.Net.Testing.CommandLineTestInterface" />
    [Serializable]
    public class LocalUserAdministrationActions : CommandLineTestInterface
    {
        /// <summary>
        /// List all users from the local database.
        /// </summary>
        [ConsoleAction("localListUsers", "LOCAL: list users")]
        public void ListLocalUsers()
        {
            Database userDatabase = ServiceTools.GetUserDatabase();
            UserCollection users = User.LoadAll(userDatabase);
            int num = 1;
            foreach (User user in users)
            {
                OutLineFormat("{0}. ({1}) {2}", num, user.Email, user.UserName);
                OutLineFormat("\tRoles: {0}", string.Join(",", user.Roles.Select(r => r.Name).ToArray()));
            }
        }

        /// <summary>
        /// Create a user in the local user database.
        /// </summary>
        [ConsoleAction("localListUsers", "LOCAL: create a user account")]
        public void CreateLocalUser()
        {
            Database userDatabase = ServiceTools.GetUserDatabase();
            string userName = Prompt("Please enter the name for the new user");
            string emailAddress = Prompt("Please enter the new user's email address");

            User user = User.Create(userName, emailAddress, ServiceTools.ConfirmPasswordPrompt().Sha1());
            OutLineFormat("User created: \r\n{0}", ConsoleColor.Cyan, user.ToJsonSafe().ToJson(true));
        }

        /// <summary>
        /// Lists the local roles.
        /// </summary>
        [ConsoleAction("localListRoles", "LOCAL: list roles")]
        public void ListLocalRoles()
        {
            Database userDatabase = ServiceTools.GetUserDatabase();
            RoleCollection roles = Role.LoadAll(userDatabase);
            int num = 1;
            foreach (Role role in roles)
            {
                OutLineFormat("{0}. {1}", num, role.Name);
            }
        }

        /// <summary>
        /// Adds the user to role.
        /// </summary>
        [ConsoleAction("localAddUserToRole", "LOCAL: add user to role")]
        public void AddUserToRole()
        {
            Database userDatabase = ServiceTools.GetUserDatabase();
            string email = Prompt("Please enter the user's email address");
            User user = User.FirstOneWhere(u => u.Email == email, userDatabase);
            if (user == null)
            {
                OutLine("Unable to find a user with the specified address", ConsoleColor.Yellow);
                return;
            }
            string role = Prompt("Please enter the role to add the user to");
            Role daoRole = Role.FirstOneWhere(r => r.Name == role, userDatabase);
            if (daoRole == null)
            {
                daoRole = new Role(userDatabase)
                {
                    Name = role
                };
                daoRole.Save(userDatabase);
            }
            Role existing = user.Roles.FirstOrDefault(r => r.Name.Equals(daoRole.Name));
            if (existing == null)
            {
                user.Roles.Add(daoRole);
                user.Save(userDatabase);
                OutLineFormat("User ({0}) added to role ({1})", user.UserName, daoRole.Name);
            }
            else
            {
                OutLine("User already in specified role");
            }
        }

        /// <summary>
        /// Deletes a local user.
        /// </summary>
        [ConsoleAction("localDeleteUser", "LOCAL: delete user account")]
        public void DeleteLocalUser()
        {
            Database userDatabase = ServiceTools.GetUserDatabase();
            if (!Confirm("Whoa, whoa, hold your horses cowboy!! Are you sure you know what you're doing?", ConsoleColor.DarkYellow))
            {
                return;
            }
            OutLineFormat("This might not work depending on the state of the user's activity and related data.  Full scrub of user's is not implemented to help ensure data integrity into the future.", ConsoleColor.DarkYellow);
            if (!Confirm("Continue?", ConsoleColor.DarkYellow))
            {
                return;
            }
            string email = Prompt("Please enter the user's email address");
            User toDelete = User.FirstOneWhere(u => u.Email == email, userDatabase);
            if (toDelete == null)
            {
                OutLineFormat("Unable to find the user with the email address {0}", ConsoleColor.Magenta, email);
                return;
            }

            try
            {
                if (!Confirm($"Last chance to turn back!! About to delete this user:\r\n{toDelete.ToJsonSafe().ToJson(true)}", ConsoleColor.Yellow))
                {
                    return;
                }
                toDelete.Delete(userDatabase);
                OutLineFormat("User deleted", ConsoleColor.DarkMagenta);
            }
            catch (Exception ex)
            {
                OutLineFormat("Delete user failed: {0}", ConsoleColor.Magenta, ex.Message);
            }
        }
    }
}
