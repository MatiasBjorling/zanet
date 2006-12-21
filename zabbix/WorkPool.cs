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
using System.Threading;
using System.Reflection;
using System.Collections;
using ZabbixAgent.Counters;
using log4net;

namespace ZabbixAgent
{
	/// <summary>
	/// Summary description for WorkPool.
	/// </summary>
	public class WorkPool
	{	
		private static readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.workpool");

		private Hashtable threads = new Hashtable();
		private readonly object lockobj = new object();
		private ArrayList aging = new ArrayList(20);
		private ArrayList counters = new ArrayList(20);
		private bool createNew = true;

		private long agingcount = 0;

		static WorkPool instance = null;
	
		// Instance lock
		static readonly object uselock =  new object();


		private WorkPool()
		{
			ArrayList tmp = ZabbixAgent.Util.ClassUtils.ScanILoadableCounter();
			foreach (Type t in tmp) 
			{
				ILoadableCounter c = (ILoadableCounter) Activator.CreateInstance(t);
				counters.Add(c);
			}
		}
		// Singleton
		public static WorkPool getInstance
		{
			get 
			{
				lock(uselock) 
				{
					if (instance == null) 
						instance = new WorkPool();
					return instance;
				}
			}
		}

		public void increaseCounter() 
		{
			agingcount += 1;
		}

		// Active host
		public void addJob(string key, int interval, int unknown) 
		{
			key = key.Trim();
			Thread obj = (Thread)threads[key];
			ILoadableCounter counterobj = null;

			/*
			 * Average Interval aggregates values ny seconds ( one per second ) and take average of them. 
			 * if Zero it is ignored and values will not be aggregated.
			 */
			int averageInteval = 0;

			if (obj == null) 
			{
				// Find if the counter match
				counterobj = FindCounterFromKey(key);

				if (counterobj != null) {
					// Make new thread with counter
					Thread t = new Thread(new ThreadStart(WorkPool.StartWorkedThread));
					
					WorkJob wj = WorkJob.getJob;
					lock (lockobj) 
					{
						// Aging of thread
						WorkJobAge wja = new WorkJobAge(key, agingcount);
					
						// Should the counter be calculated by average?

						string averagestring = key.Remove(0,key.IndexOf("],",0,key.Length)+1);
						try 
						{
							averageInteval = Int32.Parse(averagestring);
						} 
						catch (Exception ex) 
						{}

						// Add parameters to workjob
						wj.readed = false;
						wj.AddParams(key, interval, unknown, counterobj, wja, averageInteval);

						// Give thread key name
						t.Name = key;
						if (createNew)
							t.Start();

						// Wait for values to be read by thread.
						while (!wj.readed) 
						{
							Thread.Sleep(100);
						}

						threads[key] = t;

						// Add thread to aging list ( Can also be used for communication )
						aging.Add(wja);
					}
				}
			} 
			else 
			{
				// The key exist...Find it and make it older.
				foreach (WorkJobAge tmp in aging) 
				{
					if (tmp.key.Equals(key))
						tmp.age = agingcount;
				}
			}

			// Kill old threads
			foreach (WorkJobAge tmp in aging) 
			{
				if (tmp.age < agingcount - 5) {
					tmp.tooOld = true;	
					aging.Remove(tmp);
				}
			}
		}

		private static void StartWorkedThread() 
		{
			WorkThread wt = new WorkThread();
			wt.ThreadStart();
			wt.PushData();
		}

		public void StopAllThreads() 
		{
			lock(lockobj)
				createNew = false;
			// Kill threads ( making it while if something gets started in the middelway... )
			while (threads.Count != 0) 
			{

				// Does not clean up aging. It exiting. So why bother?
				foreach (string tname in threads.Keys) 
				{
					log.Debug("Killing thread: " + tname);
					Thread t = (Thread)threads[tname];
					t.Abort();
					while (!t.IsAlive) 
					{
						Thread.Sleep(100);
					}
				}
				threads.Clear();
				log.Debug("Thread count: " + threads.Count);
			}
			log.Debug("Killed all threads");
		}

		private ILoadableCounter FindCounterFromKey(string key)
		{
			foreach (ILoadableCounter c in counters) 
				if(c.isType(key)) 
					return c.getCounter(key);

			return new UnknownCounter();
		}

		// Fetch a counter value from host. ( Passive probing )
		public string FetchCounterValue(string key) 
		{
			ILoadableCounter c = FindCounterFromKey(key);
			return c.getValue();
		}
	}
}
