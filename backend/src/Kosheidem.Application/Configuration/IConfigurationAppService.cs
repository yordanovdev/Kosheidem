using System.Threading.Tasks;
using Kosheidem.Configuration.Dto;

namespace Kosheidem.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
