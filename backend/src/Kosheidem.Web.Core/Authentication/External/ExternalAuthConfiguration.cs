using System.Collections.Generic;
using Abp.Dependency;
using Kosheidem.Models.External;

namespace Kosheidem.Authentication.External
{
    public class ExternalAuthConfiguration : IExternalAuthConfiguration, ISingletonDependency
    {
        public List<ExternalLoginProviderInfo> Providers { get; }

        public ExternalAuthConfiguration(IGoogleConfigurationProvider googleConfigurationProvider)
        {
            var clientSecrets = googleConfigurationProvider
                .GetConfiguration<GoogleAuthConfiguration>("Authentication/External/webConfiguration.json").Web;

            Providers = new List<ExternalLoginProviderInfo>
            {
                new ExternalLoginProviderInfo("Google", clientSecrets.ClientId, clientSecrets.ClientSecret,
                    typeof(GoogleApiProvider))
            };
        }
    }
}