using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Kosheidem.Authentication.External
{
    public class GoogleApiProvider : ExternalAuthProviderApiBase, IExternalAuthProviderApi
    {
        public override Task<ExternalAuthUserInfo> GetUserInfo(string accessCode)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessCode);

            var externalAuthUserInfo = new ExternalAuthUserInfo
            {
                Provider = "Google",
                ProviderKey = token.Audiences.FirstOrDefault(),
                EmailAddress = token.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
                Name = token.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value,
                Surname = token.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value,
                FullName = token.Claims.FirstOrDefault(c => c.Type == "name")?.Value,
                Picture = token.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
            };

            return Task.FromResult(externalAuthUserInfo);
        }
    }
}