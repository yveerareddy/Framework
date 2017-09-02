using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingModule
{
    [Export(typeof(IParcelViewModel))]
    public class ParcelViewModel:IParcelViewModel
    {
        public ParcelViewModel()
        {
            Title = "Parcel View";
        }
        public string Title { get; set; }
    }

    public interface IParcelViewModel
    {
        string Title { get; set; }
    }
}
