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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.IO;
using log4net;
using log4net.Config;
using System.Resources;
using System.Reflection;
using ZabbixAgent;

namespace ZabbixAgent
{
	public class Service1 : System.ServiceProcess.ServiceBase
	{
		private Thread plinkoutputthread;
		private System.ComponentModel.Container components = null;

		private static ZabbixAgent.Active ac = null;

		public Service1()
		{
			InitializeComponent();
		}

		static void Main()
		{
			
			System.ServiceProcess.ServiceBase[] ServicesToRun;
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new Service1() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}

		private void InitializeComponent()
		{
			// 
			// Service1
			// 
			this.ServiceName = "tmcremotemon";

		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		protected override void OnStart(string[] args)
		{
			
			// Start a thread for getting output from plink
			ThreadStart tjob = new ThreadStart(ThreadOutput);
			plinkoutputthread = new Thread(tjob);
			plinkoutputthread.Start();

		}
 
		protected override void OnStop()
		{
			if (plinkoutputthread != null) 
			{
				// Stops secure connections too
				ac.Stop();
				plinkoutputthread.Abort();
			}
		}

		/*
		 * Thread til at vise output for plink
		 */
		protected static void ThreadOutput() 
		{
			AgentHandling.Start();
		}

	}
}
