using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using TloggerProject;

namespace TLoggerProject
{
  public static class TLogger
  {
    #region attributes

    static readonly Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
    private static string _logName { get { return AppConfig.AppSettings.Settings[Constants.LogName].Value ?? Constants.FileName; } set { AppConfig.AppSettings.Settings[Constants.LogName].Value = value; } }
    private static string _Path { get { return AppConfig.AppSettings.Settings[Constants.Path].Value ?? ""; } set { AppConfig.AppSettings.Settings[Constants.Path].Value = value; } }
    static public string GetPath { get { return AppConfig.AppSettings.Settings[Constants.Path].Value; } }
    
    
    #endregion

    /// <summary>
    /// With this constructor you can set the output path for the log and the logname which is going to save under.
    /// </summary>
    /// <param name="pathToFile"></param>
    /// <param name="filename">Prefferably the filename with a extension</param>
    public static void InitTLogger(string pathToFile, string filename = Constants.FileName)
    {


      AppConfig.AppSettings.Settings[Constants.LastStartedLog].Value = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
      _logName = checkFileName(filename);
      _Path = pathToFile;
      InitTLogger();
    }


    /// <summary>
    /// Standard initializer
    /// </summary>
    public static void InitTLogger()
    {
      using (StreamWriter sw = File.CreateText(_Path + _logName))
      {
        sw.WriteLine("TLogger: " + DateTime.Now.ToString());
      }
    }

    private static string checkFileName(string filename)
    {
      if (filename.Length < 0)
      {
        return AppConfig.AppSettings.Settings[Constants.FileName].Value +
               AppConfig.AppSettings.Settings[Constants.Extension].Value;
      }
      string finalName = String.Empty;
      Constants.FileSettingsEnum settings = Constants.FileSettingsEnum.MultipleFile;
      Enum.TryParse(AppConfig.AppSettings.Settings[Constants.FileSettings].Value, result: out settings);

      //Name
      finalName += filename.Contains(".") ? filename.Substring(0, filename.IndexOf('.')) : filename;

      if (settings == Constants.FileSettingsEnum.MultipleFile)
      {
        finalName += "_" + AppConfig.AppSettings.Settings[Constants.LastStartedLog].Value;
      }

      //Extension
      if (filename.Contains("."))
      {
        finalName += "." + filename.Split(new string[] { "." }, StringSplitOptions.None)[1];
      }
      else
      {
        finalName += "." + AppConfig.AppSettings.Settings[Constants.Extension].Value;
      }
  
      return finalName;

    }

    public static void SetFileSettings(Constants.FileSettingsEnum enumConstants)
    {
      AppConfig.AppSettings.Settings[Constants.FileSettings].Value = enumConstants.ToString();
    }

    /// <summary>
    /// For easy implementation, will do the same as the log function
    /// </summary>
    /// <param name="logline"></param>
    public static void WriteLine(object logline)
    {
      log(logline);
    }

    /// <summary>
    /// Will add 
    /// </summary>
    /// <param name="logline"></param>
    static public void log(object logline)
    {
      using (StreamWriter sr = File.AppendText(_Path + _logName))
      {
        sr.WriteLine(logline.ToString());
      }
    }
  }
}
