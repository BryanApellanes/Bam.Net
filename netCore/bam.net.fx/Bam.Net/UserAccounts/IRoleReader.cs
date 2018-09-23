namespace Bam.Net.UserAccounts
{
    public interface IRoleReader
    {
        string[] FindUsersInRole(string roleName, string usernameToMatch);
        string[] GetAllRoles();
        string[] GetRolesForUser(string username);
        string[] GetUsersInRole(string roleName);
        bool IsUserInRole(string username, string roleName);
        bool RoleExists(string roleName);
    }
}