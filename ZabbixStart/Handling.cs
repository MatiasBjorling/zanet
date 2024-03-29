 /*
 * This file is part of ZabbixAgent.NET
 * 
 * ZabbixAgent.NET is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.

 * ZabbixAgent.NET is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with ZabbixAgent.NET; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 * 
 * Copyright ZabbixAgent.NET a/s
 *
 * Used Trademarks are owned by their respective owners, There in ZABBIX SIA and Zabbix.
 */
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
		private static Thread updateThread = null;

		private static IAgentHandling ah = null;
		private static UpdateAgent ua = null;

		private static string version = null;

		private static Configuration conf = Configuration.getInstance;

		private Handling()
		{

		}

		public static void Start() 
		{

#if (DEBUG)
			XmlConfigurator.Configure(Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixStart.Logging.log_debug.xml"));
#else
			//XmlConfigurator.Configure(Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixStart.Logging.log_debug.xml"));
			XmlConfigurator.Configure(Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixStart.Logging.log.xml"));
#endif	
			ThreadStart tjob = new ThreadStart(ThreadOutput);
			agentThread = new Thread(tjob);
			agentThread.Name = "Agent Core Thread";
			agentThread.Start();
			

			// For updating the agent.
			if (Convert.ToBoolean(conf.GetConfigurationByString("EnableUpdateService", "Updater"))) 
			{
				log.Info("Starting monitoring of updates");
				updateThread = new Thread(new ThreadStart(MonitoringUpdate));
				updateThread.Name = "UpdateThread";
			}
		}

		public static void Stop() 
		{
			if (agentThread != null)
				agentThread.Abort();
			if (updateThread != null)
				updateThread.Abort();
		}

		// Agent thread
		protected static void ThreadOutput() 
		{
			try 
			{
				try 
				{
					
					bool canLoadUntrustedCode = false;
					try 
					{
						canLoadUntrustedCode = Convert.ToBoolean(conf.GetConfigurationByString("CanLoadUntrustedCode", "General"));
					} 
					catch {}

					// Loading assembly
					if (agentAssembly == null)
						if (canLoadUntrustedCode)
						{
							agentAssembly = Assembly.Load("ZabbixCore, Culture=neutral");
							log.Info("Can load untrusted code. Use only for development!");
						}
						else 
						{
							agentAssembly = Assembly.Load("ZabbixCore, Culture=neutral, PublicKeyToken=a7296e6a43eb88e1");
						}
				} 
				catch (System.IO.FileLoadException ex ) 
				{
					log.Error("Could not find assembly: " + ex.Message);
				}
				
				if (agentAssembly != null) 
				{
					// Find the startup class.
					Type[] asmTypes = agentAssembly.GetTypes();
					foreach (Type t in asmTypes) 
					{
						if (t.IsClass && t.Name.Equals("AgentHandling"))
						{
							ah = (IAgentHandling) Activator.CreateInstance(t);
						} 
						else if (t.IsClass && t.Name.Equals("VersionCounter")) 
						{
							ILoadableCounter agentversion = (ILoadableCounter) Activator.CreateInstance(t);
							version = agentversion.getValue();
							
						}
					}

					// Only a weak security check.
					if (ah == null)
						log.Error("SECURITY BREACH! Tried to load a signed DLL which was not distributed from ZabbixAgent.NET... Exiting");
					else
					{
						if (updateThread != null)
							updateThread.Start();
						ah.Start();
					}
				} 
				else 
				{
					log.Error("Cannot load a proper ZabbixAgent assembly, have you enabled CanLoadUntrustedCode?... Exiting");
				}

			} 
			catch (Exception ex) 
			{
				Console.WriteLine(ex.Message);
				log.Error("Cannot start agent: " + ex.Message + ex.StackTrace);
			}

		}

		// Monitoring update service.
		private static void MonitoringUpdate()
		{
			// If version is null, then blow to pieces.
			if (version != null) 
			{
				ua = new UpdateAgent(ah, version, agentAssembly);
				ua.PullLoopUpdateServer();
			} 
		}
	}
}
