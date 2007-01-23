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
using ZabbixCommon;
using log4net;

namespace ZabbixCounters.Counters
{
	/// <summary>
	/// Gets the last updated timestamp for a file. ( Last modified )
	/// Returns DD-MM-YY HH:SS
	/// </summary>
	public class FileTime : ILoadableCounter
	{
		private readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.filetime");

		private string filename;
		
		public FileTime() {}

		public FileTime(string filename)
		{
			this.filename = filename;
		}

		public string getValue() 
		{
			if (File.Exists(filename)) 
			{
				DateTime date = File.GetLastWriteTime(filename);
				DateTime unixepoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

				System.TimeSpan ts = new TimeSpan(date.Ticks - unixepoch.Ticks);
				return ((int)ts.TotalSeconds).ToString();
			} else
				return "-1";
		}

		/// <summary>
		/// Check if key match counter.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool isType(string key) 
		{ 
			if (key.StartsWith("vfs.file.time") )
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
			return new FileTime(key.Remove(0,key.IndexOf("[",0, key.Length)).Trim('[').Trim(']'));
		}
	}
}
