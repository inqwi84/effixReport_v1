using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using CommonLibraries.Log;

namespace EffixReportSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.Initialize("EffixReportSystem");
            Logger.AddListenerTextWriter();
            Logger.AddListenerConsole();
            base.OnStartup(e);
        }
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.TraceError(e.Exception.ToString());
            e.Handled = true;
        }
    }
}
