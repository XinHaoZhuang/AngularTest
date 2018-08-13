using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Drawing;
using System.Net;
using System.Configuration;
using SCZM.Common;

namespace SCZM.WebUI
{
    public class UpLoad
    {
        public UpLoad()
        {
        }
        /// <summary>
        /// 裁剪图片并保存

        /// </summary>
        public bool cropSaveAs(string fileName, string newFileName, int maxWidth, int maxHeight, int cropWidth, int cropHeight, int X, int Y)
        {
            string fileExt = Utils.GetFileExt(fileName); //文件扩展名，不含“.”

            if (!IsImage(fileExt))
            {
                return false;
            }
            string newFileDir = Utils.GetMapPath(newFileName.Substring(0, newFileName.LastIndexOf(@"/") + 1));
            //检查是否有该路径，没有则创建

            if (!Directory.Exists(newFileDir))
            {
                Directory.CreateDirectory(newFileDir);
            }
            try
            {
                string fileFullPath = Utils.GetMapPath(fileName);
                string toFileFullPath = Utils.GetMapPath(newFileName);
                return Thumbnail.MakeThumbnailImage(fileFullPath, toFileFullPath, 180, 180, cropWidth, cropHeight, X, Y);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 文件上传方法
        /// </summary>
        /// <param name="postedFile">文件流</param>
        /// <param name="isThumbnail">是否生成缩略图</param>
        /// <param name="isWater">是否打水印</param>
        /// <param name="level">文件级次用于生成预览路径</param>
        /// <returns>上传后文件信息</returns>
        public string fileSaveAs(HttpPostedFile postedFile, string upLoadPath, bool isThumbnail, bool isWater,string fileType)
        {
            try
            {
                string jsFileName = postedFile.FileName;
                if (jsFileName.Contains(":\\"))
                {
                    jsFileName = jsFileName.Substring(jsFileName.LastIndexOf("\\") + 1);
                }

                string fileExt = Utils.GetFileExt(jsFileName); //文件扩展名，不含“.”

                int fileSize = postedFile.ContentLength; //获得文件大小，以字节为单位

                string fileName = jsFileName.Substring(0, jsFileName.LastIndexOf(".")); //取得原文件名
                string newFileName = fileName + "_" + Utils.GetRamCode() + "." + fileExt;//随机生成新的文件名

                string newThumbnailFileName = "thumb_" + newFileName; //随机生成缩略图文件名
                if (upLoadPath == "")
                {
                    upLoadPath = GetUpLoadPath();//上传目录相对路径
                }
                else
                {
                    upLoadPath = GetUpLoadPath() + upLoadPath + "/"; //上传目录相对路径
                }
                
                string fullUpLoadPath = Utils.GetMapPath(upLoadPath); //上传目录的物理路径

                string newFilePath = upLoadPath + newFileName; //上传后的路径
                string newThumbnailPath = upLoadPath + newThumbnailFileName; //上传后的缩略图路径

                if (fileType == "xls")
                {
                    if (!CheckFileExtXLS(fileExt))
                    {
                        return "{\"status\": 0, \"msg\": \"请选择Excel类型的文件！\"}";
                    }
                }
                else
                {
                    //检查文件扩展名是否合法
                    if (!CheckFileExt(fileExt))
                    {
                        return "{\"status\": 0, \"msg\": \"不允许上传" + fileExt + "类型的文件！\"}";
                    }
                }
                //检查文件大小是否合法

                if (!CheckFileSize(fileExt, fileSize))
                {
                    return "{\"status\": 0, \"msg\": \"文件超过限制的大小啦！\"}";
                }
                //检查上传的物理路径是否存在，不存在则创建

                if (!Directory.Exists(fullUpLoadPath))
                {
                    Directory.CreateDirectory(fullUpLoadPath);
                }

                //保存文件
                postedFile.SaveAs(fullUpLoadPath + newFileName);

                Model.System.sys_Config siteConfig = new BLL.System.sys_Config().loadConfig();
                //如果是图片，检查图片是否超出最大尺寸，是则裁剪
                if (IsImage(fileExt) && (siteConfig.imgmaxheight > 0 || siteConfig.imgmaxwidth > 0))
                {
                    Thumbnail.MakeThumbnailImage(fullUpLoadPath + newFileName, fullUpLoadPath + newFileName,
                        siteConfig.imgmaxwidth, siteConfig.imgmaxheight);
                }
                //如果是图片，检查是否需要生成缩略图，是则生成

                if (IsImage(fileExt) && isThumbnail && siteConfig.thumbnailwidth > 0 && siteConfig.thumbnailheight > 0)
                {
                    Thumbnail.MakeThumbnailImage(fullUpLoadPath + newFileName, fullUpLoadPath + newThumbnailFileName,
                        siteConfig.thumbnailwidth, siteConfig.thumbnailheight, "Cut");
                }
                //如果是图片，检查是否需要打水印
                if (IsWaterMark(fileExt) && isWater)
                {
                    switch (siteConfig.watermarktype)
                    {
                        case 1:
                            WaterMark.AddImageSignText(newFilePath, newFilePath,
                                siteConfig.watermarktext, siteConfig.watermarkposition,
                                siteConfig.watermarkimgquality, siteConfig.watermarkfont, siteConfig.watermarkfontsize);
                            break;
                        case 2:
                            WaterMark.AddImageSignPic(newFilePath, newFilePath,
                                siteConfig.watermarkpic, siteConfig.watermarkposition,
                                siteConfig.watermarkimgquality, siteConfig.watermarktransparency);
                            break;
                    }
                }
                

                //处理完毕，返回JOSN格式的文件信息

                return "{\"status\": 1, \"msg\": \"上传文件成功！\", \"name\": \""
                    + jsFileName + "\", \"path\": \"" + newFilePath + "\", \"thumb\": \""
                    + newThumbnailPath + "\", \"size\": " + fileSize + ", \"ext\": \"" + fileExt + "\"}";

            }
            catch
            {
                return "{\"status\": 0, \"msg\": \"上传过程中发生意外错误！\"}";
            }
        }
        /// <summary>
        /// xls文件上传方法
        /// </summary>
        /// <param name="postedFile">文件流</param>
        /// <returns>上传后文件信息</returns>
        public string fileSaveAs(HttpPostedFile postedFile,string upLoadPath)
        {
            try
            {
                string fileExt = Utils.GetFileExt(postedFile.FileName); //文件扩展名，不含“.”

                int fileSize = postedFile.ContentLength; //获得文件大小，以字节为单位

                string fileName = postedFile.FileName.Substring(0, postedFile.FileName.LastIndexOf(".")); //取得原文件名
                string newFileName = fileName + "_" + Utils.GetRamCode() + "." + fileExt; //随机生成新的文件名

                if (upLoadPath == "")
                {
                    upLoadPath = GetUpLoadPath();//上传目录相对路径
                }
                else
                {
                    upLoadPath = GetUpLoadPath() + upLoadPath + "/"; //上传目录相对路径
                }
                string fullUpLoadPath = Utils.GetMapPath(upLoadPath); //上传目录的物理路径

                string newFilePath = upLoadPath + newFileName; //上传后的路径


                //检查文件扩展名是否合法
                if (!CheckFileExtXLS(fileExt))
                {
                    return "{\"status\": 0, \"msg\": \"请选择Excel类型的文件！\"}";
                }

                //检查上传的物理路径是否存在，不存在则创建

                if (!Directory.Exists(fullUpLoadPath))
                {
                    Directory.CreateDirectory(fullUpLoadPath);
                }

                //保存文件
                postedFile.SaveAs(fullUpLoadPath + newFileName);

                //处理完毕，返回JOSN格式的文件信息

                return "{\"status\": 1, \"msg\": \"上传文件成功！\", \"name\": \""
                    + postedFile.FileName + "\", \"path\": \"" + newFilePath + "\", \"size\": " + fileSize + ", \"ext\": \"" + fileExt + "\"}";

            }
            catch
            {
                return "{\"status\": 0, \"msg\": \"上传过程中发生意外错误！\"}";
            }
        }
        #region 私有方法

        /// <summary>
        /// 返回上传目录相对路径
        /// </summary>
        /// <param name="fileName">上传文件名</param>
        private string GetUpLoadPath()
        {
            //string path = siteConfig.webpath + siteConfig.filepath + "/"; //站点目录+上传目录
            string path = Utils.GetAppSettingValue("appName") + "UpLoad/"; //站点目录+上传目录
            return path;
        }

        /// <summary>
        /// 是否需要打水印
        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        private bool IsWaterMark(string _fileExt)
        {
            int waterMarkType = 0;
            //判断是否开启水印

            if (waterMarkType > 0)
            {
                //判断是否可以打水印的图片类型
                ArrayList al = new ArrayList();
                al.Add("bmp");
                al.Add("jpeg");
                al.Add("jpg");
                al.Add("png");
                if (al.Contains(_fileExt.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否为图片文件

        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        private bool IsImage(string _fileExt)
        {
            ArrayList al = new ArrayList();
            al.Add("bmp");
            al.Add("jpeg");
            al.Add("jpg");
            al.Add("gif");
            al.Add("png");
            if (al.Contains(_fileExt.ToLower()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查是否为合法的上传文件

        /// </summary>
        private bool CheckFileExt(string _fileExt)
        {
            Model.System.sys_Config siteConfig = new BLL.System.sys_Config().loadConfig();
            //检查危险文件
            string[] excExt = { "asp", "aspx", "php", "jsp", "htm", "html" };
            for (int i = 0; i < excExt.Length; i++)
            {
                if (excExt[i].ToLower() == _fileExt.ToLower())
                {
                    return false;
                }
            }

            //检查合法文件
            string fileExtension = siteConfig.fileextension;
            string[] allowExt = fileExtension.Split(',');
            for (int i = 0; i < allowExt.Length; i++)
            {
                if (allowExt[i].ToLower() == _fileExt.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 检查是否为合法的xls上传文件
        /// </summary>
        private bool CheckFileExtXLS(string _fileExt)
        {
            //检查合法文件

            string[] excExt = { "xls", "xlsx" };
            for (int i = 0; i < excExt.Length; i++)
            {
                if (excExt[i].ToLower() == _fileExt.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 检查文件大小是否合法

        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        /// <param name="_fileSize">文件大小(B)</param>
        private bool CheckFileSize(string _fileExt, int _fileSize)
        {
            Model.System.sys_Config siteConfig = new BLL.System.sys_Config().loadConfig();
            //判断是否为图片文件

            if (IsImage(_fileExt))
            {
                if (siteConfig.imgsize > 0 && _fileSize > siteConfig.imgsize * 1024)
                {
                    return false;
                }
            }
            else
            {
                if (siteConfig.attachsize > 0 && _fileSize > siteConfig.attachsize * 1024)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void fileDel(string filePath)
        {
            string fullUpLoadPath = Utils.GetMapPath(filePath); //物理路径
            //检查物理路径是否存在
            if (File.Exists(fullUpLoadPath))
            {
                System.IO.File.Delete(fullUpLoadPath);
            }
        }
        #endregion
    }
}
