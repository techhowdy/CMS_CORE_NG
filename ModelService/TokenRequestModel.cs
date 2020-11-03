using System;
namespace ModelService
{
    public class TokenRequestModel
    {
        // password or refresh_token    
        public string GrantType { get; set; }
        public string UserName { get; set; }
        public string RefreshToken { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
