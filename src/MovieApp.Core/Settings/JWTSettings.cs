using NetEscapades.Configuration.Validation;

namespace MovieApp.Core.Settings
{
    /// <summary>
    /// Defines validation class for JWT settings.
    /// </summary>
    public class JWTSettings : IValidatable
    {
        public string Secret { get; set; }

        /// <summary>
        /// Validates mandatory settings. If a setting is not defined throws a <see cref="ArgumentException"/>
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrEmpty(Secret))
            {
                throw new ArgumentException("Secretis required!");
            }
        }
    }
}