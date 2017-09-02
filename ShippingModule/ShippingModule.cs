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

        [Import]
        private INavigationService _navigationService;

        [Import]
        private IShippingView _shipmentView;

        [Import]
        private IParcelView _parcelView;

        public void Initialize()
        {
            //_navigationService.ActivateViewInRegion(Constants.ShipmentsRegion, _shipmentView, Constants.ShipmentView);
            //_navigationService.ActivateViewInRegion(Constants.ParcelRegion, _parcelView, Constants.ParcelView);

        }

        [ImportingConstructor]
        public ShippingModule(IRegionManager regionManager, IShippingView shipmentview,IParcelView parcelView)
        {

        }
    }
}
