using System;
namespace ModelService
{
    public class TwoFactorRequestModel
    {
        public string ProviderType { get; set; }
        public string TwoFactorToken { get; set; }       
        public bool RememberDevice { get; set; }
    }
}
