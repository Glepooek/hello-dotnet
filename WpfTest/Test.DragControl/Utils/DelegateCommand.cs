using System;
using System.Windows.Input;

namespace Test.DragControl.Utils
{
    /// <summary>
    /// 命令
    /// </summary>
    public class DelegateCommand : ICommand
    {
        #region Fields

        private Action m_Execute;
        private Func<bool> m_CanExecute;

        private Action<object> m_ParaExecute;
        private Func<object, bool> m_ParaCanExecute;

        #endregion

        /// <summary>
        /// 自定义命令构造函数
        /// </summary>
        /// <param name="execute">命令被执行时调用的方法</param>
        public DelegateCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// 自定义命令构造函数
        /// </summary>
        /// <param name="execute">命令被执行时调用的方法</param>
        /// <param name="canExecute">当前命令是否可以执行</param>
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            m_Execute = execute;
            m_CanExecute = canExecute;
        }

        /// <summary>
        /// 自定义命令构造函数
        /// </summary>
        /// <param name="execute">命令被执行时调用的方法</param>
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// 自定义命令构造函数
        /// </summary>
        /// <param name="execute">命令被执行时调用的方法</param>
        /// <param name="canExecute">当前命令是否可以执行</param>
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            m_ParaExecute = execute;
            m_ParaCanExecute = canExecute;
        }

        #region ICommand Member

        /// <summary>
        /// 命令是否能够执行状态改变事件
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (m_CanExecute != null || m_ParaCanExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (m_CanExecute != null || m_ParaCanExecute != null)
                {
                    CommandManager.RequerySuggested -= value;

                }
            }
        }

        /// <summary>
        /// 当前命令是否可以执行
        /// </summary>
        /// <param name="parameter">命令参数</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (null != m_CanExecute)
            {
                return m_CanExecute();
            }
            else if (null != m_ParaCanExecute)
            {
                return m_ParaCanExecute(parameter);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter">命令参数</param>
        public void Execute(object parameter)
        {
            if (null != m_Execute)
            {
                m_Execute();
            }
            else if (null != m_ParaExecute)
            {
                m_ParaExecute(parameter);
            }
        }
        #endregion
    }

    /// <summary>
    /// 命令
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateCommand<T> : ICommand
    {
        #region Fields

        private Action<T> _execute;
        private Func<T, bool> _canExecute;

        #endregion

        /// <summary>
        /// 自定义命令构造函数
        /// </summary>
        /// <param name="execute">命令被执行时调用的方法</param>
        public DelegateCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// 自定义命令构造函数
        /// </summary>
        /// <param name="execute">命令被执行时调用的方法</param>
        /// <param name="canExecute">当前命令是否可以执行</param>
        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        #region ICommand Member

        /// <summary>
        /// 命令是否能够执行状态改变事件
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;

                }
            }
        }

        /// <summary>
        /// 当前命令是否可以执行
        /// </summary>
        /// <param name="parameter">命令参数</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (null != _canExecute)
            {
                return _canExecute((T)parameter);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter">命令参数</param>
        public void Execute(object parameter)
        {
            if (null != _execute && parameter != null)
            {
                _execute((T)parameter);
            }
        }
        #endregion
    }
}
