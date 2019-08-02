using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LJH.VRTool.Roles.Dto;
using LJH.VRTool.Users.Dto;

namespace LJH.VRTool.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        #region MyRegion
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input); 
        #endregion

        Task<List<UserDto>> GetAllListAsync();
        Task<UserDto> CreateUser(CreateUserDto input);
        List<UserDto> GetAllList(string Keyword, DateTime? TimeMin, DateTime? TimeMax);

        Task<long> ChangeActive(long id);

        List<UserDto> GetAllListByOrganizationSearch(long? OrganizationId, string Keyword, DateTime? TimeMin, DateTime? TimeMax);
    }
}
