using MovieApp.Core.Models.Entities;
using MovieApp.Core.Settings;
using System.Security.Cryptography;
using System.Text;

namespace MovieApp.Core.Helpers
{
    /// <summary>
    /// Interface for password hashing
    /// </summary>
    public interface IPasswordHasher
    {
        string ComputeHash(string password, string salt);

        /// <summary>
        /// Generate a new salt for <see cref="User"/>
        /// </summary>
        /// <returns></returns>
        string GenerateSalt();
    }

    /// <summary>
    ///
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        private const int KeySize = 32; // 256 bit

        /// <summary>
        ///
        /// </summary>
        /// <param name="settings"></param>
        public PasswordHasher(PasswordHasherSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// <see cref="PasswordHasher"/> settings.
        /// </summary>
        protected PasswordHasherSettings settings { get; set; }

        /// <inheritdoc/>
        public string ComputeHash(string password, string salt)
        {
            byte[] saltByteValue = Encoding.UTF8.GetBytes(salt);

            using (var algorithm = new Rfc2898DeriveBytes(password, saltByteValue, settings.Iterations, HashAlgorithmName.SHA512))
            {
                return Convert.ToBase64String(algorithm.GetBytes(KeySize));
            }
        }

        /// <inheritdoc/>
        public string GenerateSalt()
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                var byteSalt = new byte[16];
                rng.GetBytes(byteSalt);
                var salt = Convert.ToBase64String(byteSalt);
                return salt;
            }
        }
    }
}