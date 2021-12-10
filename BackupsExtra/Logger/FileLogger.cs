using System;
using System.IO;

namespace BackupsExtra.Logger
{
    public class FileLogger : LocalLogger
    {
        public void SendMessage(string message)
        {
            string path = @"../../../log.txt";
            StreamWriter streamWriter = new StreamWriter(path, false, System.Text.Encoding.Default);
            streamWriter.WriteLine(DateTime.Now.ToString() + message);
        }
    }
}