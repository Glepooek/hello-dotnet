using Learning.PrismDemo.Models;
using Learning.PrismDemo.Setting.Views;
using Learning.PrismDemo.Utilities;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Learning.PrismDemo.Setting.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        #region Fields

        #endregion

        #region Properties

        private ObservableCollection<SettingMenuItem> m_SettingMenuItems;
        /// <summary>
        /// 设置菜单项集合
        /// </summary>
        public ObservableCollection<SettingMenuItem> SettingMenuItems
        {
            get
            {
                return m_SettingMenuItems;
            }
            set
            {
                if (m_SettingMenuItems != value)
                {
                    m_SettingMenuItems = value;
                    RaisePropertyChanged();
                }
            }
        }

        private UserControl m_CurrentMenuItemContent;
        /// <summary>
        /// 当前选中的菜单项
        /// </summary>
        public UserControl CurrentMenuItemContent
        {
            get
            {
                return m_CurrentMenuItemContent;
            }
            set
            {
                if (m_CurrentMenuItemContent != value)
                {
                    m_CurrentMenuItemContent = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Commands

        protected override void InitCommands()
        {
            base.InitCommands();

        }

        #endregion

        #region Constructor

        /// <summary>
        /// 初始化<see cref="SettingViewModel"/>类实例
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="regionManager"></param>
        public SettingViewModel(IContainerExtension containerExtension,
                                IEventAggregator eventAggregator,
                                IRegionManager regionManager,
                                IDialogService dialogService)
            : base(containerExtension, eventAggregator, regionManager, dialogService)
        {
            InitMenuItems();
        }

        #endregion

        #region Methods

        private void InitMenuItems()
        {
            SettingMenuItems = new ObservableCollection<SettingMenuItem>()
            {
                new SettingMenuItem(){  MenuItemName ="摄像头", MenuItemContent = new CamerasView()},
                new SettingMenuItem(){  MenuItemName ="账号", MenuItemContent = new AccountView()},
                new SettingMenuItem(){  MenuItemName ="版本", MenuItemContent = new VersionView()},
                new SettingMenuItem(){ MenuItemName = "关于", MenuItemContent = new AboutView()}
            };
        }

        #endregion
    }
}
