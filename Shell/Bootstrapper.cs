using System.ComponentModel.Composition;
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
        private IRegionManager _regionManager;

        public Bootstrapper()
        {
            _threadingService = new ThreadingService(Dispatcher.CurrentDispatcher);
        }

        protected override DependencyObject CreateShell()
        {
            var regionManager = Container.GetExportedValue<IRegionManager>();
            var shell= new Shell(_regionManager);
            Application.Current.MainWindow = shell;
            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Activate();
            RegionManager.UpdateRegions();
            return shell;
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();

        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            MefContainer.Instance.ConfigureContainer(this.Container);
            Container.ComposeExportedValue<IThreadingService>(_threadingService);
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
