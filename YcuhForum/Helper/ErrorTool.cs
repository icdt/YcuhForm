using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using YcuhForum.Models;

namespace YcuhForum.Helper
{
    public static class ErrorTool
    {
        //TXT版
        public static void RecordByTXT(string errorMessage,string url)
        {
            var directorUrl = HttpContext.Current.Server.MapPath(url);
            if (!Directory.Exists(directorUrl))
            {
                Directory.CreateDirectory(directorUrl);
            }
            var fileUrl = directorUrl + "Error.txt";

            FileStream file = new FileStream(fileUrl, FileMode.Open);
            StreamWriter sw = new StreamWriter(file);
            sw.WriteLine(errorMessage);
            sw.Flush();
            sw.Close();
        }

        //資料庫版
        public static void RecordByDB(ErrorRecord errorRecord)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.ErrorRecords.Add(errorRecord);
            }
        }
    }
}