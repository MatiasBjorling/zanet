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

namespace ZabbixAgent
{
	/// <summary>
	/// Summary description for WorkJobAge.
	/// </summary>
	public class WorkJobAge
	{
		/// <summary>
		/// Counter name from Zabbix.
		/// </summary>
		public string key;

		/// <summary>
		/// Age integer which will be increased.
		/// </summary>
	  public long age;

		/// <summary>
		/// The param will be set when thread should die.
		/// </summary>
		public bool tooOld = false;

		/// <summary>
		/// Initialize the age with show old it is and what counter it represents. 
		/// </summary>
		/// <param name="key">Countername.</param>
		/// <param name="age">Start age of thread.</param>
		public WorkJobAge(string key, long age) 
		{
			this.key = key;
			this.age = age;

		}

		/// <summary>
		/// Increase age by one. 
		/// </summary>
		public void increaseage() 
		{
			age += 1;
		}

		/// <summary>
		/// Resets the age to 0. ( Keeps the thread alive )  
		/// </summary>
		public void resetage()
		{
			age = 0;
		}
	}
}
