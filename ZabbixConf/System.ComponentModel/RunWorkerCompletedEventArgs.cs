using System;

namespace System.ComponentModel
{
	/// <summary>
	/// 
	/// </summary>
	public class RunWorkerCompletedEventArgs : AsyncCompletedEventArgs
	{          
		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		/// <param name="result"></param>
		/// <param name="exception"></param>
		/// <param name="isCancelled"></param>
		public RunWorkerCompletedEventArgs(object result, Exception exception, bool isCancelled) : base(isCancelled, exception, null)
		{                
			_result = result;
		}

		#endregion Constructors

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public object Result
		{
			get
			{ 
				if(base.Error != null)
					throw base.Error;

				if(base.Cancelled)
					throw new InvalidOperationException("Background operation was cancelled.");

				return _result;
			}
		}

		#endregion Properties

		#region Fields
		/// <summary>
		/// 
		/// </summary>
		readonly object				_result;

		#endregion Fields
	}
}
