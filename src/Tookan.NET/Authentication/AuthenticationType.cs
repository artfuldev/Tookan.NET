namespace Tookan.NET.Authentication
{
    /// <summary>
    /// Authentication protocols supported by the API
    /// </summary>
    public enum AuthenticationType
    {
        /// <summary>
        /// No credentials provided
        /// </summary>
        Anonymous,
        /// <summary>
        /// Username &amp; password
        /// </summary>
        Basic,
        /// <summary>
        /// Delegated access to a third party
        /// </summary>
        Oauth
    }
}
