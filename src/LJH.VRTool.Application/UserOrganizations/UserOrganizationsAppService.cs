using Abp.Application.Services;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using LJH.VRTool.Authorization.Users;
using LJH.VRTool.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LJH.VRTool.UserOrganizations
{
    public class UserOrganizationsAppService:  IUserOrganizationsAppService
    {
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IRepository<User,long> _userRepository;
        private readonly IObjectMapper _objectMapper;
        public UserOrganizationsAppService(IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository, IObjectMapper objectMapper, IRepository<User, long> userRepository)
        {
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _objectMapper = objectMapper;
            _userRepository = userRepository;
        }
        
    }
}
