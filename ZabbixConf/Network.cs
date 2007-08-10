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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using AMS.Profile;

namespace ZabbixConf
{
	/// <summary>
	/// Summary description for Updater.
	/// </summary>
	public class Network : System.Windows.Forms.Form, ISettingsHandler
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox lsbListenIP;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkUseActiveMode;
		private System.Windows.Forms.CheckBox chkUsePassiveMode;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 
		/// </summary>
		public Network()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			enumIPs();
			ReadSettings();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lsbListenIP = new System.Windows.Forms.ListBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.chkUseActiveMode = new System.Windows.Forms.CheckBox();
			this.chkUsePassiveMode = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkUsePassiveMode);
			this.groupBox1.Controls.Add(this.chkUseActiveMode);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.lsbListenIP);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(560, 232);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Network settings";
			// 
			// lsbListenIP
			// 
			this.lsbListenIP.Location = new System.Drawing.Point(128, 72);
			this.lsbListenIP.Name = "lsbListenIP";
			this.lsbListenIP.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.lsbListenIP.Size = new System.Drawing.Size(176, 82);
			this.lsbListenIP.TabIndex = 50;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 72);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104, 16);
			this.label7.TabIndex = 49;
			this.label7.Text = "Listen on interface";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 51;
			this.label1.Text = "Use Active mode";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 52;
			this.label2.Text = "Use Passive mode";
			// 
			// chkUseActiveMode
			// 
			this.chkUseActiveMode.Location = new System.Drawing.Point(128, 24);
			this.chkUseActiveMode.Name = "chkUseActiveMode";
			this.chkUseActiveMode.Size = new System.Drawing.Size(408, 16);
			this.chkUseActiveMode.TabIndex = 53;
			this.chkUseActiveMode.Text = "Agent contacts Zabbix for counters to monitor. Use with SSH and Update";
			// 
			// chkUsePassiveMode
			// 
			this.chkUsePassiveMode.Location = new System.Drawing.Point(128, 48);
			this.chkUsePassiveMode.Name = "chkUsePassiveMode";
			this.chkUsePassiveMode.Size = new System.Drawing.Size(400, 16);
			this.chkUsePassiveMode.TabIndex = 54;
			this.chkUsePassiveMode.Text = "Zabbix server contacts agent with counters. Only use on secure networks.";
			// 
			// Network
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 246);
			this.Controls.Add(this.groupBox1);
			this.Name = "Network";
			this.Text = "Updater";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		public void Save()
		{
			string listips = selectedips();
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			profile.SetValue("Network", "ListenIP", selectedips().ToString());
			profile.SetValue("Network", "UseActiveMode", chkUseActiveMode.Checked.ToString());
			profile.SetValue("Network", "UsePassiveMode", chkUsePassiveMode.Checked.ToString());
		}

		/// <summary>
		/// 
		/// </summary>
		private void ReadSettings() 
		{
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			try 
			{
				string tmpips = profile.GetValue("Network", "ListenIP").ToString();
				string[] arInfo;
				char[] splitter  = {','};
				arInfo = tmpips.Split(splitter);
				for(int x = 0; x < arInfo.Length - 1; x++)
					{
						lsbListenIP.SelectedItem = (arInfo[x]);
				}
			} 
			catch {}
			try 
			{	
				chkUseActiveMode.Checked = Convert.ToBoolean(profile.GetValue("Network", "UseActiveMode"));
			} 
			catch {}
			try 
			{	
				chkUsePassiveMode.Checked = Convert.ToBoolean(profile.GetValue("Network", "UsePassiveMode"));
			} 
			catch {}

		}

		private void enumIPs()
		{
			// Get host name
			String strHostName = System.Net.Dns.GetHostName();
			
			// Find host by name
			System.Net.IPHostEntry iphostentry = System.Net.Dns.GetHostByName(strHostName);

			// Enumerate IP addresses
			foreach(System.Net.IPAddress ipaddress in iphostentry.AddressList)
			{
				lsbListenIP.Items.Add(ipaddress.ToString());
			}
		}

		private string selectedips()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for(int i=0; i < lsbListenIP.SelectedItems.Count; i++)
			{
				sb.Append(lsbListenIP.SelectedItems[i].ToString());
				sb.Append(",");
			}
			return sb.ToString();
		}
}
}


