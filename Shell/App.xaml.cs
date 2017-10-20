using System;
using System.Windows;
using Prism.Regions;
using WPFPresentationCore;

namespace Presentation.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException((Exception)e.ExceptionObject);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
         
            Bootstrapper bootstrapper=new Bootstrapper();
            bootstrapper.Run();
        }

        private void HandleException(Exception ex)
        {
            var msg = ex.Message;
            MessageBox.Show(msg, "Unhandled Exception");
        }
    }
}
