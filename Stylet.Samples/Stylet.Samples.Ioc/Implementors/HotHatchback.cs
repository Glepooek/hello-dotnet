using Stylet.Samples.Ioc.Interfaces;

namespace Stylet.Samples.Ioc.Implementors
{
    public class HotHatchback : IVehicle
    {
        //[Inject]
        //public IEngine Engine { get; set; }

        public HotHatchback()
        {
            // 使用默认引擎
        }

        public HotHatchback(IEngine engine)
        {

        }
    }
}
