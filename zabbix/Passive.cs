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
using System.Net.Sockets;
using log4net;
using ZabbixCommon;

namespace ZabbixCore
{
	/// <summary>
	/// Summary description for Passive.
	/// </summary>
	public class Passive
	{

		private static readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.passive");
		
		private Connection c = Connection.getInstance;
		private Configuration conf = Configuration.getInstance;

		private Socket mainSocket = null;
		private Socket[] workerSocket = null;

		// Default values
		private int numberOfWorkerSockets = 4;
		private int passivePort = 10050;
		
		// Running values
		private int clientCount = 0;
		private AsyncCallback workerCallBack ;
	
		private WorkPool wp = WorkPool.getInstance;
		// Control bytes
		private byte stopControlByte = 10;

		// Worker information
		private class SocketPacket
		{
			public System.Net.Sockets.Socket currentSocket;
			public byte[] dataBuffer = new byte[256];
		}


		public Passive()
		{
			// Number of passive sockets
			try 
			{
				numberOfWorkerSockets = Int32.Parse(conf.GetConfigurationByString("WorkerSockets", "General"));
			} 
			catch (Exception) {}
			// Passiveport
			try 
			{
				numberOfWorkerSockets = Int32.Parse(conf.GetConfigurationByString("AgentPort", "General"));
			} 
			catch (Exception) {}

			workerSocket = new Socket[numberOfWorkerSockets];

			// Fetch workpool
			setupConnection();
		}


		private void setupConnection() 
		{
			try 
			{
				//Listen on all interfaces. Should this be customized?
				IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, passivePort);
				log.Debug("Made endpoint");
				mainSocket = c.GetPassiveSocket();
				log.Debug("Binded endpoint");
				mainSocket.Bind(ipLocal);
			
				// Naive calculation
				log.Debug("Listens on port: " + passivePort);
				mainSocket.Listen(numberOfWorkerSockets);

				mainSocket.BeginAccept(new AsyncCallback (OnClientConnect), null);
			} 
			catch (SocketException ex) 
			{
				log.Error("setupConnection:" + ex.Message);
			}
		}

		private void OnClientConnect(IAsyncResult a)
		{
			try 
			{
				// Returnere reference til indkommende socket.
				workerSocket[clientCount] = mainSocket.EndAccept(a);

				// Receive data
				WaitForData(workerSocket[clientCount]);

				++clientCount;
				log.Debug("Client #" + clientCount + " connected");

				mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);
			} 
			catch (ObjectDisposedException) 
			{
				log.Error("OnClientConnect: Socket has been closed");
			}
			catch (SocketException ex) 
			{
				log.Error("OnClientConnect: " + ex.Message );
			}
		}

		private void WaitForData(Socket s) 
		{
			try 
			{
				if (workerCallBack == null)
					workerCallBack = new AsyncCallback(OnDataReceived);

				SocketPacket sp = new SocketPacket();
				sp.currentSocket = s;
				s.BeginReceive(sp.dataBuffer, 0, sp.dataBuffer.Length, SocketFlags.None, workerCallBack, sp);
			} 
			catch (SocketException ex)
			{
				log.Error("WaitForData: " + ex.Message);
			} 
		}

		// If data received over 255 bytes the message will be broken. 
		private void OnDataReceived(IAsyncResult a)
		{
			try 
			{
				SocketPacket sData = (SocketPacket)a.AsyncState;

				int iRx = 0;

				iRx = sData.currentSocket.EndReceive(a);
				char[] chars = new char[iRx +  1];

				
				System.Text.Decoder d = System.Text.Encoding.ASCII.GetDecoder();
				int charLen = d.GetChars(sData.dataBuffer, 0, iRx, chars, 0);
				
				Byte[] b = System.Text.Encoding.ASCII.GetBytes(chars);
				
				
				// If buffer have LF in it. Stop and save the string, else continue.
				int endOfBuffer;
				for (endOfBuffer = 0;endOfBuffer<charLen;endOfBuffer++) 
				{

					if (b.GetValue(endOfBuffer).Equals(stopControlByte))
						break;
				}
				
				String recvString = new System.String(chars).Substring(0, endOfBuffer);
				

				log.Debug("Received string: \"" + recvString + "\"");
				log.Debug("Return string: \"" + wp.FetchCounterValue(recvString) + "\"");

				Byte[] send = System.Text.Encoding.ASCII.GetBytes(wp.FetchCounterValue(recvString));
				sData.currentSocket.Send(send);
				
				// Always expect only to get one string. 
				//	( The implementation from Zabbix is totally broken and now it's broken here too. )
				// If expanding implementation, call WaitForData(sData.currentSocket) as the last line.
				sData.currentSocket.Close();
			} 
			catch (ObjectDisposedException) 
			{
				log.Debug("OnDataReceived: Socket has been closed."); 
			}
			catch ( SocketException ex) 
			{
				log.Error(ex.Message);
			}
		}
	}
}
