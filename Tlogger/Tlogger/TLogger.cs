using System;
using System.Configuration;
using System.IO;

namespace TLoggerProject
{
  public static class TLogger
  {
    #region attributes

    private static string path = "TLOG.txt";
    private const string Path = "Path", TempPath = Path;
    #endregion

    public static void InitTLogger(string pathToFile)
    {
      ConfigurationManager.AppSettings[Path] = pathToFile;
      InitTLogger();
    }

   static public string getPath
    {
      get { return ConfigurationManager.AppSettings[Path]; }
    }


    public static void InitTLogger()
    {
      using (StreamWriter sw = File.CreateText(ConfigurationManager.AppSettings[Path] + path))
      {
        sw.WriteLine("TLogger: Date = " + DateTime.Now.ToString());
      }
    }

    public static void WriteLine(object logline)
    {
      log(logline);
    }

    static public void log(object logline)
    {
      using (StreamWriter sr = File.AppendText(ConfigurationManager.AppSettings[Path] + path))
      {
        sr.WriteLine(logline.ToString());
      }
    }
  }
}
