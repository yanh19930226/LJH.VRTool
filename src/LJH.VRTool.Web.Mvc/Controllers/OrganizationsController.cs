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
using LJH.VRTool.Web.Models.Organizations;
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
        public IActionResult Index(OrganizationSearch search, int pageIndex)
        {
            int pageSize = 1;
            var res = _userAppService.GetAllListByOrganizationSearch(search.OrganizationId, search.KeyWord, search.TimeMin, search.TimeMax);
            PagedList<UserDto> model = res.OrderBy(a => a.CreationTime).ToPagedList(pageIndex, pageSize);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("List", model);
            }
            ViewBag.Title = "组织机构管理";
            ViewBag.Org = GetOrganization();
            return View(model);
        }

        #region 添加组织机构
        /// <summary>
        /// 添加组织机构
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(OrganizationUnitCreateDto input)
        {
            var res = _organizationAppService.CreateAsync(input);
            return Json(new { status = "ok" });
        }
        #endregion

        #region 编辑组织机构
        [HttpPost]
        public ActionResult Edit(OrganizationUnitUpdateDto dto)
        {
            var res = _organizationAppService.UpdateAsync(dto);
            return Json(new { status = "ok" });
        } 
        #endregion

        #region 删除组织机构
        /// <summary>
        /// 删除组织机构
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Delete(long Id)
        {
            var res = _organizationAppService.DeleteAsync(Id);
            return Json(new { status = "ok" });
        }
        #endregion

        #region 获取组织机构数据
        /// <summary>
        /// 获取组织机构数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrganizationData()
        {
            var list = _organizationAppService.GetList().Where(q=>q.ParentId==null);
            List<DTreeItem> treelist = new List<DTreeItem>();
            foreach (var item in list)
            {
                DTreeItem tree = new DTreeItem();
                tree.Id = item.Id.ToString();
                tree.Title = item.DisplayName;
                if (item.Children.Count > 0)
                {
                    tree.Children = GetChildrens(item);
                }
                treelist.Add(tree);
            }
            return Json(new { status = "ok", data = treelist });
        }

        public List<DTreeItem> GetOrganization()
        {
            var list = _organizationAppService.GetList().Where(q => q.ParentId == null);
            List<DTreeItem> treelist = new List<DTreeItem>();
            foreach (var item in list)
            {
                DTreeItem tree = new DTreeItem();
                tree.Id = item.Id.ToString();
                tree.Title = item.DisplayName;
                if (item.Children.Count > 0)
                {
                    tree.Children = GetChildrens(item);
                }
                treelist.Add(tree);
            }
            return treelist;
        }
        //递归获取子节点
        public List<DTreeItem> GetChildrens(OrganizationUnitDto permission)
        {
            List<DTreeItem> nodetree = new List<DTreeItem>();
            foreach (var item in permission.Children)
            {
                DTreeItem tree = new DTreeItem();
                tree.Id = item.Id.ToString();
                tree.Title = item.DisplayName;
                if (item.Children.Count > 0)
                {
                    tree.Children = GetChildrens(item);
                }
                nodetree.Add(tree);
            }
            return nodetree;
        }
        #endregion

        #region 添加组织成员
        public ActionResult AddMember(string KeyWord,int pageIndex)
        {
            int pageSize = 2;
            var resut= _userAppService.GetOrganizationUser(KeyWord);
            PagedList<OrganizationUserDto> model = resut.OrderBy(a => a.CreationTime).ToPagedList(pageIndex, pageSize);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("AddMemberList", model);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddMember(long[] UserIds)
        {
            return Json(new { status = "ok" });
        }
        #endregion

        #region 移除组织成员
        [HttpPost]
        public ActionResult Delete(long userId,long OrganizationId)
        {
            return Json(null);
        }
        [HttpPost]
        public ActionResult BatchDelete(long userIds, long OrganizationId)
        {
            return Json(null);
        }
        #endregion
    }
}