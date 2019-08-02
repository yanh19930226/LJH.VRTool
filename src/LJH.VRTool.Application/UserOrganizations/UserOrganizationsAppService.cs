using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using LJH.VRTool.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LJH.VRTool.UserOrganizations
{
    public class UserOrganizationsAppService:IUserOrganizationsAppService
    {
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IObjectMapper _objectMapper;
        public UserOrganizationsAppService(IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository, IObjectMapper objectMapper)
        {
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _objectMapper = objectMapper;
        }
        /// <summary>
        /// 根据组织机构和条件获取数据
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <param name="Keyword"></param>
        /// <param name="TimeMin"></param>
        /// <param name="TimeMax"></param>
        /// <returns></returns>
        public List<UserDto> GetAllListByOrganizationSearch(long OrganizationId,string Keyword, DateTime? TimeMin, DateTime? TimeMax)
        {
            List<UserDto> list = new List<UserDto>();
            //var res = _userOrganizationUnitRepository.GetAllIncluding
            return list;
        }
    }
}
