using System.Globalization;
using System.Resources;

namespace Test.Multilanguage
{
    public interface IResource
    {
        string GetResourceString(string name);
        CultureInfo CurrentCulture { set; }
    }

    public class Resource : IResource
    {
        #region Fields
        private ResourceManager mResourceManager;
        #endregion

        #region Properties
        private CultureInfo mCulture = new CultureInfo("zh-cn");
        /// <summary>
        /// 当前的区域信息
        /// </summary>
        /// <remarks>
        /// ResourceManager可以通过当前区域信息去查找.resx文件
        /// </remarks>
        public CultureInfo CurrentCulture
        {
            get => mCulture;
            set
            {
                mCulture = value;
            }
        }
        #endregion

        #region Constructor
        public Resource()
        {
            // Test.Multilanguage.Resources.StringResource是根名称，
            // 该实例使用指定的System.Reflection.Assmbly查找从指定的跟名称导出的文件中包含的资源
            mResourceManager = new ResourceManager("Test.Multilanguage.Resources.StringResource", typeof(Resource).Assembly);
        }
        #endregion

        #region Methods
        /// <summary>
        /// 通过资源名称获取资源内容
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetResourceString(string name)
        {
            return mResourceManager.GetString(name, mCulture);
        }
        #endregion
    }
}
