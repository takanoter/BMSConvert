using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BMSConverter.WPF.Commands;

namespace BMSConverter.WPF.ViewModels
{
	public class BmsMenuBarViewModel : ViewModelBase
	{
		#region Private

		#region - Vars

		private DelegateCommand _openBmsCommand;
		private DelegateCommand _exitCommand;

		#region - Methods

		private void OpenBms(object parameter)
		{

		}

		private void Exit(object parameter)
		{
			
		}

		#endregion

		#endregion

		#endregion

		#region Public

		#region - Properties

		public ICommand ExitCommand
		{
			get
			{
				if (this._exitCommand == null)
					this._exitCommand = new DelegateCommand(this.Exit);

				return this._exitCommand;
			}
		}

		public ICommand OpenBmsCommand
		{
			get
			{
				if (this._openBmsCommand == null)
					this._openBmsCommand = new DelegateCommand(this.OpenBms);

				return this._openBmsCommand;
			}
		}

		#endregion

		#endregion
	}
}
