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


namespace ZabbixCore
{
	/// <summary>
	/// WorkJob is used for transfering parametres to threads.
	/// </summary>
	public class WorkJob
	{
		// Instance lock
		static readonly object uselock =  new object();
		static WorkJob instance = null;

		public string key;
		public int interval;
		public int unknown;
		public ILoadableCounter monitorobj;
		public WorkJobAge wja;
		public int averageInterval;

		public bool readed = false;

		public WorkJob() {}

		public void AddParams(string key, int i, int j, ILoadableCounter monitorobj, WorkJobAge wja, int averageInterval) 
		{
			this.key = key;
			this.interval = i;
			this.unknown = j;
			this.monitorobj = monitorobj;
			this.wja = wja;
			this.averageInterval = averageInterval;
		}

		public static WorkJob getJob 
		{
			get 
			{
				lock(uselock) 
				{
					if (instance == null) 
					{
						instance = new WorkJob();
					}
					return instance;
				}
			}
		}
	}
}
