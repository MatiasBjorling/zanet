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
 * Copyright TMCare a/s
 *
 * Used Trademarks are owned by their respective owners, There in ZABBIX SIA and Zabbix.
 */

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using log4net;

namespace ZabbixAgent
{
	/// <summary>
	/// Active communication between server and agent implementation.
	/// </summary>
	public class Active
	{
		private WorkPool wp = new WorkPool();
		private Connection c = Connection.getInstance;
		private Configuration conf = Configuration.getInstance;

		private bool stop = false;

		private static readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.active");

		public Active()	{}

		public void get_active_checks() 
		{
			log.Info("Starting agent [" + (new Counters.VersionCounter()).getValue() + "]");
			log.Info("Application start path: " + Application.StartupPath);
#if (DEBUG)
log.Info("Started in DEBUGGING mode");
#endif
			// Host
			//log.Debug("ZBX_GET_ACTIVE_CHECKS\n" + System.Net.Dns.GetHostByName("LocalHost").HostName);
			String askForActiveClients = "ZBX_GET_ACTIVE_CHECKS\n" + System.Net.Dns.GetHostByName("LocalHost").HostName + "\n";
			
			int refreshTime = 15;
			try 
			{
				refreshTime = Int32.Parse(conf.GetConfigurationByString("ActiveChecks", "General")); 
			} 
			catch {};

			try 
			{
				while (!stop) 
				{
					string response = "";
					try 
					{
						//log.Info("Asking server for Active counters...");
						response = Connection.getInstance.SendTo(askForActiveClients);
					} 
					catch (Exception ex ) 
					{
						log.Info(ex.Message);
						Thread.Sleep(refreshTime);
					}

					string[] lines = response.Split('\n');

					if (!response.Equals("")) 
					{
						foreach (string line in lines) 
						{
							string[] keys = line.Split(':');
							string key = keys[0];
							for (int i=1;i<(keys.Length-2);i++) 
							{
								key += ":" + keys[i];
							}

							if (keys.Length > 2) 
							{
								if (!stop)
									wp.addJob(key, Int32.Parse(keys[keys.Length-2]), Int32.Parse(keys[keys.Length-1]));
							}
						}
						wp.increaseCounter();
					}
					for (int i=0;i<refreshTime;i++) 
					{
						if (!stop)
							Thread.Sleep(1000);
					}
				}
			} 
			catch (Exception ex) 
			{
				log.Debug(ex.Message + ex.StackTrace);
			}
		}

		public void Stop() 
		{
			stop = true;

			// Possible racecondition if there is added a job when aborting thread list.
			wp.StopAllThreads();
			
			if (Convert.ToBoolean(conf.GetConfigurationByString("SSHUse", "SSH")))
			{
				if (c != null) 
					c.CloseSecureConnections();
			}
			log.Debug("Closed active polling");
		}
	}
}
