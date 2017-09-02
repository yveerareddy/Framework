using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using Prism.Mef;
using WPFPresentationCore;
using System.Windows.Threading;
using Prism.Regions;

namespace Presentation.Shell
{
    public class Bootstrapper:MefBootstrapper
    {
        private readonly IThreadingService _threadingService;

        public Bootstrapper()
        {
            _threadingService = new ThreadingService(Dispatcher.CurrentDispatcher);
        }

        protected override DependencyObject CreateShell()
        {
            Application.Current.MainWindow = Container.GetExportedValueOrDefault<Shell>();
            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();
            RegionManager.UpdateRegions();

            return Container.GetExportedValueOrDefault<Shell>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();

        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            MefContainer.Instance.ConfigureContainer(this.Container);
        }

        protected override void ConfigureAggregateCatalog()
        {

            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            var directorylocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var directoryCatalog = Directory.GetFiles(directorylocation, "*.dll");
            foreach (var file in directoryCatalog)
            {
                AggregateCatalog.Catalogs.Add(new AssemblyCatalog(file));
            }
            
          
            //AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(InventoryModule.InventoryModule).Assembly));

        }
    }
}
