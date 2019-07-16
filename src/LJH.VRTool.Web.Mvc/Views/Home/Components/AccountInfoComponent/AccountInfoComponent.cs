using Abp.Configuration.Startup;
using LJH.VRTool.Sessions;
using LJH.VRTool.Web.Views.Shared.Components.SideBarUserArea;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LJH.VRTool.Web.Views.Home.Components.AccountInfoComponent
{
    public class AccountInfoComponent : VRToolViewComponent
    {
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        public AccountInfoComponent(ISessionAppService sessionAppService,
           IMultiTenancyConfig multiTenancyConfig)
        {
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new SideBarUserAreaViewModel
            {
                LoginInformations = await _sessionAppService.GetCurrentLoginInformations(),
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
            };
            return View(model);
        }
    }
}
