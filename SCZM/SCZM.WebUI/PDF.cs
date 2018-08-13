using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SCZM.WebUI
{
    public class PDF
    {
        public static void GetEnPdf(string templatePath, string newFilePath, Dictionary<string, string> parameters)
       {
           PdfReader pdfReader = new PdfReader(templatePath);
           PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFilePath,FileMode.Create));
           //获取域的集合;
           AcroFields pdfFormFields = pdfStamper.AcroFields;
           //为需要赋值的域设置值;
           foreach (KeyValuePair<string, string> parameter in parameters)
           {
               pdfFormFields.SetField(parameter.Key, parameter.Value);
           }
           //这句很重要，如果为false那么生成的PDF文件还能编辑，一定要设为true;
           pdfStamper.FormFlattening = true;
           pdfStamper.Close();
           pdfReader.Close();
       }
    
       public static void GetChPdf(string templatePath, string newFilePath, string iTextAsianCmapsPath, Dictionary<string, string> parameters)
       {
           PdfReader pdfReader = new PdfReader(templatePath);
           PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFilePath, FileMode.Create));
           //获取域的集合;
           AcroFields pdfFormFields = pdfStamper.AcroFields;
    
           
           //BaseFont.AddToResourceSearch(iTextAsianCmapsPath);
           //创建中文字体，第一个参数是中文字体的路径，第二个参数表示文字方向水平，第三个貌似是字体嵌入PDF文件;
           BaseFont baseFT = BaseFont.CreateFont(@"C:\Windows\Fonts\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
           foreach (KeyValuePair<string, string> parameter in parameters)
           {
               //要输入中文就要设置域的字体;
               pdfFormFields.SetFieldProperty(parameter.Key, "textfont", baseFT, null);
               //为需要赋值的域设置值;
               pdfFormFields.SetField(parameter.Key, parameter.Value);
           }
           //这句很重要，如果为false那么生成的PDF文件还能编辑，一定要设为true;
           pdfStamper.FormFlattening = true;
           pdfStamper.Close();
           pdfReader.Close();
       }
    }
}
