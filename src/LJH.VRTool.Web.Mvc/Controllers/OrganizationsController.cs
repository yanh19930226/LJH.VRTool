using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Organizations;
using LJH.VRTool.Controllers;
using LJH.VRTool.Organizations;
using LJH.VRTool.Organizations.Dto;
using LJH.VRTool.Users;
using LJH.VRTool.Users.Dto;
using LJH.VRTool.Web.Models.Common.Tree;
using Microsoft.AspNetCore.Mvc;
using Webdiyer.AspNetCore;

namespace LJH.VRTool.Web.Mvc.Controllers
{
    public class OrganizationsController : VRToolControllerBase
    {
        private readonly IOrganizationAppService _organizationAppService;
        private readonly IUserAppService _userAppService;

        public OrganizationsController(IOrganizationAppService organizationAppService,IUserAppService userAppService)
        {
            _organizationAppService = organizationAppService;
            _userAppService = userAppService;
        }
        public async Task<IActionResult> Index()
        {
            int pageIndex = 1;
            int pageSize = 10;
            var users = (await _userAppService.GetAllListAsync());
            PagedList<UserDto> model = users.OrderBy(a => a.CreationTime).ToPagedList(pageIndex, pageSize);
            return View(model);
        }
        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetOrganizationData()
        {
            var listr = _organizationAppService.GetList();
            //var list=_organizationAppService.GetOrganizationList();
            List<EleTreeItem> treelist = new List<EleTreeItem>();
            foreach (var item in listr)
            {
                EleTreeItem tree = new EleTreeItem();
                tree.Id = item.Id.ToString();
                #region 本地化得用法
                //tree.Title = LocalizationHelper.Manager.GetString((LocalizableString)item.DisplayName);
                //tree.Title = L(item.Name);
                tree.Label = item.DisplayName;
                #endregion
                if (item.Children.Count > 0)
                {
                    tree.Children = GetChildrens(item);
                }
                treelist.Add(tree);
            }
            return Json(new { status = "ok", organizations = treelist });
        }
        //递归获取子节点
        public List<EleTreeItem> GetChildrens(OrganizationUnitDto permission)
        {
            List<EleTreeItem> nodetree = new List<EleTreeItem>();
            foreach (var item in permission.Children)
            {
                EleTreeItem tree = new EleTreeItem();
                tree.Id = item.Id.ToString();
                //tree.Title = L(item.Name);
                //tree.Title = LocalizationHelper.Manager.GetString((LocalizableString)item.DisplayName);
                tree.Label = item.DisplayName;
                if (item.Children.Count > 0)
                {
                    tree.Children = GetChildrens(item);
                }
                nodetree.Add(tree);
            }
            return nodetree;
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(OrganizationUnitCreateDto input)
        {
           var res= _organizationAppService.CreateAsync(input);
            return Json(new { status = "ok" });
        }
    }
}