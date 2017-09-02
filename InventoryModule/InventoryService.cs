using System.ComponentModel.Composition;

namespace InventoryModule
{
    [Export(typeof(IInventoryService))]
    public class InventoryService:IInventoryService
    {
        public string GetViewName()
        {
            return "Inventory View";
        }
    }

    public interface IInventoryService
    {
        string GetViewName();
    }
}
