using Microsoft.AspNetCore.Hosting;
using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;

namespace VRTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 七牛云
            //// Qiniu.Common.Config.SetZone(Qiniu.Common.ZoneID.CN_East, true);
            //Mac mac = new Mac("4YtIfUb7poesjO43tpTp6u13imtNZPsE0JtiaxrY",
            //    "WSTXcDSVBnAKdwWQMJ7FvxpPJiwNRfRDc4mmgwD8");
            //string bucket = "yanh";
            //string saveKey = "a.png";
            //string localFile = "E:\\1.png";

            //Qiniu.Common.Config.AutoZone("4YtIfUb7poesjO43tpTp6u13imtNZPsE0JtiaxrY",bucket, true);
            //// 上传策略，参见 
            //// https://developer.qiniu.com/kodo/manual/put-policy
            //PutPolicy putPolicy = new PutPolicy();
            //// 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            //// putPolicy.Scope = bucket + ":" + saveKey;
            //putPolicy.Scope = bucket;
            //// 上传策略有效期(对应于生成的凭证的有效期)          
            //putPolicy.SetExpires(3600);
            //// 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            //putPolicy.DeleteAfterDays = 1;
            //// 生成上传凭证，参见
            //// https://developer.qiniu.com/kodo/manual/upload-token            
            //string jstr = putPolicy.ToJsonString();
            //string token = Auth.CreateUploadToken(mac, jstr);
            //UploadManager um = new UploadManager();
            //HttpResult result = um.UploadFile(localFile, saveKey, token);
            //Console.WriteLine(result);

            //Console.ReadKey();

            #endregion

            //using (Image<Rgba32> image = Image.Load("E:\\1.png"))
            //{
            //    image.Mutate(x => x
            //         .Resize(200, 100)
            //         .DrawImage(Image.Load("E:\\weixin.png"), new SixLabors.Primitives.Point(10, 10), PixelColorBlendingMode.Normal, PixelAlphaCompositionMode.Dest, (float)0.4));
            //    image.Save("E:\\246.png");
            //}
            using (Image<Rgba32> image = Image.Load("E:\\bg1.jpg"))
            {
                var font = SixLabors.Fonts.SystemFonts.CreateFont("Arial", 20);
                image.Mutate(x => x
                     .Resize(200, 100)
                    .DrawText("sdfdssd", font, Rgba32.AntiqueWhite, new SixLabors.Primitives.PointF(130, 80)));
                image.Save("E:\\246.png");
            }
        }
    }
}
