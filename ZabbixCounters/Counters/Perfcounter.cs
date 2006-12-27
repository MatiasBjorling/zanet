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
using System.Collections;
using ZabbixCommon;
using log4net;

namespace ZabbixCounters.Counters
{
	/// <summary>
	/// Summary description for Perfcounter.
	/// </summary>
	/// 
	
	public class Perfcounter : ILoadableCounter
	{
		private static readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.perfcounter");

		private static PerformanceCounter pc;

		private string instance = "";
		private string category = "";
		private string instancekey = "";

		static readonly object uselock =  new object();

		public Perfcounter() {}

		public Perfcounter(String key)
		{
			// String magic
			string[] keysplit = key.Split('\\');
					
			instancekey = keysplit[2];

			int length = keysplit[1].Length;
			int startkey	= keysplit[1].IndexOf("(",0,length-1);

			if (startkey != -1) 
			{
				
				instance = keysplit[1].Substring(startkey+1, keysplit[1].Length-startkey-2);					
				category = keysplit[1].Substring(0, startkey);
			} 
			else 
			{
				category = keysplit[1];
			}
		}

		public string getValue() 
		{
			/*
			 * FIXME. Figure out how the performance counter can be handler in threads without locking all, so there aint any racecondition.
			 */
			lock(uselock)
			{
				try 
				{
					if (!instance.Equals("")) 
					{
						pc = new PerformanceCounter(category, instancekey, instance, true);		
					} 
					else 
					{
						pc = new PerformanceCounter(category, instancekey,true);
					}

					
						// Start the engine for performance monitor
						string tmp = "";

						// pc.NextValue(); ---Was wrong
						// tmp = pc.NextValue().ToString("0.000000").Replace(",","."); ---Was wrong
						//log.Debug("Getting: " + category + "-" + instancekey + "-" + instance + " Value: " + tmp);
					
						// return tmp;


					if(pc.CounterType.ToString().StartsWith("NumberOfItems"))
						{
							tmp = pc.RawValue.ToString("0.000000").Replace(",",".");
							return tmp;
						}
						else
						{
							// Retrieve a sample.
							CounterSample sample1 = pc.NextSample();
							// Wait some interval of time here and retrieve
							// a second sample.
							System.Threading.Thread.Sleep(1000);
							CounterSample sample2 = pc.NextSample();
							// Pass both samples to the Calculate method.
							tmp = CounterSample.Calculate(sample1, sample2).ToString("0.000000").Replace(",",".");
							return tmp;
						}					

					} 
				catch (Exception) 
				{
					//log.Error(ex.Message);/*
					//Console.WriteLine("Error: " + ex.Message);
					/*if (ex.InnerException != null)
						log.Error(ex.InnerException.Message + ex.InnerException.StackTrace);
					else 
						log.Error(ex.Message + ex.StackTrace);*/
				}
			}
			return "-1";
		}

		/// <summary>
		/// Check if key match counter.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool isType(string key) 
		{ 
			// Performance monitor
			if (key.StartsWith("perf_counter")) 
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
			string strippedkey = key.Remove(0,key.IndexOf("[",0, key.Length)).Trim(']').Trim('[');
			if (key.IndexOf("],", 0, key.Length) != -1) 
			{
				string averagestring = key.Remove(0,key.IndexOf(",",0,key.Length)+1);
					
				// Is this a performance average key
				if (!averagestring.Equals("") )
				{
					try 
					{
						// Fix for extended average counter.
						strippedkey = key.Remove(key.IndexOf("],", 0), key.Length - key.IndexOf("],", 0)).Remove(0,key.IndexOf("[",0) +1);
					} 
					catch(Exception ex) 
					{
						log.Error("Ignoring average counter for key: " + key + ex.Message);
					}
				}
			} 
			return new Perfcounter(strippedkey);
		}


	}
}
