using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LJH.VRTool.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LJH.VRTool.Web.Mvc.Controllers
{
    public class UploadManagerController : VRToolControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public UploadManagerController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// 文件上传全景图片、音频、图片
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadFiles(string Type)
        {
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileDir = Path.Combine(_hostingEnvironment.WebRootPath, "KrpanoFiles\\" + Type + "\\" + DateTime.Now.ToString("yyyyMMdd"));
                    string fileExt = Path.GetExtension(file.FileName);
                    string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt;
                    if (!Directory.Exists(fileDir))
                    {
                        Directory.CreateDirectory(fileDir);
                    }
                    //保存的文件路径
                    string filePath = fileDir + $@"\{ newFileName}";
                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(fs);
                        fs.Flush();
                    }
                }
            }
            return Json(new
            {
                msg = ""
            });
        }

    }
}