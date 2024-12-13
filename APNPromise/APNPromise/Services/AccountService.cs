using APNPromise.Models;

namespace APNPromise.Services
{
    public class AccountService
    {
        private readonly BearerTokenService _bearerTokenService;
        public AccountService(BearerTokenService bearerTokenService)
        {
            _bearerTokenService = bearerTokenService;
        }

        public Token LoginUser(LoginRequest loginData)
        {
            if (loginData.Username == "username" && loginData.Password == "password")
            {
                var result = new Token();
                result.AccessToken = _bearerTokenService.GenerateBearerToken();

                return result;
            }
            else
                return null;
        }
    }
}
