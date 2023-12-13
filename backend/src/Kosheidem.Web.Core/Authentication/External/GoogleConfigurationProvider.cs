using System.IO;
using System.Reflection;
using Abp.Dependency;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Kosheidem.Authentication.External
{
    public class GoogleConfigurationProvider : IGoogleConfigurationProvider, ISingletonDependency
    {
        public TConfiguration GetConfiguration<TConfiguration>(string path)
        {
            var assemblyInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var basePath = assemblyInfo.Directory.FullName;
            var configPath = Path.Combine(basePath, path);
            var configJson = File.ReadAllText(configPath);
            var config = JsonConvert.DeserializeObject<TConfiguration>(configJson);
            return config;
        }
    }
}