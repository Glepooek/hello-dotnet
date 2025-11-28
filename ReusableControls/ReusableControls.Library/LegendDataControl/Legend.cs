using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Digihail.Controls
{
    public class Legend : INotifyPropertyChanged
    {
        #region Methods

        protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            RaisePropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;

            return propertyName;
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Fields & Properties

        private Brush brush;
        /// <summary>
        /// 颜色
        /// </summary>
        public Brush Brush
        {
            get
            {
                return brush;
            }
            set
            {
                if(value != brush)
                {
                    brush = value;
                    RaisePropertyChanged(() => Brush);
                }
            }
        }

        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if(value != name)
                {
                    name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        private double number;
        /// <summary>
        /// 数值
        /// </summary>
        public double Number
        {
            get
            {
                return number;
            }
            set
            {
                if(value != number)
                {
                    number = value;
                    RaisePropertyChanged(() => Number);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
