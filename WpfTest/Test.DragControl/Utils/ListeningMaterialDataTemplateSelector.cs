using System.Windows;
using System.Windows.Controls;
using Test.DragControl.Models;

namespace Test.DragControl.Utils
{
    public class ListeningMaterialDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ListeningMaterialTextContentDataTemplate
        {
            get;
            set;
        }

        public DataTemplate ListeningMaterialPauseContentDataTemplate
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ListeningTextContent)
            {
                return ListeningMaterialTextContentDataTemplate;
            }
            else if (item is ListeningPauseContent)
            {
                return ListeningMaterialPauseContentDataTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
