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
using System.Data.SqlClient;
using log4net;

namespace ZabbixAgent.Counters
{
	/// <summary>
	/// Counter for getting aggregated values from database.
	/// </summary>
	public class SQLSelect : ILoadableCounter
	{
		private readonly ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixagent.counter.sqlselect");

		string sqlquery = "";

		private SqlConnection conn;
		private string server = "";
		private string username = "";
		private string password = "";
		private string database= "";

		public SQLSelect() {}

		/// <summary>
		/// Constructor for SQLSelect. Set configuration from config file and sets sql query to be used for getValue.
		/// </summary>
		/// <param name="sqlquery"></param>
		public SQLSelect(string sqlquery)
		{
			this.sqlquery = sqlquery;

			Configuration conf = Configuration.getInstance;

			this.server = conf.GetConfigurationByString("MSSQLServer", "SQL");
			this.username = conf.GetConfigurationByString("MSSQLUsername", "SQL");
			this.password = conf.GetConfigurationByString("MSSQLPassword", "SQL");
			this.database = conf.GetConfigurationByString("MSSQLDatabase", "SQL");
			
			//log.Debug("Starting counter with: " + this.server + " U: " + this.username + " P: " + this.password + " DB: " + this.database);
		}

		/// <summary>
		/// Return scalar value from SQL query.
		/// </summary>
		/// <returns></returns>
		public string getValue() 
		{
			// Some connection pooling?
			try 
			{
				GetConnection();
				conn.Open();

				SqlCommand sqlcmd = new SqlCommand(sqlquery, conn);
				string tmp = "-1";

				if (sqlquery.ToLower().StartsWith("select ")) 
				{
					tmp = sqlcmd.ExecuteScalar().ToString();
				} 
				else 
				{
					log.Error("Not a valid SQL Query: " + sqlquery);
				}

				conn.Close();
				return tmp;
			} 
			catch (Exception ex) 
			{	
				log.Error(ex.Message + ex.StackTrace);
				return "-1";
			}			
		}

		private void GetConnection() 
		{
			conn = new SqlConnection("server='" + this.server + "';user id='" + this.username + "';password='" + this.password + "';database='" + this.database + "'");
		}

		/// <summary>
		/// Check if key match counter.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool isType(string key) 
		{ 
			if (key.StartsWith("mssql.scalar"))
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
			return new SQLSelect(key.Remove(0,key.IndexOf("[",0, key.Length)).Trim('[').Trim(']'));
		}
	}
}
