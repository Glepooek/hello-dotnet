using Stylet.Samples.Ioc.Interfaces;
using StyletIoC;
using System.Collections.Generic;

namespace Stylet.Samples.Ioc
{
    public class ShellViewModel : Screen
    {
        //[Inject(Key ="new")]
        //public IVehicle Vehicle { get; set; }

        //public ShellViewModel([Inject(Key = "new")]IVehicle vehicle)
        //{ 

        //}

        public ShellViewModel([Inject(Key = "new")] IEnumerable<IVehicle> vehicles)
        {

        }

        //public ShellViewModel(IContainer container)
        //{
        //	//var vehicle = container.Get<IVehicle>("new");
        //	var vehicle = container.GetAll<IVehicle>("new");
        //}
    }
}
