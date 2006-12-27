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
using System.Xml;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using ZabbixCommon;


namespace ZabbixCounters.Counters
{
	/// <summary>
	/// Summary description for RemoteUpdate.
	/// </summary>
	public class RemoteUpdate : ILoadableCounter
	{
		string updateServiceLnk = "";
		
		ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.remoteupdate");

		public RemoteUpdate() {}

		public RemoteUpdate(string updateServiceLnk)
		{
				this.updateServiceLnk = updateServiceLnk;
		}

		Configuration conf = Configuration.getInstance;

		// Check for new server version and new client verion, update if not equal
		public string getValue() 
		{
			string returnval = "Error";
			try 
			{
				if (conf.GetConfigurationByString("UpdateService","Updater").Length == 0) 
				{
					UpdateService us = new UpdateService();
#if (DEBUG)
					log.Debug("Connection with: " + conf.GetConfigurationByString("CustomerGroup", "Updater") + " Servername: " + System.Net.Dns.GetHostByName("LocalHost").HostName + " Version: " + new VersionCounter().getBuildValue().ToString());
#endif		
					// Get update information from update service.	
					String response = us.getNewVersionLink(conf.GetConfigurationByString("CustomerGroup", "Updater"), System.Net.Dns.GetHostByName("LocalHost").HostName, new VersionCounter().getBuildValue().ToString());
				
					// Lock for updates to only coming from specified link. 
					// FIXME: Will be used until better security method is made. Strongly signing of DLLs maybe?

					if (response.StartsWith(updateServiceLnk)) 
					{
						log.Info("Receiving new version from " + response);
						returnval = UpdateClient(response);
					} 
					else 
					{
						returnval = response;
					}
				}
			} 
			catch (Exception ex) 
			{
#if (DEBUG)
					log.Error(ex.Message + ex.StackTrace);
#else
					log.Error(ex.Message);
#endif
			}

			return returnval;
		}


		private string UpdateClient(string downloadlnk) 
		{
			// TODO: Figure how to handle a proper update...
			try 
			{
				// Save the file
				WebClient client = new System.Net.WebClient();
				Byte[] data  = client.DownloadData(downloadlnk);
				
				FileStream fw = new FileStream (Application.StartupPath + "\\ZabbixAgent_new.exe",FileMode.Create);
				fw.Write(data, 0, data.Length);
				fw.Close();

				// Get update file
				SaveUpdateToDisk();

				// Start update
				Process p = new Process();
				p.StartInfo.FileName = Application.StartupPath + "\\remoteupdate.bat";
				p.Start();
				log.Info("Updating client to new version from " + downloadlnk);
			} 
			catch (Exception ex) 
			{
				log.Error(ex.Message + ex.StackTrace);
				return ex.Message;
			}
			return "Updating... (Agent will be restarted)";
		}

		private void SaveUpdateToDisk() 
		{
			try 
			{
				Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixAgent.SubSystem.update.bat");

				if (stream != null) 
				{
					//Stream s = new FileStream(Application.StartupPath + "\\remotemonitorupdate.bat",FileMode.Create);
					
					StreamReader sr = new StreamReader(stream);
					string remotefile = sr.ReadToEnd();
					sr.Close();

					remotefile = remotefile.Replace("%Apppath%", Application.StartupPath);
					StreamWriter sw = new StreamWriter(Application.StartupPath + "\\remoteupdate.bat",false);

					sw.Write(remotefile);
					sw.Close();
				}
			} 
			catch (IOException ex)
			{
				log.Error("Didnt install new version of update.bat because it was in use." + ex.Message + ex.StackTrace);
			}
		}

		/// <summary>
		/// Check if key match counter.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool isType(string key) 
		{ 
			// Remote update
			if (key.StartsWith("agent.update") && Convert.ToBoolean(conf.GetConfigurationByString("EnableUpdateService", "Updater"))) 
				return true;
			else 
				return false;
		}

		/// <summary>
		/// Set the key for the counter.
		/// </summary>
		/// <param name="key"></param>
		public ILoadableCounter getCounter(string key)
		{ 
			return new RemoteUpdate(key.Remove(0,key.IndexOf("[",0, key.Length)).Trim('[').Trim(']'));
		}
	}


}
