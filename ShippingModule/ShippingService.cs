using System.ComponentModel.Composition;

namespace ShippingModule
{
    [Export(typeof(IShippingService))]
    public class ShippingService:IShippingService
    {
        public string GetTitle()
        {
            return "Shipping View";
        }
    }

    public interface IShippingService
    {
        string GetTitle();
    }
}
