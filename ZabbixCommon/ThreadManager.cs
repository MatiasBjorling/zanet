using System;
using System.Threading;
using log4net;

namespace ZabbixCommon
{
	/// <summary>
	/// Summary description for ThreadManager.
	/// </summary>
	public class ThreadManager
	{
		private static readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.threadmanager");
		
		public ThreadManager()
		{
		}

		public static Thread GetThread(string name, ThreadStart threadStart) 
		{
			Thread t = new Thread(threadStart);
			t.Name = name;
			log.Debug("Starting thead: " + name);
			return t;
		}
	}
}
