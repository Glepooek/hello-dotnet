using SimpleAOPSamples.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAOP462Samples
{
    public class RepositoryFactory
    {
        public static IRepository<T> Create<T>()
        {
            var repository = new Repository<T>();
            var decoratedRepository =
                (IRepository<T>)new DynamicProxy<IRepository<T>>(repository) { Filter = m => !m.Name.StartsWith("Get") }
                .GetTransparentProxy();
            // Create a dynamic proxy for the class already decorated
            //decoratedRepository =
            //    (IRepository<T>)new AuthenticationProxy<IRepository<T>>(decoratedRepository)
            //    .GetTransparentProxy();
            return decoratedRepository;
        }
    }
}
