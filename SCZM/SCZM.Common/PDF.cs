using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

using System.Reflection;

namespace SCZM.Common
{
    /// <summary>
    /// 未整理，仅实现销售协议生成pdf
    /// </summary>
    public class PDF
    {
        public static bool GetPdf(string templatePath, string newFilePath, Dictionary<string, string> parameters)
        {
            bool result = true;
            try
            {
                PdfReader pdfReader = new PdfReader(templatePath);
                PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFilePath, FileMode.Create));


                //加密
                pdfStamper.SetEncryption(PdfWriter.STRENGTH128BITS, "", null, PdfWriter.AllowPrinting);

                //通过iTextAsian调用中文字体
                //iTextSharp.text.io.StreamUtil.AddToResourceSearch(Assembly.LoadFile(Utils.GetMapPath(Utils.GetAppSettingValue("appName")+"lib/iTextAsian.dll")));
                //iTextSharp.text.io.StreamUtil.AddToResourceSearch(Assembly.LoadFile(Utils.GetMapPath(Utils.GetAppSettingValue("appName") + "lib/iTextAsianCmaps.dll")));
                //BaseFont baseFT = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.EMBEDDED);
                // BaseFont baseFT1 = BaseFont.CreateFont("MHei-Medium", "UniCNS-UCS2-H", BaseFont.EMBEDDED);


                //调用系统字体
                BaseFont baseFT = BaseFont.CreateFont(@"C:\Windows\Fonts\MSYH.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                //BaseFont baseFT1 = BaseFont.CreateFont(@"C:\Windows\Fonts\msyhbd.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                BaseFont baseFT1 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);


                //获取域的集合;
                AcroFields pdfFormFields = pdfStamper.AcroFields;

                //设置域的字体;生成文件几十K

                pdfFormFields.AddSubstitutionFont(baseFT);

                //为需要赋值的域设置值;
                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    if (parameter.Key == "titile")
                    {
                        pdfFormFields.SetFieldProperty(parameter.Key, "textfont", baseFT1, null);
                    }

                    //pdfFormFields.SetFieldProperty(parameter.Key, "textfont", baseFT, null );//生成文件过大(十几MB左右) 摒弃掉了
                    pdfFormFields.SetField(parameter.Key, parameter.Value);

                }
                //这句很重要，如果为false那么生成的PDF文件还能编辑，一定要设为true;
                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
                pdfReader.Close();
            }
            catch
            {
                result = false;
                throw;
            }
            
            return result;
        }
        
    }
}
