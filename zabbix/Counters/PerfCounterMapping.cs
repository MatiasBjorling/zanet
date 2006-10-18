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
using System.IO;
using System.Collections;
using log4net;
using Microsoft.Win32;
using System.Xml;

namespace ZabbixAgent.Counters
{
	/// <summary>
	/// Summary description for PerfCounterMapping.
	/// </summary>
	public class PerfCounterMapping : ILoadableCounter
	{
		private static readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.PerfCounterMapping");

		private Hashtable ht = null;
		Configuration conf = Configuration.getInstance;
		/// <summary>
		/// Return a counter's value at a given time.
		/// </summary>
		/// <returns></returns>
		public string getValue( ) {
			// Dosent matter as it's a wrapper for perfcounter
			return "";
		}

		/// <summary>
		/// Returns counter object if counter accept the string, else null.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool isType(string key) 
		{ 
			// Get mappings
			Hashtable ht = conf.GetConfigurationBySection("PerfCounters"); 

			if (ht != null) 
				foreach (Object obj in ht.Keys) 
					if (obj.ToString().Equals(key.Trim())) 
						return true;	
			// none matched.
			return false;
		}

		/// <summary>
		/// Request a counter of it self with the key initialized.
		/// </summary>
		public ILoadableCounter getCounter(string key) {

			// Get mappings
			Hashtable ht = conf.GetConfigurationBySection("PerfCounters"); 

			if (ht != null) 
				foreach (Object obj in ht.Keys) 
				{
					if (obj.ToString().Equals(key.Trim())) 
					{
						//log.Info("launching : " +ht[obj].ToString());
						return new Perfcounter(ht[obj].ToString());
					}
				}
			// TODO Return an instance of PerfCounter, with the key stripped for details ( This would be the real performance counter which alias is pointing to )
			return null;
		}

		public PerfCounterMapping(String key) 
		{
			// Dosent matter as it's a wrapper.
		}
		public PerfCounterMapping() {}
	}
}
