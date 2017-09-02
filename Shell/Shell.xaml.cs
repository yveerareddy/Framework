using System.ComponentModel.Composition;
using System.Windows;
using DevExpress.Xpf.Core;
using MahApps.Metro.Controls;

namespace Presentation.Shell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export]
    public partial class Shell : MetroWindow
    {
        public Shell()
        {
            InitializeComponent();
        }
    }
}
