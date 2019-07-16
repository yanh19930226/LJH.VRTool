using System.Threading.Tasks;
using LJH.VRTool.Configuration.Dto;

namespace LJH.VRTool.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
