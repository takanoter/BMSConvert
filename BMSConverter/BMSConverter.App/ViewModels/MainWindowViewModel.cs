using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMSConverter.WPF.ViewModels;

namespace BMSConverter.App.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		#region Private

		#region - Vars

		private BmsMenuBarViewModel _bmsMenuBarVm;
		private BmsExportViewModel _bmsExportVm;

		#endregion

		#endregion

		#region Public

		#region - Constructors

		public MainWindowViewModel()
		{
			this._bmsMenuBarVm = new BmsMenuBarViewModel();
			this._bmsExportVm = new BmsExportViewModel();
		}

		#endregion

		#region - Properties

		/// <summary>
		/// Gets or sets the current window width
		/// </summary>
		public double WindowWidth
		{
			get { return Properties.Settings.Default.WindowWidth; }

			set
			{
				if (Properties.Settings.Default.WindowWidth != value)
				{
					Properties.Settings.Default.WindowWidth = value;
					base.OnPropertyChanged(() => this.WindowWidth);
				}
			}
		}

		/// <summary>
		/// Gets or sets the current window height
		/// </summary>
		public double WindowHeight
		{
			get { return Properties.Settings.Default.WindowHeight; }

			set
			{
				if (Properties.Settings.Default.WindowHeight != value)
				{
					Properties.Settings.Default.WindowHeight = value;
					base.OnPropertyChanged(() => this.WindowHeight);
				}
			}
		}

		public BmsMenuBarViewModel BmsMenuBarVm
		{
			get { return this._bmsMenuBarVm; }

			set
			{
				if (this._bmsMenuBarVm != value)
				{
					this._bmsMenuBarVm = value;
					base.OnPropertyChanged(() => this.BmsMenuBarVm);
				}
			}
		}

		public BmsExportViewModel BmsExportVm
		{
			get { return this._bmsExportVm; }

			set
			{
				if (this._bmsExportVm != value)
				{
					this._bmsExportVm = value;
					base.OnPropertyChanged(() => this.BmsExportVm);
				}
			}
		}

		#endregion

		#endregion
	}
}
