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
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Resources;
using System.Reflection;
using log4net;

namespace ZabbixAgent
{
	/// <summary>
	/// Summary description for Tunneler.
	/// </summary>
	public class Tunneler
	{
		private static readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.tunneler");

		private System.Diagnostics.Process p1;
		private System.Diagnostics.Process p2;

		private bool closeConnections = false;

		private Configuration c = Configuration.getInstance;

		private string arguments = "";

		private void StartZabbixSecure() 
		{
			bool create = true;
			if (p1 != null) 
			{
				if (p1.HasExited == true) 
					create = true;
				else
					create = false;
			}

			if (create && Convert.ToBoolean(c.GetConfigurationByString("SSHUse", "SSH")))
			{
				// Start Zabbix
				p1 = new Process();
				p1.StartInfo.UseShellExecute = false;
				p1.StartInfo.RedirectStandardInput = true;
				p1.StartInfo.RedirectStandardOutput = true;
				p1.EnableRaisingEvents = true;
				p1.StartInfo.Arguments = arguments;
				//p1.StartInfo.Arguments = "-l " + c.getSSHUser() + " -L " + c.getTunnelZabbixLocalPort() +":localhost:" + c.GetTunnelZabbixRemotePort() +" -x -a -T -C -i " + c.getSSHKey() + " -N " + c.getTunnelZabbixServer();
				p1.StartInfo.FileName = Environment.GetEnvironmentVariable("SystemRoot") + "\\plink.exe";
				//Console.WriteLine(this.zabbixprocess.StartInfo.Arguments);
				p1.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				p1.Exited += new EventHandler(p1_Exited);
				p1.Start();
				log.Debug("Started secure connection");
			}
		}

		private void StartLog4jSecure() 
		{
			bool create = true;
			if (p2 != null) 
			{
				if (p2.HasExited == true) 
					create = true;
				else
					create = false;
			}

			if (create) {
				// Start Log4j
				p2 = new Process();
				p2.StartInfo.UseShellExecute = false;
				p2.StartInfo.RedirectStandardInput = true;
				p2.StartInfo.RedirectStandardOutput = true;
				p2.EnableRaisingEvents = true;
				p2.StartInfo.Arguments = "-l " + c.GetConfigurationByString("SSHUser", "SSH") + " -L " + c.GetConfigurationByString("LocalBoundPort", "SSH") +":localhost:" + c.GetConfigurationByString("ServerPort") +" -x -a -T -C -i " + c.GetConfigurationByString("SSHPrivateKeyPath", "SSH") + " -N " + c.GetConfigurationByString("ServerHost");
				p2.StartInfo.FileName = Environment.GetEnvironmentVariable("SystemRoot") + "\\plink.exe";		
				p2.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				p2.Exited += new EventHandler(p2_Exited);
				p2.Start();
				log.Debug("Started log4j secure connection");
			}
		}

		private void StopZabbixSecure() 
		{
			if (Convert.ToBoolean(c.GetConfigurationByString("SSHUse", "SSH")))
			{
				if (p1 != null) 
				{
					if (!p1.HasExited)
						p1.Kill();
					p1.Close();
				}
			} 
		}

		private void StopLog4jSecure() 
		{
			if (p2 != null) 
			{
				if (!p2.HasExited)
					p2.Kill();
				p2.Close();
			}
		}

		private void SavePlinkToDisk() 
		{
			try 
			{
				Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixAgent.SubSystem.plink.exe");

				if (stream != null) 
				{
					FileStream write = new FileStream(Environment.GetEnvironmentVariable("SystemRoot") + "\\plink.exe",FileMode.Create);

					int Length = 256;
					Byte [] buffer = new Byte[Length];


					int bytesRead = stream.Read(buffer,0,Length);
					// write the required bytes
					while( bytesRead > 0 )
					{
						write.Write(buffer,0,bytesRead);
						bytesRead = stream.Read(buffer,0,Length);
					}
					stream.Close();
					write.Close();
				}
			} 
			catch (IOException ex)
			{
				log.Error("Didnt install new version of plink.exe because it was in use." + ex.Message + ex.StackTrace);
			}
		}

		private void p1_Exited(object sender, System.EventArgs e)
		{
			if (!closeConnections) 
			{
				if (p1.HasExited) 
				{
					StartZabbixSecure();
					log.Error("Restarting secure connection for Agent");
					
				}
			}
		}
		
		private void p2_Exited(object sender, System.EventArgs e) 
		{
			if (!closeConnections) 
			{
				if (p2.HasExited) 
				{
					StartLog4jSecure();
					log.Error("Restarting secure connection for Log4j");
					Thread.Sleep(5000);
				}
			}
		}
		
		public void Start() 
		{
			closeConnections = false;
			if (!File.Exists(Environment.GetEnvironmentVariable("SystemRoot") + "\\plink.exe"))
				SavePlinkToDisk();

			log.Info("Systemroot for files: " + Environment.GetEnvironmentVariable("SystemRoot"));

			// Generate argumentstring

			if (Convert.ToBoolean(c.GetConfigurationByString("SSHUse", "SSH")))
			{
				// tunnels strings
				string log4j = "";
				string zabbix = "";

				/*if (c.getTunnelLog4jSSH()) 
					log4j = " -L " + c.getTunnelLog4jLocalPort() +":localhost:" + c.GetTunnelLog4jRemotePort();
				*/
				zabbix = " -L " + c.GetConfigurationByString("LocalBoundPort", "SSH") +":localhost:" + c.GetConfigurationByString("ServerPort");
	
				arguments = "-l " + c.GetConfigurationByString("SSHUser", "SSH") + zabbix + " -x -a -T -C -i " + c.GetConfigurationByString("SSHPrivateKeyPath", "SSH") + " -N " + c.GetConfigurationByString("ServerHost");

				log.Debug("Arguments for plink: " + arguments);
				StartZabbixSecure();
			}



			//StartLog4jSecure();
		}

		public void Stop()
		{
			closeConnections = true;
			StopZabbixSecure();
			//StopLog4jSecure();
		}
	}
}
