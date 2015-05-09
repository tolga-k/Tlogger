using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TloggerProject
{
  public class Constants
  {
    public enum FileSettingsEnum
    {
      SingleFile,
      MultipleFile
    }

    public const string Path = "Path",
      TempPath = Path,
      LogName = "LogName",
      FileName = "TLog",
      Extension = "Extension",
      LastStartedLog = "LastStartedLog",
      FileSettings = "FileSettings";
  }
}
