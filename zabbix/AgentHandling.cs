using System;
using System.Threading;
using System.Text;
using log4net;
using log4net.Config;
using System.Resources;
using System.Reflection;

namespace ZabbixAgent
{
	/// <summary>
	/// Summary description for Startup.
	/// </summary>
	public class AgentHandling
	{
		private static readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent");

		private AgentHandling()
		{

		}

		public static void Start() 
		{
#if (DEBUG)
			
			foreach (string tmp in Assembly.GetCallingAssembly().GetManifestResourceNames())
			{
				Console.WriteLine(tmp);
			}
			

			XmlConfigurator.Configure(Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixAgent.SubSystem.log_debug.xml"));
#else
			XmlConfigurator.Configure(Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixAgent.SubSystem.log.xml"));
#endif		
			//log.Info("Starting agent [" + (new Counters.VersionCounter()).getValue() + "]");
			
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
			}
		}
	}
}
