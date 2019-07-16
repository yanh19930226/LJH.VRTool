using System.Threading.Tasks;
using Abp.Application.Services;
using LJH.VRTool.Sessions.Dto;

namespace LJH.VRTool.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
