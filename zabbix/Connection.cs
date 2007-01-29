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
using ZabbixCommon;
using log4net;

namespace ZabbixCore
{
	/// <summary>
	/// Connection singleton. 
	/// </summary>
	public class Connection
	{
		// Queue Object
		public class ConnQueueEventArgs : EventArgs
		{
			private string key;
			private string val;
			public ConnQueueEventArgs(string key, string val) 
			{
				this.key = key;
				this.val = val;
			}

			public string Key
			{
				get { return key; }
				set { this.key = value; }
			}

			public string Val
			{
				get { return val; }
				set { this.val = value; }
			}
		}

		// Queue
		Queue commandQueue = null;
		public delegate void NewCounterInQueueEventHandler(object sender, ConnQueueEventArgs e);
		public event NewCounterInQueueEventHandler NewCounterInQueue;

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
		//private static string hostName = System.Net.Dns.GetHostByName("localhost").HostName;
		private static string hostName = System.Net.Dns.GetHostByName("localhost").HostName;

		// ConnectionPart
		private static string connHostName = "<req><host>" + Convert.ToBase64String(Encoding.UTF8.GetBytes(hostName)) + "</host><key>";

		// Secure tunnel 
		private Tunneler tunnel = new Tunneler();
		private bool useSSH = false;

		// Configurations
		int queueLength = 65768; // Arbitrary ( ... 500.000 can be allocated for a few megs )

		private Connection() {
			
			// Setup queue
			NewCounterInQueue += new NewCounterInQueueEventHandler(this.sendCountersFromQueue);

			// Get static configuration
			try 
			{
				int qtmp = Convert.ToInt32(conf.GetConfigurationByString("QueueLength"));
				if (qtmp > 0)
					queueLength = qtmp;

				if(Convert.ToBoolean(conf.GetConfigurationByString("UseQueue"))) 
					commandQueue = new Queue(queueLength);
			} 
			catch {}
			finally 
			{
				if (commandQueue != null) 
					log.Info("Using queue version with " + queueLength + " as queue length initalized.");
				else 
					log.Info("Using non-queue version. Counter reports will not be queued");
			}

			try 
			{
				if (!Convert.ToBoolean(conf.GetConfigurationByString("FQDN", "General"))) 
				{
					connHostName = "<req><host>" + Convert.ToBase64String(Encoding.UTF8.GetBytes((string) conf.GetConfigurationByString("Hostname", "General"))) + "</host><key>"; ;
					log.Debug("Using hostname: " + conf.GetConfigurationByString("Hostname", "General"));
				}
			}
			catch {}

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

		// Event for new a counter have pushed some data.
		protected virtual void OnNewCounterInQueue(ConnQueueEventArgs e) {
			if (NewCounterInQueue != null) 
				NewCounterInQueue(this, e);
		}
		
		private Socket GetSocket() 
		{
			Socket s = null;
			Int32 serverport = 0;
			// Get IP for DNS
			if (useSSH)
			{
				hostEntry = Dns.GetHostByName("localhost");
				serverport = Int32.Parse(conf.GetConfigurationByString("LocalPort", "SSH"));
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

		public void PushCounter(string key, string value) 
		{
			OnNewCounterInQueue(new ConnQueueEventArgs(key, value));
			//log.Debug("Continuing...");
			//return SendTo("<req><host>" + Convert.ToBase64String(Encoding.UTF8.GetBytes(hostName)) + "</host><key>" + Convert.ToBase64String(Encoding.UTF8.GetBytes(key)) + "</key><data>" + Convert.ToBase64String(Encoding.UTF8.GetBytes(value)) + "</data></req>");		
		}

		public void sendCountersFromQueue(object sender, ConnQueueEventArgs e) 
		{
			//log.Debug("Responses in Counter Queue: " +  commandQueue.Count);
			//log.Debug("Key: " + e.Key + " Value: " +e.Val);

			if (commandQueue != null) 
			{
				// If not just a wakeupevent, but a counter which want to send some data, push to queue
				if (!e.Key.Equals("")) 
					Queue.Synchronized(commandQueue).Enqueue(e);
			
				while (commandQueue.Count != 0) 
				{
					ConnQueueEventArgs cqe = (ConnQueueEventArgs)Queue.Synchronized(commandQueue).Dequeue();
					if (cqe != null) 
					{
						try 
						{
						
							// Get unixtime
							string timestamp = ((long)(DateTime.Now.AddHours(-2).AddMinutes(-20).ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
							// connHostName is <reg><host>hostName</host><key>
							SendTo(connHostName + Convert.ToBase64String(Encoding.UTF8.GetBytes(cqe.Key)) + "</key><data>" + Convert.ToBase64String(Encoding.UTF8.GetBytes(cqe.Val)) + "</data><timestamp>"+ Convert.ToBase64String(Encoding.UTF8.GetBytes(timestamp)) +"</timestamp></req>");			
						} 
						catch (SocketException) 
						{
							// Requeue if broken.
							//log.Debug("Connection down...Resending");
							Queue.Synchronized(commandQueue).Enqueue(cqe);
							break;
						}
					}
				}
			} 
			else /* Just send if queue is not used */
			{
				SendTo(connHostName + Convert.ToBase64String(Encoding.UTF8.GetBytes(e.Key)) + "</key><data>" + Convert.ToBase64String(Encoding.UTF8.GetBytes(e.Val)) + "</data></req>");			
			}
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
			//log.Debug("Msg: " + message + " Res: \"" + response + "\"");
			return response;			
		}

		public void CloseSecureConnections() 
		{
			tunnel.Stop();
		}

		// Passive socket handling

		public Socket GetPassiveSocket() 
		{
			Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			return s;
		}
	}
}
