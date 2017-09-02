
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Prism.Regions;

namespace ShippingModule
{
    /// <summary>
    /// Interaction logic for ShippingView.xaml
    /// </summary>
    [Export(typeof(IShippingView))]
    public partial class ShippingView : UserControl, IShippingView
    {
        [Import]
        private IRegionManager _regionManager;
        [ImportingConstructor]
        public ShippingView(IShippingViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }


    }

    public interface IShippingView
    {
    }
}
