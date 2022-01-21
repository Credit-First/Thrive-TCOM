using System;
using System.Windows.Forms;
using System.Threading;

namespace TCom
{
    public class SocketData
    {
        public string message { get; set; }
        public string id { get; set; }
    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static Mutex mutex = null;
        [STAThread]
        static void Main()
        {
            try
            {
                const string appName = "TCom " + Global.Version;
                bool createdNew;
                mutex = new Mutex(true, appName, out createdNew);
                if (!createdNew)
                {
                    //app is already running! Exiting the application
                    MessageBox.Show("Another TCOM instance is already running.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(e.StackTrace, "Error Stack Trace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Session.logException(e);
            }
        }
    }
}