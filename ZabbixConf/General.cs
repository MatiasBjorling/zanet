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
using System.Data;
using Microsoft.Win32;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Xml;
using AMS.Profile;

namespace ZabbixConf
{
	/// <summary>
	/// Summary description for General.
	/// </summary>
	public class General : System.Windows.Forms.Form, ISettingsHandler
	{
		private System.ComponentModel.IContainer components;
		/// <summary>
		/// 
		/// </summary>
		public static string RegistryRoot = ".DEFAULT\\Software\\Zabbix\\MonitoringAgent\\v1";
		//public static string ConfigFile = "ZabbixAgent.xml";
		/// <summary>
		/// 
		/// </summary>
		public static string PuttyKeyGlobal = ".DEFAULT\\Software\\SimonTatham\\PuTTY\\SshHostKeys";
		/// <summary>
		/// 
		/// </summary>
		public static string PuttyKeyRoot = "Software\\SimonTatham\\PuTTY\\SshHostKeys";
		internal System.Windows.Forms.Label lblHostname;
		internal System.Windows.Forms.GroupBox grpBoxGeneral;
		private System.Windows.Forms.Label lblFQDN;
		private System.Windows.Forms.Label lblServerHost;
		private System.Windows.Forms.Label lblServerPort;
		private System.Windows.Forms.Label lblAgentPort;
		private System.Windows.Forms.Label lblRefresh;
		private System.Windows.Forms.Label lblActiveChecks;
		private System.Windows.Forms.Label lblTimeOut;
		private System.Windows.Forms.Label lblRemComm;
		private System.Windows.Forms.TextBox txtHostname;
		private System.Windows.Forms.TextBox txtServerHost;
		private System.Windows.Forms.TextBox txtServerPort;
		private System.Windows.Forms.TextBox txtAgentPort;
		private System.Windows.Forms.TextBox txtRefresh;
		private System.Windows.Forms.CheckBox chkFQDN;
		private System.Windows.Forms.CheckBox chkActiveChecks;
		private System.Windows.Forms.CheckBox chkRemComm;
		private System.Windows.Forms.NumericUpDown nudTimeOut;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtQueueLength;
		private System.Windows.Forms.CheckBox chkUseQueue;
		private Hashtable ht = new Hashtable(20);

		/// <summary>
		/// 
		/// </summary>
		public General()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			ReadValues();

