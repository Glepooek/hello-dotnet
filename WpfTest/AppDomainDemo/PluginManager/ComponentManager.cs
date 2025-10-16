using System;

namespace PluginManager
{
    public class ComponentManager : MarshalByRefObject
    {
        // 每个组件对应一个 AppDomain 和 Loader 代理
        private AppDomain mComponentDomain;
        private ComponentLoader mComponentLoader;

        public override object InitializeLifetimeService() => null;

        public void RunFlashcard(FlashcardTeachingParams teachingParams)
        {
            RunComponentInDomain(teachingParams, "Flashcard.dll");
        }

        public void RunEbook(EBookTeachingParams teachingParams)
        {
            RunComponentInDomain(teachingParams, "EbookReader.dll");
        }

        /// <summary>
        /// 在新域中加载组件
        /// </summary>
        private void RunComponentInDomain(TeachingParams teachingParams, string dllName)
        {
            // 清理旧域
            Unload();

            // 创建新应用域
            var setup = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile,
                ShadowCopyFiles = "true"

            };

            mComponentDomain = AppDomain.CreateDomain($"ComponentDomain_{Guid.NewGuid()}", null, setup);

            // 跨域创建 Loader
            mComponentLoader = (ComponentLoader)mComponentDomain.CreateInstanceAndUnwrap(
                typeof(ComponentLoader).Assembly.FullName,
                typeof(ComponentLoader).FullName);

            // 在新域中加载组件
            mComponentLoader.RunComponent(teachingParams, dllName);
        }

        /// <summary>
        /// 关闭并卸载域
        /// </summary>
        public void Unload()
        {
            if (mComponentDomain == null) return;

            // Loader释放资源
            mComponentLoader?.Unload();
            // 卸载整个应用域。资源并没有释放，主动调用了GC
            AppDomain.Unload(mComponentDomain);

            mComponentLoader = null;
            mComponentDomain = null;

            GC.Collect();
        }
    }
}
