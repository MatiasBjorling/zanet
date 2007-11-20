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
using log4net;
using ZabbixCommon;


namespace ZabbixCounters.Counters
{
	/// <summary>
	/// Summary description for RemoteUpdate.
	/// </summary>
	public class RemoteUpdateLink : ILoadableCounter
	{
		ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.remoteupdate");

		Configuration conf = Configuration.getInstance;

		public RemoteUpdateLink() {}

		// Check for new server version and new client verion, update if not equal
		public string getValue() 
		{
			return null;
		}

		/// <summary>
		/// Check if key match counter.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool isType(string key) 
		{ 
			// Stores MD5 in Configuration hashtable. This is used for updater
			if (key.StartsWith("agent.update.link")) 
			{
				Configuration.SetObjectInHashByKey("UpdatePackageLink", key.Remove(0,key.IndexOf("[",0, key.Length)).Trim('[').Trim(']'));
				return true;
			} 
			return false;
		}

		/// <summary>
		/// Set the key for the counter.
		/// </summary>
		/// <param name="key"></param>
		public ILoadableCounter getCounter(string key)
		{ 
			// It's a setter counter, so this will just return null, it will be accepted as a item, but it will not create any instance of the object.
			return null;
		}
	}


}
