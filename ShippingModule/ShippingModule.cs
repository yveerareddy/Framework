using System.ComponentModel.Composition;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using WPFPresentationCore;

namespace ShippingModule
{
    [ModuleExport(typeof(ShippingModule))]
    public class ShippingModule:IModule
    {
        private INavigationService _navigationService;
        public void Initialize()
        {
            _navigationService.ActivateViewInRegion(Constants.ShipmentsRegion, MefContainer.Instance.Resolve<IShippingView>(), Constants.ShipmentView);
            _navigationService.ActivateViewInRegion(Constants.ParcelRegion, MefContainer.Instance.Resolve<IParcelView>(), Constants.ParcelView);

        }

        [ImportingConstructor]
        public ShippingModule(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

    }
}
