using System;

namespace Stylet.Samples.OverridingViewManager
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ViewModelAttribute : Attribute
    {
        private readonly Type mViewModel;
        public Type ViewModel
        {
            get { return mViewModel; }
        }

        public ViewModelAttribute(Type viewModel)
        {
            this.mViewModel = viewModel;
        }
    }
}
