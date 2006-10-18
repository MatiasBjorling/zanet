using System;
using System.ComponentModel;
 
namespace System.ComponentModel
{
#if !NET_2_0
	/// <summary>
	/// 
	/// </summary>
	public class BackgroundWorker
	{
		#region Events
		/// <summary>
		/// 
		/// </summary>
		public event                DoWorkEventHandler DoWork;
		/// <summary>
		/// 
		/// </summary>
		public event                ProgressChangedEventHandler ProgressChanged;
		/// <summary>
		/// 
		/// </summary>
		public event                RunWorkerCompletedEventHandler RunWorkerCompleted;
 
		#endregion Events
 
		#region Methods
		/// <summary>
		/// 
		/// </summary>
		public void CancelAsync()
		{
			lock(this)
				_isCancelPending = true;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="handler"></param>
		/// <param name="args"></param>
		void InvokeDelegate(Delegate handler, object[] args)
		{
			ISynchronizeInvoke synchronizer = (ISynchronizeInvoke)handler.Target;
 
			if(synchronizer == null)
			{
				handler.DynamicInvoke(args);
				return;
			}
 
			if(synchronizer.InvokeRequired == false)
			{
				handler.DynamicInvoke(args);
				return;
			}
 
			synchronizer.Invoke(handler, args);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="asyncResult"></param>
		void ReportCompletion(IAsyncResult asyncResult)
		{
			System.Runtime.Remoting.Messaging.AsyncResult ar = (System.Runtime.Remoting.Messaging.AsyncResult)asyncResult;
 
			DoWorkEventHandler del;
 
			del  = (DoWorkEventHandler)ar.AsyncDelegate;
 
			DoWorkEventArgs doWorkArgs = (DoWorkEventArgs)ar.AsyncState;
 
			object result = null;
 
			Exception error = null;
 
			try
			{
				del.EndInvoke(asyncResult);
				result = doWorkArgs.Result;
			}
			catch(Exception exception)
			{
				error = exception;
			}
 
			object[] args = new object[] { this, new RunWorkerCompletedEventArgs(result, error, doWorkArgs.Cancel) };
 
			foreach(Delegate handler in RunWorkerCompleted.GetInvocationList())
				InvokeDelegate(handler, args);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="percent"></param>
		public void ReportProgress(int percent)
		{
			if(_isProgressReported == false)
				throw new InvalidOperationException("Background worker does not report its progress");
 
			object[] args = new object[] { this, new ProgressChangedEventArgs(percent, null) };
 
			foreach(Delegate handler in ProgressChanged.GetInvocationList())
				InvokeDelegate(handler, args);
		}
		/// <summary>
		/// 
		/// </summary>
		public void RunWorkerAsync()
		{
			RunWorkerAsync(null);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="argument"></param>
		public void RunWorkerAsync(object argument)
		{
			if(DoWork == null)
				return;
                                
			_isCancelPending = false;
 
			DoWorkEventArgs args = new DoWorkEventArgs(argument);
			DoWork.BeginInvoke(this, args, new AsyncCallback(ReportCompletion), args);
		}
 
		#endregion Methods
 
		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public bool WorkerSupportsCancellation
		{
			get { lock(this) return _isCancellationSupported; } 
			set { lock(this) _isCancellationSupported = value; } 
		}
		/// <summary>
		/// 
		/// </summary>
		public bool WorkerReportsProgress
		{
			get { lock(this) return _isProgressReported; }
			set { lock(this) _isProgressReported = value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public bool CancellationPending
		{
			get { lock(this) return _isCancelPending; }
		}
 
		#endregion Properties
 
		#region Fields
		/// <summary>
		/// 
		/// </summary>
		bool                        _isCancelPending = false;
		/// <summary>
		/// 
		/// </summary>
		bool                        _isProgressReported = false;
		/// <summary>
		/// 
		/// </summary>
		bool                        _isCancellationSupported = false;
 
		#endregion Fields
	}
 
#endif
} 
