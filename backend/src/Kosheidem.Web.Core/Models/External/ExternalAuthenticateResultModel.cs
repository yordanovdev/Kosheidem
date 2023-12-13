namespace Kosheidem.Models.External
{
    public class ExternalAuthenticateResultModel
    {
        public string AccessToken { get; set; }
        public string EncryptedAccessToken { get; set; }
        public int ExpireInSeconds { get; set; }
    }
}