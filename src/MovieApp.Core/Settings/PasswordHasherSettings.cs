using NetEscapades.Configuration.Validation;
using System.Numerics;

namespace MovieApp.Core.Settings
{
    public class PasswordHasherSettings : IValidatable
    {
        public int Iterations { get; set; } 
        public int KeySize { get; set; } 

        /// <summary>
        /// Validates mandatory settings. If a setting is not defined throws a <see cref="ArgumentException"/>
        /// </summary>
        public void Validate()
        {
            if (Iterations <= 0)
            {
                throw new ArgumentException("Iterations must be larger than zero!");
            }

            if (KeySize <= 0 && BitOperations.IsPow2(KeySize))
            {
                throw new ArgumentException("KeySize must be larger than zero and a power of 2!");
            }
        }
    }
}
