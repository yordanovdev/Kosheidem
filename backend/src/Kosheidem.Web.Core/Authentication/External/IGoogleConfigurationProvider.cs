using Microsoft.Extensions.Configuration;

namespace Kosheidem.Authentication.External
{
    public interface IGoogleConfigurationProvider
    {
        TConfiguration GetConfiguration<TConfiguration>(string path);
    }
}