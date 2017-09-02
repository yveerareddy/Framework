using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ShippingModule
{
    /// <summary>
    /// Interaction logic for ParcelView.xaml
    /// </summary>
    [Export(typeof(IParcelView))]
    public partial class ParcelView : UserControl,IParcelView
    {
        [ImportingConstructor]
        public ParcelView(IParcelViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }

    public interface IParcelView
    {
    }
}
