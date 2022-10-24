using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WindowRemoteFileTransport.CommonClass
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        Action<object> _executeMethod;
        Func<bool> canExecute;
        Func<object, bool> _canexecuteMethod;
        Action _execute;

        public Command(Action<object> executeMethod, Func<object, bool> canexecuteMethod)
        {
            this._executeMethod = executeMethod;
            this._canexecuteMethod = canexecuteMethod;
        }
        public Command(Action execute, Func<bool> canExecute)
        {
            this._execute = execute;
            this.canExecute = canExecute;
        }
        public Command(Action execute)
        {
            _execute = execute;
        }

        public Command(Action<object> execute)
        {
            _executeMethod = execute;
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }
            //throw new NotImplementedException();
            return this.canExecute();
        }

        public void Execute(object parameter)
        {
            //throw new NotImplementedException();

            _executeMethod(parameter);

        }

        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
    class DelegateCommand : ICommand
    {
        private readonly Func<bool> canExecute;
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        private readonly Action execute;


        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new NullReferenceException("execute can not null");

            _execute = execute;
            _canExecute = canExecute;
        }

        public DelegateCommand(Action<object> execute) : this(execute, null)
        {

        }


        public DelegateCommand(Action exectue) : this(exectue, null)
        {

        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// can executes event handler
        /// </summary>

        /// <summary>
        /// implement of icommand can execute method
        /// </summary>
        /// <param name="o">parameter by default of icomand interface</param>
        /// <returns>can execute or not</returns>
        public bool CanExecute(object o)
        {
            if (this.canExecute == null)
            {
                return true;
            }
            return this.canExecute();
        }

        /// <summary>
        /// implement of icommand interface execute method
        /// </summary>
        /// <param name="o">parameter by default of icomand interface</param>
        public void Execute(object o)
        {
            this.execute();
        }

        /// <summary> 
        /// raise ca excute changed when property changed
        /// </summary>

    }
}
