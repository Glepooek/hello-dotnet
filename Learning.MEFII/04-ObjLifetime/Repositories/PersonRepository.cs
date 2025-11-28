using _04_ObjLifetime.DataAccess;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace _04_ObjLifetime.Repositories
{
    public class PersonRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IPerson
    {
        public ExportFactory<IMockDbContext> DbContext { get; set; }

        public PersonRepository(ExportFactory<IMockDbContext> dbContext)
        {
            DbContext = dbContext;
        }

        public TEntity GetByAge(Func<IPerson, bool> filter)
        {
            using (var ctx = DbContext.CreateExport())
            {
                return ctx.Value.MockPersonDbSet.AsEnumerable().SingleOrDefault(filter) as TEntity;
            }
        }
    }
}
