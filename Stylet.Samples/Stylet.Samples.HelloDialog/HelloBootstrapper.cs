using StyletIoC;

namespace Stylet.Samples.HelloDialog
{
    public class HelloBootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            builder.Bind<IDialogFactory>().ToAbstractFactory();
        }
    }
}
