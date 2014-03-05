using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BMSConverter.WPF.Commands
{
	public class DelegateCommand : ICommand
	{
		#region Private

		private Action<object> _execute;
		private Predicate<object> _canExecute;

		#endregion

		#region Public

		#region - Events

		public event EventHandler CanExecuteChanged;

		#endregion

		#region - Constructors

		public DelegateCommand(Action<object> execute) : this(execute, null)
		{}

		public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
		{
			this._execute = execute;
			this._canExecute = canExecute;
		}

		#endregion

		#region - Methods

		public void Execute(object parameter)
		{
			if (this._execute != null)
			{
				this._execute(parameter);
			}
		}

		public bool CanExecute(object parameter)
		{
			if (this._canExecute != null)
			{
				return this._canExecute(parameter);
			}
			else
			{
				return true;
			}
		}

		#endregion

		#endregion
	}
}
