using System;

namespace BackupsExtra.Logger
{
    public class ConsoleLogger : LocalLogger
    {
        public void SendMessage(string message)
        {
            Console.WriteLine(DateTime.Now.ToString() + message);
        }
    }
}