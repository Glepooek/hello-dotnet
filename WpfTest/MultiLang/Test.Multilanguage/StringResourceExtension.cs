using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Test.Multilanguage
{
    [MarkupExtensionReturnType(typeof(BindingExpression))]
    public class StringResourceExtension : MarkupExtension, INotifyPropertyChanged
    {
        #region Properties
        /// <summary>
        /// 资源的名称
        /// </summary>
        /// <remarks>
        /// 与资源文件StringResource.resx对应
        /// </remarks>
        [ConstructorArgument("key")]
        public string Key
        {
            get;
            set;
        }

        string mDesignValue;
        /// <summary>
        /// 设计模式值。在设计器模式时显示该值
        /// </summary>
        public string DesignValue
        {
            get
            {
                return mDesignValue;
            }
            set
            {
                mDesignValue = value;
            }
        }

        string mValue;
        /// <summary>
        /// 资源的具体内容，通过资源名称也就是上面的Key找到对应内容
        /// </summary>
        public string Value
        {
            get
            {
                // 如果为设计器模式，本地的资源没有实例化，不能从资源文件得到内容，所以将KEY或默认值绑定到设计器去
                if (GlobalConfig.IsDesignMode)
                {
                    if (Key != null && DesignValue != null)
                        return DesignValue;
                    if (Key == null && DesignValue != null)
                        return DesignValue;
                    if (Key != null && DesignValue == null)
                        return Key;
                    if (Key == null && DesignValue == null)
                        return "NULL";
                }
                else
                {
                    if (Key != null)
                    {
                        string strResault = null;
                        try
                        {
                            strResault = GlobalConfig.GetString(Key);
                        }
                        catch
                        {
                        }
                        if (strResault == null)
                        {
                            strResault = mDesignValue;
                        }
                        return strResault;
                    }
                }
                return mValue;
            }
            set
            {
                mValue = value;
            }
        }
        #endregion

        #region Constructor
        public StringResourceExtension()
        {
        }

        public StringResourceExtension(string key) : this()
        {
            Key = key;
            GlobalConfig.LanguageChangedEvent += new EventHandler<EventArgs>(OnLanguageChanged);
        }

        public StringResourceExtension(string key, string defaultValue) : this()
        {
            Key = key;
            mDesignValue = defaultValue;
            GlobalConfig.LanguageChangedEvent += new EventHandler<EventArgs>(OnLanguageChanged);

        }
        #endregion

        #region Methods
        /// <summary>
        /// 每一标记扩展实现的 ProvideValue 方法能在可提供上下文的运行时使用 IServiceProvider。然后会查询此 IServiceProvider 以获取传递信息的特定服务
        /// 当 XAML 处理器在处理一个类型节点和成员值，且该成员值是标记扩展时，它将调用该标记扩展的 ProvideValue 方法并将结果写入到对象关系图或序列化流,XAML 对象编写器将服务环境通过 serviceProvider 参数传递到每个此类实现。
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;

            Setter setter = target.TargetObject as Setter;
            if (setter != null)
            {
                return new Binding(nameof(Value)) { Source = this, Mode = BindingMode.OneWay };
            }
            else
            {
                Binding binding = new Binding(nameof(Value)) { Source = this, Mode = BindingMode.OneWay };
                return binding.ProvideValue(serviceProvider);
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly PropertyChangedEventArgs propertyChangedEventArgs = new PropertyChangedEventArgs(nameof(Value));

        protected void RaisePropertyChanged()
        {
            PropertyChanged?.Invoke(this, propertyChangedEventArgs);
        }

        /// <summary>
        /// 语言改变通知事件，当程序初始化的时候会绑定到全局的GlobalClass.LanguageChangeEvent事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLanguageChanged(object sender, EventArgs e)
        {
            // 通知Value值已经改变，需重新获取
            RaisePropertyChanged();
        }
        #endregion
    }
}
