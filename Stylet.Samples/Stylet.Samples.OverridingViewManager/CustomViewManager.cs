using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Stylet.Samples.OverridingViewManager
{
    /// <summary>
    /// 自定义ViewManager
    /// <para>
    /// 修改默认的通过ViewModel查找View的方式。
    /// 修改为在View类上添加ViewModelAttribute特性来为ViewModel指定View。
    /// </para>
    /// </summary>
    public class CustomViewManager : ViewManager
    {
        // the dic of viewmodel type to view type
        private readonly Dictionary<Type, Type> viewModelToViewMappings;

        public CustomViewManager(ViewManagerConfig viewManagerConfig)
            : base(viewManagerConfig)
        {
            var mappings = from type in this.ViewAssemblies.SelectMany(x => x.GetExportedTypes())
                           let attribute = type.GetCustomAttribute<ViewModelAttribute>()
                           // Attribute不能为空，且View是一个UIElement，或者继承自UIElement
                           where attribute != null && typeof(UIElement).IsAssignableFrom(type)
                           select new { ViewModel = attribute.ViewModel, View = type };

            this.viewModelToViewMappings = mappings.ToDictionary(x => x.ViewModel, x => x.View);
        }

        protected override Type LocateViewForModel(Type modelType)
        {
            if (!this.viewModelToViewMappings.TryGetValue(modelType, out Type viewType))
            {
                throw new Exception($"Could not find View for ViewModel {modelType.Name}");
            }

            return viewType;
        }
    }
}
