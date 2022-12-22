using System;
using System.Drawing;
using System.Windows.Forms;

namespace Freelancer_Companion_by_Dormammu.Services
{
    public class LogService
    {
        private RichTextBox Logger { get; set; }

        public LogService(RichTextBox logger)
        {
            Logger = logger;
        }

        public void LogEvent(string message)
        {
            Logger.BackColor = Color.LightGray;
            Logger.SelectionColor = Color.Black;
            Logger.AppendText("-> " + message + Environment.NewLine);
            Logger.SelectionStart = Logger.TextLength;
            Logger.ScrollToCaret();
        }

        public void ErrorLogEvent(string message)
        {
            Logger.BackColor = Color.LightGray;
            Logger.SelectionColor = Color.Red;
            Logger.AppendText("-> " + message + Environment.NewLine);
            Logger.SelectionStart = Logger.TextLength;
            Logger.ScrollToCaret();
        }
    }
}
