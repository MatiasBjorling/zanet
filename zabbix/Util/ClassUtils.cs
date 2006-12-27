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
using System.Reflection;
using System.IO;
using System.Collections;
using log4net;
using ZabbixCommon;


namespace ZabbixAgent.Util
{
	/// <summary>
	/// Summary description for ClassUtils.
	/// </summary>
	public class ClassUtils
	{
		private static readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.util.classutils");

		/// <summary>
		/// Looks for ILookCounter Classes and returns them in a arraylist.
		/// </summary>
		public static ArrayList ScanILoadableCounter()
		{
			
			// Tmo code
			Assembly ab = Assembly.Load("ZabbixCounters, Culture=neutral, PublicKeyToken=60e868a7b39e2319");

			Type[] asmTypes2 = ab.GetTypes();
			foreach (Type t in asmTypes2) 
			{
				log.Debug("ZabbixCounters: " + t.ToString());
				if (t.IsClass) 
				{
					Type[] interfaces = t.GetInterfaces();
					foreach (Type i in interfaces) 
					{
						if (i.Equals(typeof(ILoadableCounter)))
						{
							//a.Add(t);
							log.Info("Counter: " + t.Name + " found");
						}
					}
				}
			}

			ArrayList a = new ArrayList(10);

			log.Debug("Class Scan Begin");
			Assembly asm = Assembly.GetAssembly(typeof(ZabbixAgent.Active));

			Type[] asmTypes = asm.GetTypes();
			foreach (Type t in asmTypes) 
			{
				if (t.IsClass) 
				{
					Type[] interfaces = t.GetInterfaces();
					foreach (Type i in interfaces) 
					{
						if (i.Equals(typeof(ILoadableCounter)))
						{
							a.Add(t);
							log.Info("Counter: " + t.Name + " found");
						}
					}
				}
			}
			log.Debug("Class Scan Ended");
			return a;
		}
	}
}
