using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Kosheidem.Configuration.Dto;

namespace Kosheidem.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : KosheidemAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
