using MovieApp.Core.Helpers;

namespace MovieApp.Core.Constants
{
    /// <summary>
    /// All of the constans used in the solution
    /// </summary>
    public class MovieAppConstants
    {
        /// <summary>
        /// Connection string section.
        /// </summary>
        public const string MovieAppDatabaseSection = "MovieAppDatabase";

        /// <summary>
        /// <see cref="PasswordHasher"/> section.
        /// </summary>
        public const string PasswordHasherSection = "PasswordHasher";

        /// <summary>
        /// JWT section.
        /// </summary>
        public const string JWTSection = "JWT";

        /// <summary>
        /// Used for BearerToken
        /// </summary>
        public const string BearerToken = "Bearer";

        /// <summary>
        /// Used for added new element to context for JWT auth
        /// </summary>
        public const string ContextUser = "User";
    }
}