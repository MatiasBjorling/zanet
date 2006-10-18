using System;

namespace System.ComponentModel
{
	/// <summary>
	/// 
	/// </summary>
	public class DoWorkEventArgs : CancelEventArgs
	{
		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		/// <param name="argument"></param>
		public DoWorkEventArgs(object argument)
		{
			_argument = argument;
		}

		#endregion Constructors

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public object Argument
		{
			get { return _argument; }
		}
		/// <summary>
		/// 
		/// </summary>
		public object Result
		{
			get { return _result; }
			set { _result = value; }
		}

		#endregion Properties

		#region Fields
		/// <summary>
		/// 
		/// </summary>
		readonly object				_argument;
		/// <summary>
		/// 
		/// </summary>
		object						_result;

		#endregion Fields
	}
}
