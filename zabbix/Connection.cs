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
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using log4net;

namespace ZabbixAgent
{
	/// <summary>
	/// Connection singleton. 
	/// </summary>
	public class Connection
	{
		// Logging
		public ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.connection");

		static Connection instance = null;

		// Instance lock
		static readonly object uselock =  new object();
		// Connection lock
		private readonly object connlock = new object();

		Configuration conf = Configuration.getInstance;

		// Connection
		private IPHostEntry hostEntry = null;
		private Socket socket = null;
		private string hostName = System.Net.Dns.GetHostByName("localhost").HostName;
		
		// Secure tunnel 
		private Tunneler tunnel = new Tunneler();
		private bool useSSH = false;
		private Connection() {
			try 
			{
				try 
				{
					useSSH = Convert.ToBoolean(conf.GetConfigurationByString("Use", "SSH"));
				}
				catch {}
				
				if (useSSH)
				{
					tunnel.Start();
					// Wait for tunnel to start up.
					Thread.Sleep(2000);
				}
			} 
			catch (Exception ex) 
			{
				log.Error(ex.Message);
				// Some sleep time if some error happend. Prevents going into a infinite loop which hook all the cpu 
				// resources.
				Thread.Sleep(5000);
			}
		}

		// Singleton
		public static Connection getInstance
		{
			get 
			{
				lock(uselock) 
				{
					if (instance == null) 
						instance = new Connection();
					return instance;
				}
			}
		}

		private Socket GetSocket() 
		{
			Socket s = null;
			Int32 serverport = 0;
			// Get IP for DNS
			if (useSSH)
			{
				hostEntry = Dns.GetHostByName("localhost");
				serverport = Int32.Parse(conf.GetConfigurationByString("LocalBoundPort", "SSH"));
			} 
			else 
			{
				hostEntry = Dns.GetHostByName(conf.GetConfigurationByString("ServerHost"));
				serverport = Int32.Parse(conf.GetConfigurationByString("ServerPort"));
			}

			foreach(IPAddress address in hostEntry.AddressList) 
			{
				IPEndPoint ipe = new IPEndPoint(address, serverport);
				
				//log.Debug("Tryint connection to: " +  address + ":" + serverport);
				Socket tmpSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

				try 
				{
					tmpSocket.Connect(ipe);
					if (tmpSocket.Connected) 
					{
						s = tmpSocket;
						break; 
					} 
					else 
						continue;
				} 
				catch (Exception ex)
				{
					Thread.Sleep(5000);
					log.Error("Could not connect to server. Check the secure tunnel and the zabbix server. Error: " + ex.Message);
				}
			}
			return s;
		}

		private void SetSocket(Socket s)
		{
			lock (connlock) 
				socket = s;
		}

		public string PushCounter(string key, string value) 
		{
			return SendTo("<req><host>" + Convert.ToBase64String(Encoding.UTF8.GetBytes(hostName)) + "</host><key>" + Convert.ToBase64String(Encoding.UTF8.GetBytes(key)) + "</key><data>" + Convert.ToBase64String(Encoding.UTF8.GetBytes(value)) + "</data></req>");		
		}

		public string SendTo(string message) 
		{
			Byte[] recvbytes = new Byte[256];
			string response = "";
			lock (connlock) 
			{
				SetSocket(GetSocket());
				
				if (socket != null && socket.Connected) 
				{
					byte[] data = Encoding.UTF8.GetBytes(message + "\n");
					
					socket.Send(data,data.Length,0);
					int bytes = 0;					
					do 
					{
						bytes = socket.Receive(recvbytes, recvbytes.Length, 0);
						response += Encoding.ASCII.GetString(recvbytes,0, bytes);
					} while (bytes > 0); 

					// Close and cleanup connection. 
					socket.Close();
				} 				
			}
			return response;			
		}

		public void CloseSecureConnections() 
		{
			tunnel.Stop();
		}
	}
}
