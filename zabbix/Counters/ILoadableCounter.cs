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

namespace ZabbixAgent.Counters
{
	/// <summary>
	/// Interface implementation of a counter. This should be used for implementing counters.
	/// </summary>
	public interface ILoadableCounter
	{
		/// <summary>
		/// Return a counter's value at a given time.
		/// </summary>
		/// <returns></returns>
		string getValue();

		/// <summary>
		/// Returns counter object if counter accept the string, else null.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		bool isType(string key);

		/// <summary>
		/// Request a counter of it self with the key initialized.
		/// </summary>
		ILoadableCounter getCounter(string key);
	}
}
