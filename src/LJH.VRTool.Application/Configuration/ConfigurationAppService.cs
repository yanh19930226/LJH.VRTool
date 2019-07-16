using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using LJH.VRTool.Configuration.Dto;

namespace LJH.VRTool.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : VRToolAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
