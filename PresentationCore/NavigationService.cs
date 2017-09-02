using Prism.Regions;
using System;
using System.ComponentModel.Composition;


namespace WPFPresentationCore
{

    [Export(typeof(INavigationService))]
    public class NavigationService : INavigationService
    {
        private IRegionManager _regionManager;

        [ImportingConstructor]
        public NavigationService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void ActivateViewInRegion(string regionName, object view, string viewName)
        {
            if(! _regionManager.Regions.ContainsRegionWithName(regionName))
            {
                IRegion region = new Region();
                _regionManager.Regions.Add(regionName, region);
            }

            var activeview = _regionManager.Regions[regionName].GetView(viewName);

            if (activeview == null)
            {
                _regionManager.Regions[regionName].Add(view,viewName);
                _regionManager.Regions[regionName].Activate(view);
                return;
            }

            _regionManager.Regions[regionName].Activate(view);
        }
        
    }

    public interface INavigationService
    {

        void ActivateViewInRegion(string regionName, object view, string viewName);

    }
}
