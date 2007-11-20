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
using System.IO;
using System.Text;
using System.Net;
using ZabbixCommon;
using log4net;

namespace ZabbixCounters.Counters
{
	/// <summary>
	/// Gets time which a webpage is loaded.
	/// </summary>
	public class Httping : ILoadableCounter
	{
		private readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.counter.web.httping");

		HttpWebRequest request = null;
		string webaddress = "";
		public Httping(string webaddress)
		{
			this.webaddress = webaddress;
		}

		public Httping() 
		{

		}

		public string getValue()
		{
			TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
		
			request = (HttpWebRequest)HttpWebRequest.Create(webaddress);
			request.Timeout = 5000;
			try 
			{
				request.GetResponse().Close();
			} 
			catch (WebException) 
			{
				log.Error("Website " + webaddress + " timed out.");
			}
			
			TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
			ts1 = ts2.Subtract(ts1);

			return ts1.TotalMilliseconds.ToString("00.000000").Replace(",",".");
		}

		public bool isType(string key)
		{
			if (key.ToLower().StartsWith("web.httping"))
				return true;
			else 
				return false;
		}

		public ILoadableCounter getCounter(string key)
		{
			return new Httping(key.Remove(0,key.IndexOf("[",0, key.Length)).Trim('[').Trim(']'));
		}


	}
}
