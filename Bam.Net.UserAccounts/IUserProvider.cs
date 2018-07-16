using Bam.Net.ServiceProxy;

namespace Bam.Net.UserAccounts
{
    /// <summary>
    /// When implemented, used to set the user for a given IHttpContext.
    /// </summary>
    public interface IUserProvider
    {
        /// <summary>
        /// Sets the user for the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
        void SetUser(IHttpContext context, string userName, bool isAuthenticated);
    }
}