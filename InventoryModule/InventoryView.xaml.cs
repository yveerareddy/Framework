using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace InventoryModule
{
    /// <summary>
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    [Export(typeof(IInventoryView))]
    public partial class InventoryView : UserControl, IInventoryView
    {
        [ImportingConstructor]
        public InventoryView(IInventoryViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void changeButBorderedBlinky_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }

    public interface IInventoryView
    {
        object DataContext { get; set; }
    }
}
