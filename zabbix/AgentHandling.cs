using System;
using System.Threading;
using System.Text;
using log4net;
using log4net.Config;
using System.Resources;
using System.Reflection;
using ZabbixCommon;

namespace ZabbixAgent
{
	/// <summary>
	/// Summary description for Startup.
	/// </summary>
	public class AgentHandling : IAgentHandling
	{
		private static readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent");

		public AgentHandling()
		{

		}

		public void Start() 
		{
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
			}
		}

		public void Stop() 
		{

		}
	}
}
