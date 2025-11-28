using _05_SelectConstructor.Teaching;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Reflection;

namespace _05_SelectConstructor
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
            //Lazy<Learner> lazyLearner = new Lazy<Learner>(() =>
            //new Learner(container.GetExportedValues<Educator>().OfType<Teacher>().First()), true);

            //var learner = lazyLearner.Value;

            var exams = container.GetExportedValue<Learner>();

            Console.WriteLine("Human Characteristics Name: {0} LastName: {1}",
                exams.Name, exams.LastName);
            Console.ReadKey();
        }

        private void RegisterExports()
        {
            var builder = new RegistrationBuilder();

            // 选择用哪个构造函数初始化对象
            Func<ConstructorInfo[], ConstructorInfo> constructorFilter =
                x => x.First(z => z.GetParameters().First().ParameterType == typeof(IEnumerable<Educator>));
            builder.ForType<Learner>().SelectConstructor(constructorFilter).Export<Learner>();

            builder.ForTypesMatching(x => x.GetProperty("SourceMaterial") != null).Export<Exam.Exam>();
            builder.ForTypesDerivedFrom<Educator>().Export<Educator>();
            //builder.ForType<Learner>().Export<Learner>();

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
