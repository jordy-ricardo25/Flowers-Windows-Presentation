using System.Runtime.InteropServices;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Linq;
using System;

namespace Flowers
{
    /// <summary>
    /// 
    /// </summary>
    static class Program
    {
        [DllImport("user32.dll")]
        extern static bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        extern static bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">Event data.</param>
        [STAThread]
        [DebuggerNonUserCodeAttribute]
        [GeneratedCodeAttribute("PresentationBuildTasks", "6.0.10.0")]
        public static void Main(string[] args)
        {
            var currentProcess = Process.GetCurrentProcess();
            var runningProcess = (from process in Process.GetProcesses()
                                  where
                                    process.Id != currentProcess.Id &&
                                    process.ProcessName.Equals(
                                      currentProcess.ProcessName,
                                      StringComparison.Ordinal)
                                  select process).FirstOrDefault();

            if (runningProcess is not null)
            {
                ShowWindow(runningProcess.MainWindowHandle, SW_RESTORE);
                SetForegroundWindow(runningProcess.MainWindowHandle);

                return;
            }

            new App().Run();
        }

        const int SW_RESTORE = 9;
    }
}


