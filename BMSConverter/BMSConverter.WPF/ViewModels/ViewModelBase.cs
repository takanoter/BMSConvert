using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BMSConverter.WPF.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		#region Public

		#region - Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region - Methods

		public void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		public void OnPropertyChanged<T>(Expression<Func<T>> memberExpression)
		{
			MemberExpression body = (MemberExpression)memberExpression.Body;

			if (body == null)
			{
				throw new ArgumentNullException("memberExpression", "The body property of the memberExpression must not be null");
			}

			this.OnPropertyChanged(body.Member.Name);
		}

		public void OnPropertyChanged<T>(params Expression<Func<T>>[] memberExpressions)
		{
			foreach (var memberExpression in memberExpressions)
			{
				this.OnPropertyChanged(memberExpression);
			}
		}

		#endregion

		#endregion
		
	}
}
