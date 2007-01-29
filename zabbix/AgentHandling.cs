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
			Configuration conf = Configuration.getInstance;

			bool usePassiveMode = false;
			bool useActiveMode = false;

			try 
			{
				usePassiveMode = Convert.ToBoolean(conf.GetConfigurationByString("UsePassiveMode", "Network"));
			}
			catch {}
			try 
			{
				useActiveMode = Convert.ToBoolean(conf.GetConfigurationByString("UseActiveMode", "Network"));
			}
			catch {}

			if (usePassiveMode)
			{
				log.Info("Activating passive mode");
				// Loads as a thread
				ap = new ZabbixCore.Passive();
			}

			while (running) 
			{
				try 
				{
					if (useActiveMode || (useActiveMode == false && usePassiveMode == false))
					{
						log.Info("Activating active mode");
						ac = new ZabbixCore.Active();
						ac.get_active_checks();
					} 
					System.Threading.Thread.Sleep(30000);
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
