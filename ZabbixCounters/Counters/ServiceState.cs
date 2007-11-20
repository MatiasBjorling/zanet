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
using System.Diagnostics;
using System.ServiceProcess;
using ZabbixCommon;

namespace ZabbixCounters.Counters
{
	/// <summary>
	/// Summary description for ServiceState.
	/// </summary>
	public class ServiceState : ILoadableCounter
	{
		ServiceController[] services;
		string servicename;
		
		public ServiceState(string servicename)
		{
			this.servicename = servicename;
		}

		public ServiceState() {}

		public string getValue() 
		{
			services = ServiceController.GetServices();
			foreach(ServiceController service in services) 
			{
				if (service.ServiceName.Equals(servicename)) 
				{
					switch (service.Status) 
					{
						case ServiceControllerStatus.Running:
							return "0";
						case ServiceControllerStatus.Paused:
							return "1";
						case ServiceControllerStatus.StartPending: 
							return "2";
						case ServiceControllerStatus.PausePending:
							return "3";
						case ServiceControllerStatus.ContinuePending:
							return "4";
						case ServiceControllerStatus.StopPending:
							return "5";
						case ServiceControllerStatus.Stopped:
							return "6";
					}
				}
			}
			return "7";
		}
		/// <summary>
		/// Check if key match counter.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool isType(string key) 
		{ 
			if (key.StartsWith("service_state"))
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
			string strippedkey = key.Remove(0,key.IndexOf("[",0, key.Length)).Trim('[').Trim(']');
			return new ServiceState(strippedkey);
		}
	}
}
