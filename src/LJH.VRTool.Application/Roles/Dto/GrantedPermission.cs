using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace LJH.VRTool.Roles.Dto
{
    public class GrantedPermission
    {
        public string Name { get; set; }
        public ILocalizableString DisplayName { get; set; }
    }
}
