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
using System.Net;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using ZabbixCommon;

namespace ZabbixStart
{
	/// <summary>
	/// Summary description for UpdateAgent.
	/// </summary>
	public class UpdateAgent
	{
		Configuration conf = Configuration.getInstance;
		IAgentHandling ah = null;
		Assembly agent = null;

		string version = "";
		string hostname = System.Net.Dns.GetHostByName("LocalHost").HostName;
		string customerGroup = "";

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixstart.update");

		public UpdateAgent(IAgentHandling ah, string version, Assembly agent)
		{
			if (ah == null)
				log.Debug("argh");
			this.ah = ah;
			this.agent = agent;
			
			// Strips build number ( This should just be fetched from Version, but i don't have a instance of it, only 
			// ILoadableCounber which don't know about the extended methods. Limits the build number to < 1000
			this.version = version.Substring(version.Length-3, 3).Trim();
			customerGroup = conf.GetConfigurationByString("CustomerGroup", "Updater");
		}

		public void PullLoopUpdateServer()
		{
			version = version;
			// Get update information from update service.	
			while (true)
			{
				log.Debug("Checking version");
				try 
				{
					// Simple check
					if (Configuration.GetObjectInHashByKey("UpdatePackageLink") != null && Configuration.GetObjectInHashByKey("UpdatePackageLink") != null ) 
					{
						string dlink = (string)Configuration.GetObjectInHashByKey("UpdatePackageLink");
						
						// Simple check
						if (dlink.StartsWith("http://")) 
						{
							// Get version
							string webversion = dlink.Substring(dlink.IndexOf("|", 0, dlink.Length) +1, dlink.Length - dlink.IndexOf("|", 0, dlink.Length)-1);
							
							// Update if version is unequal
							if (!webversion.Equals(version)) 
							{
								log.Info("Version from Zabbix: " + Configuration.GetObjectInHashByKey("UpdatePackageLink"));
								log.Info("Client build version: " + version);
								log.Info("Zabbix version: " + webversion);
								log.Info("Zabbix download link: " + dlink.Substring(0, dlink.IndexOf("|", 0, dlink.Length)));

								if (GetNewAssemblies(dlink.Substring(0, dlink.IndexOf("|", 0, dlink.Length))) == 1)
									InstallNewAssemblies();
							}
						}
					}
				} 
				catch (Exception ex)
				{	
					log.Error("Update error: " + ex.Message);
				}
				System.Threading.Thread.Sleep(60000);
			}
		}

		private string GetMD5HashFromBytes(Byte[] buf) 
		{
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			// Converts from Byte code to Hex and lowercase's it before returning. 
			return BitConverter.ToString(md5.ComputeHash(buf)).Replace("-", "").ToLower();	
		}

		private int GetNewAssemblies(string uri) 
		{
			// Save the file
			WebClient client = new System.Net.WebClient();
			Byte[] data  = client.DownloadData(uri);
			client.Dispose();

			// Checking that the MD5 received from Zabbix is the same as the downloaded bytes
			if (GetMD5HashFromBytes(data).Equals(Configuration.GetObjectInHashByKey("UpdatePackageMD5Sum")))
			{
				log.Debug("MD5 sum of downloaded file: " + GetMD5HashFromBytes(data));
				FileStream fw = new FileStream (System.Windows.Forms.Application.StartupPath + "\\_NewAgentVersion.exe",FileMode.Create);
				fw.Write(data, 0, data.Length);
				fw.Close();
				return 1;
			} 
			else 
			{
				log.Error("MD5 of downloaded content does not match the one from counter. Not updating");
				return 0;
			}
			
		}

		private void InstallNewAssemblies() 
		{
			// Stopping agent :o) ( The Stop routine shall handle the quitting of all the threads )
			ah.Stop();

			// Unloading agent
			SaveUpdateToDisk();

			// Start update
			Process p = new Process();
			p.StartInfo.FileName = Application.StartupPath + "\\update.bat";
			p.Start();
		}

		private void SaveUpdateToDisk()
		{
			try
			{
				Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixStart.update.bat");

				if (stream != null) 
				{
					StreamReader sr = new StreamReader(stream);
					string remotefile = sr.ReadToEnd();
					sr.Close();

					remotefile = remotefile.Replace("%Apppath%", Application.StartupPath);
					StreamWriter sw = new StreamWriter(Application.StartupPath + "\\update.bat",false);

					sw.Write(remotefile);
					sw.Close();
				}
			} 
			catch (IOException ex)
			{
				log.Error("Didnt install new version of update.bat because it was in use." + ex.Message + ex.StackTrace);
			}
		}
	}
}
