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
using System.IO;
using System.Collections;
using Microsoft.Win32;
using System.Xml;
using AMS.Profile;
using System.Windows.Forms;

namespace ZabbixCommon
{
	
	public class Configuration
	{
		//private static readonly string RegistryRoot = ".DEFAULT\\Software\\Zabbix\\MonitoringAgent\\v1";
		private static readonly string ConfigFile = Application.StartupPath +"\\ZabbixAgent.xml";

		private readonly log4net.ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.configuration");

		static Configuration instance = null;

		// Instance lock
		static readonly object uselock =  new object();

		

		// New Configuration
		private Hashtable ht = new Hashtable(20);

		private Configuration()
		{
			
		}

		// Small hashtable to keep elements between processes.
		private static Hashtable globalHashtable = new Hashtable(1);
		private static readonly object hashtableLock = new object();

		public static object GetObjectInHashByKey(string key) 
		{
			lock (hashtableLock)
				return globalHashtable[key];
		}

		public static void SetObjectInHashByKey(string key, string val) 
		{
			lock (hashtableLock)
				globalHashtable[key] = val;
		}


		/// <summary>
		/// Returns a configuration parameter from configuration file as a string.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string GetConfigurationByString(string str) 
		{
			Hashtable t = (Hashtable)ht["General"];
			if (t != null)
				if (t[str] != null) 
				{
					return t[str].ToString();
				}
			return "";
		}

		public string GetConfigurationByString(string str, string section) 
		{
			Hashtable t= (Hashtable)ht[section];
			if (t !=null) 
				if (t[str] != null) 
				{
					return t[str].ToString();
				} 
				else 
				{
					throw new Exception("Cannot find the specified key " + str + " in section " + section);
				}
			else 
				throw new Exception("Cannot find the specified section " + section + ". Check missing configuration file.");
			//return "";
		}

		public Hashtable GetConfigurationBySection(string section) 
		{
			// Return type raw... There can happing an exception here if not an hashtable is returned.
			return (Hashtable)ht[section];
		}

		// Singleton
		public static Configuration getInstance
		{
			get 
			{
				lock(uselock) 
				{
					if (instance == null) 
					{
						instance = new Configuration();
						instance.GetConfiguration();
					}

					return instance;
				}
			}
		}

		public void GetConfiguration() 
		{
			log.Info("Configuration setup read from: " + ConfigFile);
			try 
			{

				Xml profile = new Xml(ConfigFile);
				
				string[] sections = profile.GetSectionNames();

				if (sections != null) 
				{
					foreach(string section in profile.GetSectionNames()) 
					{
						log.Info("\tSection: " + section);
						Hashtable htsection = new Hashtable();

						foreach(string val in profile.GetEntryNames(section)) 
						{
							Object obj = profile.GetValue(section,val);
							log.Info("\t\t"+val+": " + obj  );

							htsection.Add(val, obj);
						}   
						ht.Add(section, htsection);
					}       
					log.Info("Loaded configuration");
				} 
				else throw new Exception("Cannot find configuration file " + ConfigFile + " or configuration file is empty.");
				profile.Buffer().Close();
				profile.Buffer().Dispose();
			} catch (Exception ex) {				
				log.Error(ex.Message);
			}
		}
	}
}
