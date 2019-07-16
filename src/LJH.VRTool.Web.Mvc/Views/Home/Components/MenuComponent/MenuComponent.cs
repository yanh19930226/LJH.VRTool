using Abp.Application.Navigation;
using Abp.Runtime.Session;
using LJH.VRTool.Web.Views.Shared.Components.SideBarNav;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LJH.VRTool.Web.Views.Home.Components.MenuComponent
{
    public class MenuComponent : VRToolViewComponent
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IAbpSession _abpSession;
        public MenuComponent(
                IUserNavigationManager userNavigationManager,
                IAbpSession abpSession)
        {
            _userNavigationManager = userNavigationManager;
            _abpSession = abpSession;
        }
        public async Task<IViewComponentResult> InvokeAsync(string activeMenu = "")
        {
            var model = new SideBarNavViewModel
            {
                MainMenu = await _userNavigationManager.GetMenuAsync("MainMenu", _abpSession.ToUserIdentifier()),
                ActiveMenuItemName = activeMenu
            };
            return View(model);
        }
    }
}
