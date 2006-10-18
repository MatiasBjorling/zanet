using System;

namespace System.ComponentModel
{
	/// <summary>
	/// 
	/// </summary>
	public class AsyncCompletedEventArgs : EventArgs
	{   
		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		/// <param name="isCancelled"></param>
		/// <param name="exception"></param>
		/// <param name="state"></param>
		public AsyncCompletedEventArgs(bool isCancelled, Exception exception, object state)
		{
			_isCancelled = isCancelled;
			_exception = exception;
			_state = state;
		}

		#endregion Constructors

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public bool Cancelled
		{
			get { return _isCancelled; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Exception Error
		{
			get { return _exception; }
		}
		/// <summary>
		/// 
		/// </summary>
		public object State
		{
			get { return _state; }
		}
		/// <summary>
		/// 
		/// </summary>
		#endregion Properties

		#region Fields

		readonly Exception			_exception;
		readonly bool				_isCancelled;
		readonly object				_state;

		#endregion Fields
	}
}
