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

		private bool closeConnections = false;

		private string arguments = "";

		private bool useSSH = false;
		private bool usePrivateKey = false;
		
		private int localport = 0;
		private int serverport = 0;
		
		private string server = "";
		private string user = "";
		private string password = "";
		private string keypath = "";
		
		public Tunneler()		
		{	
			// Retrieve config 
			Configuration c = Configuration.getInstance;
			
			try {
				useSSH = Convert.ToBoolean(c.GetConfigurationByString("Use", "SSH"));
			} catch (Exception ex) {
				log.Error("Not using SSH. Error: " + ex.Message);
			} 

			if (useSSH) 
			{
				try 
				{
					// Required
					usePrivateKey = Convert.ToBoolean(c.GetConfigurationByString("UsePrivateKey", "SSH"));
					server = c.GetConfigurationByString("Server", "SSH");
					serverport = Convert.ToInt32(c.GetConfigurationByString("ServerPort", "SSH"));
					localport = Convert.ToInt32(c.GetConfigurationByString("LocalPort", "SSH"));
					user = c.GetConfigurationByString("User", "SSH");
				} 
				catch (Exception ex) 
				{
					log.Error("Not using SSH. Error: " + ex.Message);
					useSSH = false;
				}
			}

			try 
			{
				if (usePrivateKey)
				{
					keypath = c.GetConfigurationByString("KeyPath", "SSH");
				} 
				else 
				{
					password = c.GetConfigurationByString("Password", "SSH");
				}
				
			} 
			catch (Exception ex) 
			{
				log.Error("Not using SSH. Error: " + ex.Message);
				useSSH = false;
			}

			
		}

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

			if (create && useSSH)
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
				log.Debug("Started SSH connection");
			}
		}


		private void StopZabbixSecure() 
		{
			if (p1 != null && useSSH) 
			{
				if (!p1.HasExited)
					p1.Kill();
				p1.Close();
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
		
		
		public void Start() 
		{
			closeConnections = false;
			if (!File.Exists(Environment.GetEnvironmentVariable("SystemRoot") + "\\plink.exe"))
				SavePlinkToDisk();

			// Generate argumentstring

			if (useSSH)
			{
				// tunnels strings
				string zabbix = "";

				zabbix = " -L " + localport +":localhost:" + serverport;
	
				if (usePrivateKey)
					arguments = "-l " + user + zabbix + " -x -a -T -C -i " + keypath + " -N " + server;
				else
					arguments = "-l " + user + zabbix + " -x -a -T -C -pw " + password + " -N " + server;

				log.Debug("Arguments for plink: " + arguments);
				StartZabbixSecure();
			}
		}

		public void Stop()
		{
			closeConnections = true;
			StopZabbixSecure();
		}
	}
}
