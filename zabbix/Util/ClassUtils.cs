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
using System.Reflection;
using System.IO;
using System.Collections;
using log4net;
using ZabbixCommon;
using System.Diagnostics;


namespace ZabbixCore.Util
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
			
			ArrayList a = new ArrayList(10);
			
			
			//Assembly asm = Assembly.GetAssembly(typeof(ZabbixAgent.Active));
#if (DEBUG)
			Assembly asm = Assembly.Load("ZabbixCounters, Culture=neutral");
#else
			Assembly asm = Assembly.Load("ZabbixCounters, Culture=neutral, PublicKeyToken=a7296e6a43eb88e1");
#endif
			FileVersionInfo f = FileVersionInfo.GetVersionInfo(asm.Location);
			log.Info("Loading agent with: [" + f.OriginalFilename + ", Version: " + f.ProductVersion + " With debugging: " + f.IsDebug +"]");
			
			log.Debug("Counter Scan Begin");
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
			log.Debug("Counter Scan Ended");

			// Finding local assemblies.
			asm = Assembly.GetAssembly(typeof(ZabbixCore.Active));
			log.Debug("Fetching default counters");
			asmTypes = asm.GetTypes();
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
			log.Debug("Counter Scan Ended");

			return a;
		}
	}
}
