using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LJH.VRTool.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LJH.VRTool.Web.Mvc.Controllers
{
    public class UploadManagerController : VRToolControllerBase
    {
        //public ActionResult Upload(int type, HttpPostedFileBase file)
        //{
        //    string md5 = CommonHelper.CalcMD5(file.InputStream);
        //    string ext = Path.GetExtension(file.FileName);
        //    string path = "/upload/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + ext;// /upload/2017/07/07/afadsfa.jpg
        //    string thumbPath = "/upload/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + md5 + "_thumb" + ext;
        //    string fullPath = HttpContext.Server.MapPath("~" + path);//d://22/upload/2017/07/07/afadsfa.jpg
        //    string thumbFullPath = HttpContext.Server.MapPath("~" + thumbPath);
        //    new FileInfo(fullPath).Directory.Create();//尝试创建可能不存在的文件夹

        //    file.InputStream.Position = 0;//指针复位
        //    //file.SaveAs(fullPath);//SaveAs("d:/1.jpg");
        //    //缩略图
        //    ImageProcessingJob jobThumb = new ImageProcessingJob();
        //    jobThumb.Filters.Add(new FixedResizeConstraint(200, 200));//缩略图尺寸200*200
        //    jobThumb.SaveProcessedImageToFileSystem(file.InputStream, thumbFullPath);

        //    file.InputStream.Position = 0;//指针复位

        //    //水印
        //    ImageWatermark imgWatermark =
        //        new ImageWatermark(HttpContext.Server.MapPath("~/Content/Images/watermark.jpg"));
        //    imgWatermark.ContentAlignment = System.Drawing.ContentAlignment.BottomRight;//水印位置
        //    imgWatermark.Alpha = 50;//透明度，需要水印图片是背景透明的png图片
        //    ImageProcessingJob jobNormal = new ImageProcessingJob();
        //    jobNormal.Filters.Add(imgWatermark);//添加水印
        //    jobNormal.Filters.Add(new FixedResizeConstraint(600, 600));
        //    jobNormal.SaveProcessedImageToFileSystem(file.InputStream, fullPath);
        //    houseService.AddNewHousePic(new HousePicDTO { HouseId = houseId, Url = path, ThumbUrl = thumbPath });
        //    return Json(new AjaxResult
        //    {
        //        Status = "ok"
        //    });
        //}
    }
}