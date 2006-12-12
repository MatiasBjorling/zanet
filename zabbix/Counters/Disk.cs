#region CopyRight
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
#endregion

using System;
using System.IO;
using System.Management;
using log4net;

namespace ZabbixAgent.Counters
{
	/// <summary>
	/// Disk check
	/// when the key contains free, return free disk space in bytes
	/// when the key contains total, return total disk space in bytes
	/// when the key contains used, return used disk space in bytes
	/// </summary>
	public class Disk : ILoadableCounter
	{
		private readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.counter.disk");

		String diskname;
		String disktype;

		public Disk() {}

		/// <summary>
		/// Constructor for Disk. Sets diskname to be monitored for counter, and what to return.
		/// </summary>
		/// <param name="diskname"></param>
		public Disk(string diskname, string disktype)
		{
			this.diskname = diskname;
			this.disktype = disktype;
			
		}

		/// <summary>
		/// Returns counter value at a given time.
		/// </summary>
		/// <returns></returns>
		public string getValue() 
		{
			try
			{
			ManagementObject mydisk = new 
			ManagementObject("win32_logicaldisk.deviceid='" + diskname + "'"); 
			mydisk.Get();
			if (disktype.Equals("total") )
				return mydisk["Size"].ToString();
			else if (disktype.Equals("used") )
				return (Convert.ToDouble(mydisk["Size"]) - Convert.ToDouble(mydisk["FreeSpace"])).ToString();
			else if (disktype.Equals("free") )
				return mydisk["FreeSpace"].ToString();
			else
				log.Info("vfs.fs.size does not take " + disktype + " as parameter.");
				return "-1";
			}
			catch
			{
				log.Info("Please check you configuration (vfs.fs.size), disk: " + diskname + " is not present on this host.");
				return "-1";
			}
			//return mydisk["Size"].ToString(); 
			//return mydisk["FreeSpace"].ToString();
		}

		/// <summary>
		/// Check if type is vfs.fs.size and give back Disk object.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool isType(string key) 
		{ 
			if (key.StartsWith("vfs.fs.size") )
				return true;
			else
				return false;
		}

		public ILoadableCounter getCounter(string key)
		{ 
			string[] tokens = key.Split(new char[] { '[', ',', ']'});
			return new Disk(tokens[1], tokens[2]);



		}
	}
}
