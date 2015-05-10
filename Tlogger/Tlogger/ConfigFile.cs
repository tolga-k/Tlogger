namespace TloggerProject
{
  public static class ConfigFile
  {
    public static string Path = "",
    OriginalLogName = Constants.FileName,
   LogName = "",
   LastStartedLog = "",
   Extension = "txt",
   FileSettings = Constants.FileSettingsEnum.SingleFile.ToString();

  }
}
