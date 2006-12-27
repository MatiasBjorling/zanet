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
using ZabbixCommon;

namespace ZabbixCounters.Counters
{
	/// <summary>
	/// Summary description for Version.
	/// </summary>
	public class VersionCounter : ILoadableCounter
	{
		private int _AgentVersionLittle = 9;
		private int _AgentVersionMajor = 0;

		private int _AgentVersionBuild = 13;
		private string _AgentVersion = "Agent.NET";

		public VersionCounter()
		{
		}

		public int getMajorValue() 
		{
			return _AgentVersionMajor;
		}

		public int getLittleValue() 
		{
			return _AgentVersionLittle;
		}

		public int getBuildValue() 
		{
			return _AgentVersionBuild;
		}	

		public string getValue() 
		{
			return _AgentVersion + " " + getMajorValue() + "." + getLittleValue() + " Build: " + getBuildValue();
		}

		/// <summary>
		/// Check if key match counter.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool isType(string key) 
		{ 
			if (key.StartsWith("agent.version"))
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
			return new VersionCounter();
		}
	}
}
