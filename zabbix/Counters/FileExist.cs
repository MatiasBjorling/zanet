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

namespace ZabbixAgent.Counters
{
	/// <summary>
	/// Checks if a file exist. Return 1 if true else 0 
	/// </summary>
	public class FileExist : ILoadableCounter
	{
		String filename;

		public FileExist() {}

		/// <summary>
		/// Constructor for FileExist. Sets filename to be monitored for counter.
		/// </summary>
		/// <param name="filename"></param>
		public FileExist(string filename)
		{
			this.filename = filename;
		}

		/// <summary>
		/// Returns counter value at a given time.
		/// </summary>
		/// <returns></returns>
		public string getValue() 
		{
			if (File.Exists(filename))
				return "1";
			else
				return "0";
		}

		/// <summary>
		/// Check if type is vfs.file.exists and give back FileExist object.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool isType(string key) 
		{ 
			if (key.StartsWith("vfs.file.exists") )
				return true;
			else
				return false;
		}

		public ILoadableCounter getCounter(string key)
		{ 
			return new FileExist(key.Remove(0,key.IndexOf("[",0, key.Length)).Trim('[').Trim(']'));
		}
	}
}
