using System.Threading.Tasks;
using Abp.Application.Services;
using LJH.VRTool.Authorization.Accounts.Dto;

namespace LJH.VRTool.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
