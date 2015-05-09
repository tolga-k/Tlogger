using System;
using System.Configuration;
using System.IO;

namespace TLoggerProject
{
  public static class TLogger
  {
    #region attributes

    private static string _logName { get { return ConfigurationManager.AppSettings[LogName] ?? FileName; } set { ConfigurationManager.AppSettings[LogName] = value; } }
    private static string _Path { get { return ConfigurationManager.AppSettings[Path] ?? ""; } set { ConfigurationManager.AppSettings[Path] = value; } }
    static public string GetPath { get { return ConfigurationManager.AppSettings[Path]; } }
    
    private const string Path = "Path", TempPath = Path, LogName = "LogName", FileName = "TLog.txt";
    #endregion

    /// <summary>
    /// With this constructor you can set the output path for the log and the logname which is going to save under.
    /// </summary>
    /// <param name="pathToFile"></param>
    /// <param name="filename">Prefferably the filename with a extension</param>
    public static void InitTLogger(string pathToFile, string filename = FileName)
    {
      if (filename.Length<0)
      {
        _logName = FileName;
      }
      _logName = filename;
      _Path = pathToFile;
      InitTLogger();
    }

    public static void InitTLogger()
    {
      using (StreamWriter sw = File.CreateText(_Path + _logName))
      {
        sw.WriteLine("TLogger: " + DateTime.Now.ToString());
      }
    }


    public static void WriteLine(object logline)
    {
      log(logline);
    }

    static public void log(object logline)
    {
      using (StreamWriter sr = File.AppendText(_Path + _logName))
      {
        sr.WriteLine(logline.ToString());
      }
    }
  }
}
