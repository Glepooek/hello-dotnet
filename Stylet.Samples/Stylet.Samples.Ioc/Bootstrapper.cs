using Stylet.Samples.Ioc.Implementors;
using Stylet.Samples.Ioc.Interfaces;
using StyletIoC;

/***
 * 有三种方式获取StyletIoC中注入的服务：
 * 1、通过Container.Get<T>();
 * 2、构造函数注入，需要使用Bind<...>().To<...>()方式注册服务
 * 3、字段、属性注入，需要[Inject]特性标签。字段、属性注入是在构造函数注入之后发生。
 * 
 * 有两种方式指定与类型关联的Key：
 * 1、通过具体类型自身的[Inject(Key = "old")]属性指定，如：
 *  [Inject(Key = "old")]
    public class OldBanger : IVehicle
    {
    }
 * 2、使用WithKey方法在绑定类型时指定，这将覆盖属性中的键（如果有）：
 * builder.Bind<IVehicle>().To<HotHatchback>().WithKey("new");
 * builder.Bind<IVehicle>().To(container => new OldBanger()).WithKey("old");
 * 
 * 3、功能工厂和抽象工厂、在工厂方法中使用Key
 * 功能工厂和抽象工厂可参看Stylet.Samples.HelloDialog项目
 * public interface IDialogViewModelFactory
{
   [Inject(Key = "someKey")]
   DialogViewModel CreateDialogViewModel();
}
或
 *public interface IDialogViewModelFactory
{
   DialogViewModel CreateDialogViewModel(string key);
}
 * 
 * 4、Modules
 * 当程序有大量依赖注入时，可以用Modules拆分
 * 
 * *****/

namespace Stylet.Samples.Ioc
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            // Type Binding
            //builder.Bind<IVehicle>().To<HotHatchback>();
            //builder.Bind(typeof(IVehicle)).To(typeof(HotHatchback));
            //builder.Bind<IEngine>().To<Engine>();

            // 类型绑定时指定Key：
            builder.Bind<IEngine>().To<Engine>();
            builder.Bind<IVehicle>().To<HotHatchback>().WithKey("new");
            builder.Bind<IVehicle>().To<Roadster>().WithKey("new");
            builder.Bind<IVehicle>().To<OldBanger>().WithKey("old");

            // Type Binding，绑定到服务自身
            //builder.Bind<HotHatchback>().To<HotHatchback>();
            //builder.Bind<HotHatchback>().ToSelf();
            //builder.Bind<Engine>().ToSelf();

            // Factory Binding
            //builder.Bind<IVehicle>().ToAbstractFactory();
            //builder.Bind<IVehicle>().ToFactory(context => new HotHatchback());
            //builder.Bind<IVehicle>().ToFactory(context => new HotHatchback(Container.Get<Engine>()));
            //builder.Bind<HotHatchback>().ToFactory(context => new HotHatchback());

            // Instance Binding，自动单例
            //builder.Bind<IVehicle>().ToInstance(new HotHatchback());

            // 单例作用域
            // Container.Get<IVehicle>();每次获取的都是单例
            //builder.Bind<IVehicle>().To<HotHatchback>().InSingletonScope();
            //builder.Bind<IVehicle>().ToFactory(context => new HotHatchback()).InSingletonScope();


            // 将多个类型绑定到同一服务
            //builder.Bind<IVehicle>().To<HotHatchback>();
            //builder.Bind<IVehicle>().To<OldBanger>();
            // ***绑定到所有实现****
            //builder.Bind<IVehicle>().ToAllImplementations();
            //IEnumerable<IVehicle> vehicles = Container.GetAll<IVehicle>();

            // 绑定到泛型类型，指定具体T类型
            //builder.Bind<IValidator<int>>().To<IntValidator>();
            //builder.Bind<IValidator<int>>().To<Validator<int>>();
            // ***绑定到所有实现****
            //builder.Bind(typeof(IValidator<>)).ToAllImplementations();

            // 绑定到泛型类型，不指定具体T类型
            //builder.Bind(typeof(IValidator<>)).To(typeof(Validator<>));
            //Container.Get<IValidator<string>>();

            // *********添加Module*********
            //builder.AddModules(new FactoriesModule(), new ServiceModule());
        }
    }
}
