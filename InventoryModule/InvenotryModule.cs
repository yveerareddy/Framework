
using System.ComponentModel.Composition;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using WPFPresentationCore;

namespace InventoryModule
{

    [ModuleExport(typeof(InventoryModule))]
    public class InventoryModule:IModule
    {
        private IRegionManager _regionManager;
        public void Initialize()
        {
            
        }

        [ImportingConstructor]
        public InventoryModule(IRegionManager regionManager,IInventoryView view)
        {
            _regionManager = regionManager;
            _regionManager.AddToRegion(Constants.DealRegion, view);

        }
    }
}
