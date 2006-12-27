using System;
using System.Threading;
using System.Text;
using log4net;
using log4net.Config;
using System.Resources;
using System.Reflection;
using ZabbixCommon;


namespace ZabbixStart
{
	/// <summary>
	/// Summary description for Startup.
	/// </summary>
	public class Handling
	{
		private static readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixstart");

		private static Assembly agentAssembly = null;

		private static Thread agentThread = null;

		private static IAgentHandling ah = null;

		private Handling()
		{

		}



		public static void Start() 
		{

#if (DEBUG)
			XmlConfigurator.Configure(Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixStart.Logging.log_debug.xml"));
#else
			XmlConfigurator.Configure(Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixStart.Logging.log.xml"));
#endif	
			ThreadStart tjob = new ThreadStart(ThreadOutput);
			agentThread = new Thread(tjob);
			agentThread.Start();
		}

		/*
		 * Agent thread
		 */
		protected static void ThreadOutput() 
		{
			
			try 
			{
				if (agentAssembly == null)
					agentAssembly = Assembly.Load("ZabbixAgent, Culture=neutral, PublicKeyToken=166854b3241b8293");

				
				// Find the startup class.
				Type[] asmTypes = agentAssembly.GetTypes();
				foreach (Type t in asmTypes) 
				{
					if (t.IsClass && t.Name.Equals("AgentHandling"))
					{
						ah = (IAgentHandling) Activator.CreateInstance(t);
					}
				}

				if (ah == null)
					log.Error("SECURITY BREACH!!!!!! Loaded a signed DLL which was not distributed from ZabbixAgent.NET");
				else
					ah.Start();
			} 
			catch (Exception ex) 
			{
				Console.WriteLine(ex.Message);
				log.Error("Cannot start agent: " + ex.Message);
			}

			



	/*
			log.Info("Starting agent [" + (new Counters.VersionCounter()).getValue() + "]");
			
			while (true) 
			{
				try 
				{
					ZabbixAgent.Active ac = new ZabbixAgent.Active();
					ZabbixAgent.Passive ap = new ZabbixAgent.Passive();
					ac.get_active_checks();
					Thread.Sleep(10000);
				} 
				catch (Exception ex)
				{
					log.Error(ex.Message + ex.StackTrace);
				}
			}*/
		}
	}
}
