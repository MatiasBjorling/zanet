using System;
using ZabbixCommon;

namespace ZabbixCore
{
	/// <summary>
	/// Summary description for Startup.
	/// </summary>
	public class AgentHandling : IAgentHandling
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent");

		ZabbixCore.Active ac = null;
		ZabbixCore.Passive ap = null;

		bool running = true;

		public AgentHandling()
		{

		}

		public void Start() 
		{
			running = true;
			log.Info("Starting agent [" + (new Counters.VersionCounter()).getValue() + "]");
			
			while (running) 
			{
				try 
				{
					ac = new ZabbixCore.Active();
					ap = new ZabbixCore.Passive();
					ac.get_active_checks();
					System.Threading.Thread.Sleep(10000);
				} 
				catch (Exception ex)
				{
					log.Error(ex.Message + ex.StackTrace);
				}
			}
		}

		public void Stop() 
		{
			running = false;
			if (ac != null)
				ac.Stop();
		}
	}
}
