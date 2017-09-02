using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace WPFPresentationCore
{
    public class MefContainer
    {
        public static MefContainer Instance { get; set; }
        private CompositionContainer _container;

        static MefContainer()
        {
            Instance=new MefContainer();
        }
        public void ConfigureContainer(CompositionContainer container)
        {
          _container = container;
        }

        public void Register<T>(T exportedValue)
        {
            _container.ComposeExportedValue(exportedValue);
        }

        public T Resolve<T>()
        {
            return _container.GetExportedValueOrDefault<T>();
        }

        public T Resolve<T>(string type)
        {

            return _container.GetExportedValueOrDefault<T>(type);
        }
    }
}
