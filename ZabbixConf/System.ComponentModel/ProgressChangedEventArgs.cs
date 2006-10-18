using System;

namespace System.ComponentModel
{
	/// <summary>
	/// 
	/// </summary>
	public class ProgressChangedEventArgs : EventArgs
	{
		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		/// <param name="progressPercentage"></param>
		public ProgressChangedEventArgs(int progressPercentage) : this(progressPercentage, null)
		{
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="progressPercentage"></param>
		/// <param name="state"></param>
		public ProgressChangedEventArgs(int progressPercentage, object state)
		{
			_progressPercentage = progressPercentage;
			_state = state;
		}

		#endregion Constructors

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public int ProgressPercentage
		{
			get { return _progressPercentage; }
		}
		/// <summary>
		/// 
		/// </summary>
		public object State
		{
			get { return _state; }
		}

		#endregion Properties

		#region Fields
		/// <summary>
		/// 
		/// </summary>
		readonly int				_progressPercentage;
		/// <summary>
		/// 
		/// </summary>
		object						_state;

		#endregion Fields
	}
}
