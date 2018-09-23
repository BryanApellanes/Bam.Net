namespace Bam.Net.UserAccounts
{
    public interface IRoleProvider: IRoleReader
    {
        void AddUsersToRoles(string[] usernames, string[] roleNames);
        void CreateRole(string roleName);
        bool DeleteRole(string roleName, bool throwOnPopulatedRole);
        void RemoveUsersFromRoles(string[] usernames, string[] roleNames);
    }
}