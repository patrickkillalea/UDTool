using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Sniper_App.Models
{
    public class Logging
    {
        public void LogException(Exception ex)
        {
            string date = DateTime.Today.ToShortDateString();

            string filePath = @"C:\TEMP\Error" + date.Replace("/", "_")  + ".txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }
    }
}