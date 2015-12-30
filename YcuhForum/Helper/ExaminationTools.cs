using NPOI.HSSF.UserModel;
using YcuhForum.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace YcuhForum.Helper
{
    public  static class ExaminationTools
    {
        //public static String CreateExamination(String targetUrl,HttpPostedFileBase file)
        //{
        //    List<Examination> finallString = new List<Examination>();
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        if (!Directory.Exists(targetUrl))
        //        {
        //            Directory.CreateDirectory(targetUrl);
        //        }
        //        string targetFilePath = Path.Combine(targetUrl, file.FileName);
        //        file.SaveAs(targetFilePath);

        //        var excelObj = new ExcelQueryFactory(targetFilePath);
        //        var excelData = excelObj.Worksheet(0).ToList();

        //        for (int i = 0; i < excelData.Count(); i++)
        //        {
        //            List<String> optionsValue = excelData[i][3].ToString().Split('/').ToList();
        //            Examination tempObj = new Examination();
        //            tempObj.Question = excelData[i][1].ToString();

        //            for (int k = 0; k < optionsValue.Count();k++ )
        //            {
        //                if(k == 0)
        //                {
        //                    tempObj.Options.Add(new OptionsAndAnswer() { Option = optionsValue[k], Answer = true });
        //                }
        //                else
        //                {
        //                    tempObj.Options.Add(new OptionsAndAnswer() { Option = optionsValue[k], Answer = false });
        //                }
        //            }
        //            finallString.Add(tempObj);
        //        }
        //    }

        //   return Newtonsoft.Json.JsonConvert.SerializeObject(finallString);
        //}





        public static String CreateExamination(HttpPostedFileBase file)
        {
            List<ExamQuestion> finallString = new List<ExamQuestion>();
            HSSFWorkbook wk = new HSSFWorkbook(file.InputStream);
            var sheet = wk.GetSheetAt(0);
            for (int row = 1; row <= sheet.LastRowNum; row++)
            {
                if (sheet.GetRow(row) != null)
                {
                    List<String> optionsValue = sheet.GetRow(row).Cells[1].ToString().Split('/').ToList();

                    ExamQuestion newExamQuestion = new ExamQuestion();
                    newExamQuestion.Id = row;
                    newExamQuestion.Question = sheet.GetRow(row).Cells[0].ToString();
                     for (int k = 0; k < optionsValue.Count();k++)
                     {
                         if (k == 0)
                         {
                             newExamQuestion.Options.Add(new OptionsAndAnswer() { Option = optionsValue[k], Answer = true });
                         }
                         else
                         {
                             newExamQuestion.Options.Add(new OptionsAndAnswer() { Option = optionsValue[k], Answer = false });
                         }
                     }
                     finallString.Add(newExamQuestion);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(finallString);
        }

    }
}