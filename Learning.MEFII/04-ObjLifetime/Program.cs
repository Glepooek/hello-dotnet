using _04_ObjLifetime.DataAccess;
using _04_ObjLifetime.PartCreation;
using _04_ObjLifetime.Repositories;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;

namespace _04_ObjLifetime
{
    class Program
    {
        public CompositionContainer container;

        static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
        }

        private void Run()
        {
            RegisterExports();
            //RunLifetimeExample();

            container.GetExportedValue<ProgramManager>().SimulateUserAction();
        }

        private void RunLifetimeExample()
        {
            container.GetExportedValue<LifeTimeManager>().ShowObjLifetime(container);
        }

        private void RegisterExports()
        {
            RegistrationBuilder builder = new RegistrationBuilder();
            builder.ForType(typeof(PersonRepository<>))
                   .Export(x => x.AsContractType(typeof(IRepository<>)));
            builder.ForTypesDerivedFrom<IMockDbContext>()
                   .Export<IMockDbContext>().SetCreationPolicy(CreationPolicy.NonShared);
            builder.ForType<ProgramManager>()
                   .Export<ProgramManager>();
            builder.ForType<LifeTimeManager>()
                   .Export<LifeTimeManager>();
            builder.ForType<ObjectTester>()
                   .Export<ObjectTester>().SetCreationPolicy(CreationPolicy.NonShared);

            Compose(builder);
        }

        private void Compose(RegistrationBuilder registeredExports)
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly(), registeredExports);
            container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);
            container.ComposeParts(this);
        }
    }
}
