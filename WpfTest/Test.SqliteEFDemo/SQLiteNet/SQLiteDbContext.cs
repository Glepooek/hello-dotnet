using SQLite.CodeFirst;
using System.Data.Entity;
using Test.SqliteEFDemo.Models;

// https://www.cnblogs.com/AJun816/p/14458959.html
namespace Test.SqliteEFDemo.SQLiteNet
{
    public class SQLiteDbContext : DbContext
    {
        #region Singleton

        private SQLiteDbContext() : base("SqliteEFDemo")
        {

        }

        private static SQLiteDbContext mInstance;
        /// <summary>
        /// the instance of <see cref="SQLiteDbContext"/>
        /// </summary>
        public static SQLiteDbContext Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new SQLiteDbContext();
                }
                return mInstance;
            }
        }

        #endregion

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Valuation> Valuations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new SqliteCreateDatabaseIfNotExists<SQLiteDbContext>(modelBuilder));
        }
    }
}
