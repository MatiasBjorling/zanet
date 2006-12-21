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
using System.Threading;
using System.Text;
using log4net;
using log4net.Config;
using System.Resources;
using System.Reflection;


namespace ZabbixAgent
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		private static readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.agent");
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            
            
            foreach (string tmp in Assembly.GetCallingAssembly().GetManifestResourceNames())
            {
                   Console.WriteLine(tmp);
            }


#if (DEBUG)
			XmlConfigurator.Configure(Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixAgent.SubSystem.log_debug.xml"));
#else
			XmlConfigurator.Configure(Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixAgent.SubSystem.log.xml"));
#endif		
			log.Info("Starting agent [" + (new Counters.VersionCounter()).getValue() + "]");
			
			while (true) 
			{
				try 
				{
					ZabbixAgent.Active ac = new ZabbixAgent.Active();
					ZabbixAgent.Passive ap = new ZabbixAgent.Passive();
					ac.get_active_checks();
					Thread.Sleep(10000);
				} 
				catch (Exception ex)
				{
					log.Error(ex.Message + ex.StackTrace);
				}
			}
		}		
	}
}
