using Learning.PrismDemo.Controls.DialogServices;
using Learning.PrismDemo.Utilities;
using Prism.Commands;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System.Reflection;

namespace Learning.PrismDemo.Setting.ViewModels
{
    public class VersionViewModel : ViewModelBase
    {
        #region Fields


        #endregion

        #region Properties

        /// <summary>
        /// 主程序版本号
        /// </summary>
        public string VisionNumber => GetVisionNumber();

        #endregion

        #region Commands

        /// <summary>
        /// 检查更新命令
        /// </summary>
        public DelegateCommand CheckUpdateCommand { get; private set; }

        protected override void InitCommands()
        {
            CheckUpdateCommand = new DelegateCommand(OnCheckUpdate);
        }

        private void OnCheckUpdate()
        {
            mDialogService.ShowAlert("当前版本已是最新版本！");
        }

        #endregion

        #region Constructor

        public VersionViewModel(IContainerExtension containerExtension, IDialogService dialogService)
            : base(containerExtension, dialogService)
        {

        }

        #endregion

        #region PrivateMehtods

        /// <summary>
        /// 获取主程序的版本号
        /// </summary>
        /// <returns></returns>
        private string GetVisionNumber()
        {
            return Assembly.GetEntryAssembly().GetName().Version.ToString(3);
        }

        #endregion
    }
}
