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
using ZabbixCommon;
using log4net;

namespace ZabbixCore
{
	/// <summary>
	/// WorkThread is the thread which execute a single counter.
	/// </summary>
	public class WorkThread
	{
		private static readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.workthread");

		int interval;
		int unknown;
		string key;
		ILoadableCounter counter;
		Connection c = Connection.getInstance;
		WorkJobAge wja;
		int averageInterval;


		public WorkThread()
		{

		}

		public void ThreadStart() 
		{
			// Read the parametres from workjob
			WorkJob wj = WorkJob.getJob;
			
			// Parametres
			interval	= wj.interval;
			unknown		= wj.unknown;
			key				= wj.key;
			counter		= wj.monitorobj;
			wja				= wj.wja;
			averageInterval = wj.averageInterval;

			// Done with the object and let workpool release the creation lock
			wj.readed = true;

		}

		public void PushData() 
		{
			Double total = 0;
			int times = 0;
			DateTime ts = new DateTime(DateTime.Now.Ticks).AddSeconds(averageInterval);

			//log.Debug("Starting Thread: " + Thread.CurrentThread.Name);
			while (true) 
			{
				if (wja.tooOld) 
				{
					log.Info("Exiting: " + Thread.CurrentThread.Name);
					break;
				}
	
				string msg = counter.getValue();
							
				
				// Aggregate data
				if (averageInterval > 0) 
				{
					if (ts > DateTime.Now) 
					{
						try 
						{
							total += Double.Parse(msg);
							times++;
							//log.Debug("Total: " + total + " Times: " + times);
						} 
						catch (Exception) 
						{
							log.Debug(Thread.CurrentThread.Name + "Could not read aggregate value: " + msg);
						}

					} 
					else 
					{

						Double average = total / times;
						c.PushCounter(Thread.CurrentThread.Name, average.ToString().Replace(',','.'));

						// Reset counters
						times = 0;
						total = 0;
						ts = new DateTime(DateTime.Now.Ticks).AddSeconds(60);
					}
					Thread.Sleep(1000);
				} 
				else 
				{ 
					// Single data
					try 
					{
						Int32.Parse(msg);
						msg += ".000000";
						
					} 
					catch (Exception) {};
				
					//log.Debug("Value: " +msg);
					c.PushCounter(Thread.CurrentThread.Name, msg);
					Thread.Sleep(interval*1000);
				}
				
				
			}
		}

		
	}
}