			// The hostname is locked to the FQDN until the performance monitor respects it.
			txtHostname.Text = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).HostName;
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
			this.grpBoxGeneral = new System.Windows.Forms.GroupBox();
			this.txtQueueLength = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.chkUseQueue = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.chkRemComm = new System.Windows.Forms.CheckBox();
			this.lblRemComm = new System.Windows.Forms.Label();
			this.nudTimeOut = new System.Windows.Forms.NumericUpDown();
			this.lblTimeOut = new System.Windows.Forms.Label();
			this.chkActiveChecks = new System.Windows.Forms.CheckBox();
			this.txtRefresh = new System.Windows.Forms.TextBox();
			this.lblActiveChecks = new System.Windows.Forms.Label();
			this.lblRefresh = new System.Windows.Forms.Label();
			this.txtAgentPort = new System.Windows.Forms.TextBox();
			this.lblAgentPort = new System.Windows.Forms.Label();
			this.chkFQDN = new System.Windows.Forms.CheckBox();
			this.lblFQDN = new System.Windows.Forms.Label();
			this.txtServerPort = new System.Windows.Forms.TextBox();
			this.txtServerHost = new System.Windows.Forms.TextBox();
			this.txtHostname = new System.Windows.Forms.TextBox();
			this.lblServerPort = new System.Windows.Forms.Label();
			this.lblServerHost = new System.Windows.Forms.Label();
			this.lblHostname = new System.Windows.Forms.Label();
			this.grpBoxGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudTimeOut)).BeginInit();
			this.SuspendLayout();
			// 
			// grpBoxGeneral
			// 
			this.grpBoxGeneral.Controls.Add(this.txtQueueLength);
			this.grpBoxGeneral.Controls.Add(this.label5);
			this.grpBoxGeneral.Controls.Add(this.label4);
			this.grpBoxGeneral.Controls.Add(this.label3);
			this.grpBoxGeneral.Controls.Add(this.chkUseQueue);
			this.grpBoxGeneral.Controls.Add(this.label2);
			this.grpBoxGeneral.Controls.Add(this.label1);
			this.grpBoxGeneral.Controls.Add(this.chkRemComm);
			this.grpBoxGeneral.Controls.Add(this.lblRemComm);
			this.grpBoxGeneral.Controls.Add(this.nudTimeOut);
			this.grpBoxGeneral.Controls.Add(this.lblTimeOut);
			this.grpBoxGeneral.Controls.Add(this.chkActiveChecks);
			this.grpBoxGeneral.Controls.Add(this.txtRefresh);
			this.grpBoxGeneral.Controls.Add(this.lblActiveChecks);
			this.grpBoxGeneral.Controls.Add(this.lblRefresh);
			this.grpBoxGeneral.Controls.Add(this.txtAgentPort);
			this.grpBoxGeneral.Controls.Add(this.lblAgentPort);
			this.grpBoxGeneral.Controls.Add(this.chkFQDN);
			this.grpBoxGeneral.Controls.Add(this.lblFQDN);
			this.grpBoxGeneral.Controls.Add(this.txtServerPort);
			this.grpBoxGeneral.Controls.Add(this.txtServerHost);
			this.grpBoxGeneral.Controls.Add(this.txtHostname);
			this.grpBoxGeneral.Controls.Add(this.lblServerPort);
			this.grpBoxGeneral.Controls.Add(this.lblServerHost);
			this.grpBoxGeneral.Controls.Add(this.lblHostname);
			this.grpBoxGeneral.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpBoxGeneral.Location = new System.Drawing.Point(8, 8);
			this.grpBoxGeneral.Name = "grpBoxGeneral";
			this.grpBoxGeneral.Size = new System.Drawing.Size(560, 296);
			this.grpBoxGeneral.TabIndex = 5;
			this.grpBoxGeneral.TabStop = false;
			this.grpBoxGeneral.Text = "General";
			// 
			// txtQueueLength
			// 
			this.txtQueueLength.Location = new System.Drawing.Point(168, 264);
			this.txtQueueLength.Name = "txtQueueLength";
			this.txtQueueLength.Size = new System.Drawing.Size(64, 20);
			this.txtQueueLength.TabIndex = 43;
			this.txtQueueLength.Text = "50000";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 264);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.TabIndex = 42;
			this.label5.Text = "Queue length";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(192, 240);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(216, 16);
			this.label4.TabIndex = 41;
			this.label4.Text = "Use only with timestamp patched server.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 240);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(144, 16);
			this.label3.TabIndex = 40;
			this.label3.Text = "Enable queue";
			// 
			// chkUseQueue
			// 
			this.chkUseQueue.Location = new System.Drawing.Point(168, 240);
			this.chkUseQueue.Name = "chkUseQueue";
			this.chkUseQueue.Size = new System.Drawing.Size(16, 16);
			this.chkUseQueue.TabIndex = 39;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(184, 160);
			this.label2.Name = "label2";
			this.label2.TabIndex = 38;
			this.label2.Text = "Not implemented";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(224, 88);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(264, 32);
			this.label1.TabIndex = 37;
			this.label1.Text = "If SSH is used. Zabbix server is localhost and the port is the local bound port.";
			// 
			// chkRemComm
			// 
			this.chkRemComm.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkRemComm.Location = new System.Drawing.Point(168, 214);
			this.chkRemComm.Name = "chkRemComm";
			this.chkRemComm.Size = new System.Drawing.Size(16, 16);
			this.chkRemComm.TabIndex = 36;
			// 
			// lblRemComm
			// 
			this.lblRemComm.Location = new System.Drawing.Point(8, 216);
			this.lblRemComm.Name = "lblRemComm";
			this.lblRemComm.Size = new System.Drawing.Size(144, 16);
			this.lblRemComm.TabIndex = 35;
			this.lblRemComm.Text = "Enable remote commands?";
			// 
			// nudTimeOut
			// 
			this.nudTimeOut.Location = new System.Drawing.Point(168, 185);
			this.nudTimeOut.Maximum = new System.Decimal(new int[] {
																	   30,
																	   0,
																	   0,
																	   0});
			this.nudTimeOut.Minimum = new System.Decimal(new int[] {
																	   1,
																	   0,
																	   0,
																	   0});
			this.nudTimeOut.Name = "nudTimeOut";
			this.nudTimeOut.Size = new System.Drawing.Size(40, 20);
			this.nudTimeOut.TabIndex = 34;
			this.nudTimeOut.Value = new System.Decimal(new int[] {
																	 3,
																	 0,
																	 0,
																	 0});
			// 
			// lblTimeOut
			// 
			this.lblTimeOut.Location = new System.Drawing.Point(8, 191);
			this.lblTimeOut.Name = "lblTimeOut";
			this.lblTimeOut.Size = new System.Drawing.Size(136, 16);
			this.lblTimeOut.TabIndex = 33;
			this.lblTimeOut.Text = "Timeout (seconds)";
			this.lblTimeOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkActiveChecks
			// 
			this.chkActiveChecks.Enabled = false;
			this.chkActiveChecks.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkActiveChecks.Location = new System.Drawing.Point(167, 164);
			this.chkActiveChecks.Name = "chkActiveChecks";
			this.chkActiveChecks.Size = new System.Drawing.Size(16, 16);
			this.chkActiveChecks.TabIndex = 32;
			this.chkActiveChecks.TabStop = false;
			// 
			// txtRefresh
			// 
			this.txtRefresh.Location = new System.Drawing.Point(167, 136);
			this.txtRefresh.Name = "txtRefresh";
			this.txtRefresh.Size = new System.Drawing.Size(48, 20);
			this.txtRefresh.TabIndex = 31;
			this.txtRefresh.Text = "120";
			// 
			// lblActiveChecks
			// 
			this.lblActiveChecks.Location = new System.Drawing.Point(8, 167);
			this.lblActiveChecks.Name = "lblActiveChecks";
			this.lblActiveChecks.Size = new System.Drawing.Size(136, 16);
			this.lblActiveChecks.TabIndex = 30;
			this.lblActiveChecks.Text = "Disable active checks";
			// 
			// lblRefresh
			// 
			this.lblRefresh.Location = new System.Drawing.Point(8, 142);
			this.lblRefresh.Name = "lblRefresh";
			this.lblRefresh.Size = new System.Drawing.Size(152, 16);
			this.lblRefresh.TabIndex = 29;
			this.lblRefresh.Text = "Refresh Active Checks (secs)";
			// 
			// txtAgentPort
			// 
			this.txtAgentPort.Enabled = false;
			this.txtAgentPort.Location = new System.Drawing.Point(167, 112);
			this.txtAgentPort.Name = "txtAgentPort";
			this.txtAgentPort.Size = new System.Drawing.Size(49, 20);
			this.txtAgentPort.TabIndex = 28;
			this.txtAgentPort.Text = "Not used";
			// 
			// lblAgentPort
			// 
			this.lblAgentPort.Location = new System.Drawing.Point(8, 119);
			this.lblAgentPort.Name = "lblAgentPort";
			this.lblAgentPort.Size = new System.Drawing.Size(136, 16);
			this.lblAgentPort.TabIndex = 27;
			this.lblAgentPort.Text = "Agent listen port";
			// 
			// chkFQDN
			// 
			this.chkFQDN.Checked = true;
			this.chkFQDN.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFQDN.Enabled = false;
			this.chkFQDN.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkFQDN.Location = new System.Drawing.Point(167, 40);
			this.chkFQDN.Name = "chkFQDN";
			this.chkFQDN.Size = new System.Drawing.Size(16, 16);
			this.chkFQDN.TabIndex = 26;
			this.chkFQDN.TabStop = false;
			this.chkFQDN.CheckedChanged += new System.EventHandler(this.useFQDN_changed);
			// 
			// lblFQDN
			// 
			this.lblFQDN.Location = new System.Drawing.Point(8, 43);
			this.lblFQDN.Name = "lblFQDN";
			this.lblFQDN.Size = new System.Drawing.Size(100, 16);
			this.lblFQDN.TabIndex = 25;
			this.lblFQDN.Text = "Use FQDN ?";
			// 
			// txtServerPort
			// 
			this.txtServerPort.Location = new System.Drawing.Point(167, 88);
			this.txtServerPort.Name = "txtServerPort";
			this.txtServerPort.Size = new System.Drawing.Size(48, 20);
			this.txtServerPort.TabIndex = 2;
			this.txtServerPort.Text = "10051";
			// 
			// txtServerHost
			// 
			this.txtServerHost.Location = new System.Drawing.Point(167, 64);
			this.txtServerHost.Name = "txtServerHost";
			this.txtServerHost.Size = new System.Drawing.Size(256, 20);
			this.txtServerHost.TabIndex = 1;
			this.txtServerHost.Text = "";
			// 
			// txtHostname
			// 
			this.txtHostname.Location = new System.Drawing.Point(167, 16);
			this.txtHostname.Name = "txtHostname";
			this.txtHostname.ReadOnly = true;
			this.txtHostname.Size = new System.Drawing.Size(256, 20);
			this.txtHostname.TabIndex = 22;
			this.txtHostname.TabStop = false;
			this.txtHostname.Text = "";
			// 
			// lblServerPort
			// 
			this.lblServerPort.Location = new System.Drawing.Point(8, 92);
			this.lblServerPort.Name = "lblServerPort";
			this.lblServerPort.Size = new System.Drawing.Size(136, 16);
			this.lblServerPort.TabIndex = 12;
			this.lblServerPort.Text = "Zabbix server port";
			// 
			// lblServerHost
			// 
			this.lblServerHost.Location = new System.Drawing.Point(8, 68);
			this.lblServerHost.Name = "lblServerHost";
			this.lblServerHost.Size = new System.Drawing.Size(136, 16);
			this.lblServerHost.TabIndex = 11;
			this.lblServerHost.Text = "Zabbix server address";
			// 
			// lblHostname
			// 
			this.lblHostname.Location = new System.Drawing.Point(8, 19);
			this.lblHostname.Name = "lblHostname";
			this.lblHostname.Size = new System.Drawing.Size(88, 16);
			this.lblHostname.TabIndex = 0;
			this.lblHostname.Text = "Hostname:";
			// 
			// General
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 310);
			this.Controls.Add(this.grpBoxGeneral);
			this.HelpButton = true;
			this.Name = "General";
			this.Text = "General Settings";
			this.grpBoxGeneral.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nudTimeOut)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		public void Save()
		{

			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);

			profile.SetValue("General", "Hostname", txtHostname.Text);
			profile.SetValue("General", "FQDN", chkFQDN.Checked.ToString());
			profile.SetValue("General", "ServerHost", txtServerHost.Text);
			profile.SetValue("General", "ServerPort", txtServerPort.Text);
			profile.SetValue("General", "AgentPort", txtAgentPort.Text);
			profile.SetValue("General", "Refresh", txtRefresh.Text);
			profile.SetValue("General", "ActiveChecks", chkActiveChecks.Checked.ToString());
			profile.SetValue("General", "TimeOut", nudTimeOut.Value);
			profile.SetValue("General", "RemoteCommands", chkRemComm.Checked.ToString());
			profile.SetValue("General", "UseQueue", chkUseQueue.Checked.ToString());
			profile.SetValue("General", "QueueLength", txtQueueLength.Text);
		}

		/// <summary>
		/// 
		/// </summary>
		private void ReadValues() 
		{
			RegistryKey key = Microsoft.Win32.Registry.Users.OpenSubKey(RegistryRoot);
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			
			if (key != null) 
			{
				foreach(string val in key.GetValueNames()) 
					{
					//MessageBox.Show(key.GetValue(val).GetType().ToString());
					ht.Add(val, key.GetValue(val));
					}			
				// General
				profile.SetValue("General", "ServerHost", (string)ht["ServerHost"]);
				profile.SetValue("General", "ServerPort", (string)ht["ServerPort"]);

				profile.SetValue("Logging", "DebugLevel", (string)ht["DebugLevel"]);
				//profile.SetValue("Logging", "LogFile", txtLogFile.Text);
				profile.SetValue("Updater", "CustomerGroup", (string)ht["CustomerGroup"]);
				profile.SetValue("Updater", "UpdateService", (string)ht["UpdateServiceLink"]);
				
				
				profile.SetValue("SSH", "SSHUse", (int)ht["UseSSH"]);
				profile.SetValue("SSH", "SSHLocalPort", (string)ht["LocalBoundPort"]);
				profile.SetValue("SSH", "SSHUser", (string)ht["SSHUser"]);
				profile.SetValue("SSH", "SSHKeyPath", (string)ht["SSHPrivateKeyPath"]);

				profile.SetValue("SQL", "MSSQLUse", (int)ht["UseMSSQL"]);
				profile.SetValue("SQL", "MSSQLServer", (string)ht["MSSQLServer"]);
				profile.SetValue("SQL", "MSSQLUsername", (string)ht["MSSQLUsername"]);
				profile.SetValue("SQL", "MSSQLPassword", (string)ht["MSSQLPassword"]);
				profile.SetValue("SQL", "MSSQLDatabase", (string)ht["MSSQLDatabase"]);

				try
				{
					RegistryKey delkey = Microsoft.Win32.Registry.Users.OpenSubKey(".DEFAULT\\Software", true);
					delkey.DeleteSubKeyTree("Zabbix");
					MessageBox.Show("Your configuration settings have been migrated, old registry keys are deleted");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}

			try
			{
				txtHostname.Text = profile.GetValue("General", "Hostname").ToString();
			}
			catch {}

			try 
			{
				chkFQDN.Checked = Convert.ToBoolean(profile.GetValue("General", "FQDN"));
			} 
			catch {}
			try 
			{
				txtServerHost.Text = profile.GetValue("General", "ServerHost").ToString();
			} 
			catch {}
			try 
			{
				txtServerPort.Text = profile.GetValue("General", "ServerPort").ToString();
			} 
			catch {}
			try 
			{
				txtAgentPort.Text = profile.GetValue("General", "AgentPort").ToString();
			} 
			catch {}
			try 
			{
				txtRefresh.Text = profile.GetValue("General", "Refresh").ToString();
			} 
			catch {}
			try 
			{	
				chkActiveChecks.Checked = Convert.ToBoolean(profile.GetValue("General", "ActiveChecks"));
			} 
			catch {}
			try 
			{	
				nudTimeOut.Value = Convert.ToDecimal(profile.GetValue("General", "TimeOut"));
			} 
			catch {}
			try 
			{
				chkRemComm.Checked = Convert.ToBoolean(profile.GetValue("General", "RemoteCommands"));
			} 
			catch {}
			try 
			{
				chkUseQueue.Checked = Convert.ToBoolean(profile.GetValue("General", "UseQueue"));
			} 
			catch {}
			try 
			{
				txtQueueLength.Text = profile.GetValue("General", "QueueLength").ToString();
			} 
			catch {}
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void useFQDN_changed(object sender, System.EventArgs e)
		{
			if (chkFQDN.Checked == true)
                txtHostname.Text = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).HostName;
			else
				txtHostname.Text = System.Net.Dns.GetHostName();
		}
		
	}
}
