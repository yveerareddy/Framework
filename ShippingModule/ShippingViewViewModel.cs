using System.ComponentModel.Composition;

namespace ShippingModule
{
    [Export(typeof(IShippingViewModel))]
    public class ShippingViewViewModel:IShippingViewModel
    {
        [ImportingConstructor]
        public ShippingViewViewModel(IShippingService service)
        {
            Title = service.GetTitle();
        }

        public string Title { get; set; }
    }

    public interface IShippingViewModel
    {
         string Title { get; set; }

    }
}
