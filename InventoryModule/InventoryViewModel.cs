using Prism.Commands;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;

namespace InventoryModule
{
    [Export(typeof(IInventoryViewModel))]
    public class InventoryViewModel:IInventoryViewModel,INotifyPropertyChanged
    {
        public string ViewName { get; set; }

        private bool _selectedMode;
        private string _selectedText;

        public bool SelectedMode
        {
            get
            {
                return _selectedMode;
            }

            set
            {
                _selectedMode = value;
                OnPropertyChanged(this, "SelectedMode");
            }
        }

        public string SelectedModeText
        {
            get
            {
                return _selectedText;
            }
            set
            {
                _selectedText = value;
                OnPropertyChanged(this, "SelectedModeText");
            }
           
        }

        public DelegateCommand ToggleCommand { get; set; }
        [ImportingConstructor]
        public InventoryViewModel(IInventoryService service)
        {
            ViewName = service.GetViewName();
            this.SelectedMode = true;
            this.SelectedModeText = "Injection";
            ToggleCommand = new DelegateCommand(OnToggleCommand);
        }

        private void OnToggleCommand()
        {
            if (SelectedMode == true)
            {
                this.SelectedModeText = "Delivery";
                SelectedMode = false;
            }
            else
            {
                this.SelectedModeText = "Injection";
                SelectedMode = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(object sender,string propertyName)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }

    public interface IInventoryViewModel
    {
        string ViewName { get; set; }
        bool SelectedMode { get; set; }
        string SelectedModeText
        {
            get; set;
        }
    }
}
