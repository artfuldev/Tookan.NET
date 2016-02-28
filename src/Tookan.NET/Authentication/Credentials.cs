using Tookan.NET.Sanity;

namespace Tookan.NET.Authentication
{
    public class Credentials
    {
        public readonly static Credentials Anonymous = new Credentials();

        private Credentials()
        {
            AuthenticationType = AuthenticationType.Anonymous;
        }

        public Credentials(string token)
        {
            Ensure.ArgumentIsNotNullOrEmptyString(token, "token");

            Login = null;
            Password = token;
            AuthenticationType = AuthenticationType.Oauth;
        }

        public Credentials(string login, string password)
        {
            Ensure.ArgumentIsNotNullOrEmptyString(login, "login");
            Ensure.ArgumentIsNotNullOrEmptyString(password, "password");

            Login = login;
            Password = password;
            AuthenticationType = AuthenticationType.Basic;
        }

        public string Login
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }

        public AuthenticationType AuthenticationType
        {
            get; 
            private set;
        }
    }
}