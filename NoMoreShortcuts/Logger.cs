using System;
using System.IO;

/// <summary>
/// Static logger class that allows direct logging of anything to a text file
/// </summary>
static class Logger
{
    public static void Log(object message, string logFileName = "NoMoreShortcuts.log")
    {
        File.AppendAllText(logFileName, DateTime.Now + " : " + message + Environment.NewLine);
    }

    public static void ResetLogFile(string logFileName = "NoMoreShortcuts.log")
    {
        FileStream fs = File.Create(logFileName);
        fs.Close();
    }
}