using Prism.Ioc;
using Prism.Unity;
using System.Diagnostics;
using System.Windows;
using WpfApp.Base.Interfaces;
using WpfApp.Core.Services;
using WpfApp.Views;

namespace WpfApp
{
    public partial class App : PrismApplication
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Trace.Listeners.Add(new TextWriterTraceListener("App.log"));
            Trace.AutoFlush = true;
            
            base.OnStartup(e);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ITimeZoneService, TimeZoneService>();
        }

        private void PrismApplication_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Trace.TraceError($"An unhandled exception occurred: {e.Exception}");
            e.Handled = true;
        }
    }
}
