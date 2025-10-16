using SqlSugar;
using System.Configuration;

namespace Test.DragControl.Helper
{
    public class SqlSugarHelper
    {
        #region Singleton

        private static SqlSugarHelper instance = null;
        private static readonly object syncRoot = new object();

        private SqlSugarHelper()
        {

        }

        public static SqlSugarHelper GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new SqlSugarHelper();
                    }
                }
            }

            return instance;
        }

        #endregion

        #region Properties

        private SqlSugarClient mSqlSugarClient = null;
        public SqlSugarClient SqlSugarClient
        {
            get
            {
                return mSqlSugarClient;
            }
        }

        #endregion

        #region Methods

        public void InitSqlSugarClient()
        {
            mSqlSugarClient = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConfigurationManager.AppSettings["MysqlStr"],
                DbType = DbType.MySql,
                IsAutoCloseConnection = true//自动释放
            });
        }

        #endregion
    }
}
