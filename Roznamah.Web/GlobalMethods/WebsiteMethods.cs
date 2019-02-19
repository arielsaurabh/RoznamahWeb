using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Umbraco.Core;

namespace Roznamah.Web.GlobalMethods
{
    public class WebsiteMethods
    {
        public void WriteWebsiteLogs(string text)
        {
            var date = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Date;
            var filePath = @"~/Logs/WebsiteLogs/WebsiteLogs" + date + ".txt";
            var completePath = HttpContext.Current.Server.MapPath(filePath);
           
            if (File.Exists(completePath))
            {
                TextWriter tw = new StreamWriter(completePath);
                tw.WriteLine(text);
            }
            else
            {
                File.Create(completePath);
                TextWriter tw = new StreamWriter(completePath);
                tw.WriteLine(text);
                tw.Close();
            }
              
        }
        public void WriteBackOfficeLogs(string text)
        {
            var date = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Date;
            var filePath = @"~/Logs/BackOfficeLogs/BackOfficeLogs" + date + ".txt";
            var completePath = HttpContext.Current.Server.MapPath(filePath);

            if (File.Exists(completePath))
            {
                TextWriter tw = new StreamWriter(completePath);
                tw.WriteLine(text);
            }
            else
            {
                File.Create(completePath);
                TextWriter tw = new StreamWriter(completePath);
                tw.WriteLine(text);
                tw.Close();
            }

        }
    }
}