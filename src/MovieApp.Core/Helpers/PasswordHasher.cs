using MovieApp.Core.Models.Entities;
using MovieApp.Core.Settings;
using System.Security.Cryptography;

namespace MovieApp.Core.Helpers
{
    /// <summary>
    /// Interface for password hashing
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Generate new hashed password.
        /// </summary>
        /// <param name="password">Password to hash.</param>
        /// <returns></returns>
        string ComputeHash(string password);

        /// <summary>
        /// Check if <paramref name="password"/> and <paramref name="storedHash"/> are the same password.
        /// </summary>
        /// <param name="password">Password that is being compared to <paramref name="storedHash"/></param>
        /// <param name="storedHash"><see cref="User"/>'s password.</param>
        /// <returns>Return true if they match, return false if they do not match.</returns>
        bool VerifyPassword(string password, string storedHash);
    }

    /// <summary>
    /// Defines a password hasher
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        protected PasswordHasherSettings settings { get; set; }

        /// <summary>
        /// Initilizes new instance of <see cref="PasswordHasher"/>
        /// </summary>
        /// <param name="settings"><see cref="PasswordHasherSettings"/></param>
        public PasswordHasher(PasswordHasherSettings settings)
        {
            this.settings = settings;
        }

        /// <inheritdoc/>
        public string ComputeHash(string password)
        {
            //Generate salt
            byte[] saltByteValue = GenerateSalt();

            //Compute hash
            using (var algorithm = new Rfc2898DeriveBytes(password, saltByteValue, settings.Iterations, HashAlgorithmName.SHA512))
            {
                return $"{Convert.ToBase64String(saltByteValue)}.{Convert.ToBase64String(algorithm.GetBytes(settings.KeySize))}"; ;
            }
        }

        /// <inheritdoc/>
        public bool VerifyPassword(string password, string storedHash)
        {
            //Split storedHash
            string[] splitStoredHash = storedHash.Split('.');

            //Compute hash and compare it with the old
            using (var algorithm = new Rfc2898DeriveBytes(password, Convert.FromBase64String(splitStoredHash[0]), settings.Iterations, HashAlgorithmName.SHA512))
            {
                string hash = Convert.ToBase64String(algorithm.GetBytes(settings.KeySize));
                return hash == splitStoredHash[1];
            }
        }

        /// <summary>
        /// Generate a new salt for <see cref="User"/>
        /// </summary>
        /// <returns>Salt as a byte array.</returns>
        private byte[] GenerateSalt()
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                var byteSalt = new byte[16];
                rng.GetBytes(byteSalt);
                return byteSalt;
            }
        }
    }
}