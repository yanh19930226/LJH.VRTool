using Microsoft.AspNetCore.Antiforgery;
using LJH.VRTool.Controllers;

namespace LJH.VRTool.Web.Host.Controllers
{
    public class AntiForgeryController : VRToolControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
