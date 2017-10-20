using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using System.Windows;
using MahApps.Metro.Controls;
using Presentation.Shell.Annotations;
using Prism.Regions;


namespace Presentation.Shell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class Shell :INotifyPropertyChanged
    {
        public Shell(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            this.DataContext = this;
            Prism.Regions.RegionManager.SetRegionManager(this,_regionManager);

        }

        private IRegionManager _regionManager;
        public IRegionManager RegionManager
        {
            set
            {
                _regionManager = value; 
                OnPropertyChanged();
            }
            get { return _regionManager; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
