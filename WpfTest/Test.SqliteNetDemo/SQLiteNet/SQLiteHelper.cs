using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Test.SqliteNetDemo.SQLite;

namespace Test.SqliteNetDemo.SQLiteNet
{
    public class SQLiteHelper
    {
        #region Fields
        private SQLiteAsyncConnection _db;
        private static readonly string _dbPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\warehouse.db";
        #endregion

        #region Singleton

        private SQLiteHelper()
        {
            _db = new SQLiteAsyncConnection(_dbPath);
            var types = GetTableTypes();
            // 表，未创建则创建，已创建则不创建
            _db.CreateTablesAsync(CreateFlags.None, types);
        }

        private static Lazy<SQLiteHelper> instance = new Lazy<SQLiteHelper>(() => new SQLiteHelper());

        /// <summary>
        /// the instance of <see cref="SQLiteHelper"/>
        /// </summary>
        public static SQLiteHelper Instance
        {
            get { return instance.Value; }
        }

        #endregion

        #region PublicMethods

        public async Task<int> AddAsync<T>(T model)
        {
            return await _db.InsertAsync(model);
        }

        public async Task<int> AddAllAsync<T>(IEnumerable<T> models)
        {
            return await _db.InsertAllAsync(models);
        }

        public async Task<int> UpdateAsync<T>(T model)
        {
            return await _db.UpdateAsync(model);
        }

        public async Task<int> UpdateAllAsync<T>(IEnumerable<T> models)
        {
            return await _db.UpdateAllAsync(models);
        }

        public async Task<int> DeleteAsync<T>(T model)
        {
            return await _db.DeleteAsync(model);
        }

        public async Task<int> DeleteAllAsync<T>()
        {
            return await _db.DeleteAllAsync<T>();
        }

        public async Task<List<T>> GetAllAsync<T>(string sql) where T : class, new()
        {
            return await _db.QueryAsync<T>(sql);
        }

        public async Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            return await _db.Table<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> GetOneAsync<T>(Predicate<T> predicate) where T : class, new()
        {
            return await _db.FindAsync<T>(predicate);
            //return await _db.GetAsync<T>(predicate);
        }
        #endregion

        #region PrivateMethods

        //Func<Attribute[], bool> IsTableAttribute = attributes =>
        //{
        //    if (attributes == null || attributes.Length == 0)
        //    {
        //        return false;
        //    }

        //    return attributes.Any(a => a is TableAttribute);
        //};

        Func<TableAttribute[], bool> IsTableAttribute = attributes =>
        {
            if (attributes == null)
            {
                return false;
            }

            return attributes.Length > 0;
        };

        public Type[] GetTableTypes()
        {
            var assembly = Assembly.Load("Test.SqliteNetDemo");
            Type[] types = assembly.GetExportedTypes().Where(t =>
            {
                return IsTableAttribute(t.GetCustomAttributes<TableAttribute>().ToArray());
            }).ToArray();

            return types;
        }
        #endregion
    }
}
