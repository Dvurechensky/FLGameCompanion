using System;
using System.Windows.Forms;

namespace Freelancer_Companion_by_Dormammu.Services
{
    public class LogService
    {
        private TextBox Logger { get; set; }

        public LogService(TextBox logger)
        {
            Logger = logger;
        }

        public void LogEvent(string message)
        {
            Logger.AppendText("-> " + message + Environment.NewLine);
        }
    }
}
