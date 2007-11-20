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
 * Copyright ZabbixAgent.NET
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
	/// Summary description for SQL.
	/// </summary>
	public class SQL : System.Windows.Forms.Form, ISettingsHandler
	{
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chkUseMSSQL;
		private System.Windows.Forms.TextBox txtMSSQLDatabase;
		private System.Windows.Forms.TextBox txtMSSQLPassword;
		private System.Windows.Forms.TextBox txtMSSQLUsername;
		private System.Windows.Forms.TextBox txtMSSQLServer;
		/// <summary>
		/// 
		/// </summary>
		public static string PuttyKeyGlobal = ".DEFAULT\\Software\\SimonTatham\\PuTTY\\SshHostKeys";
		/// <summary>
		/// 
		/// </summary>
		public static string PuttyKeyRoot = "Software\\SimonTatham\\PuTTY\\SshHostKeys";
		private Hashtable ht = new Hashtable(20);
		private System.Windows.Forms.Label lblDatabase;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.Label lblServer;
		private System.Windows.Forms.Label label1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 
		/// </summary>
		public SQL()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			getValues();
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chkUseMSSQL = new System.Windows.Forms.CheckBox();
			this.txtMSSQLDatabase = new System.Windows.Forms.TextBox();
			this.txtMSSQLPassword = new System.Windows.Forms.TextBox();
			this.txtMSSQLUsername = new System.Windows.Forms.TextBox();
			this.txtMSSQLServer = new System.Windows.Forms.TextBox();
			this.lblDatabase = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblUsername = new System.Windows.Forms.Label();
			this.lblServer = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.chkUseMSSQL);
			this.groupBox2.Controls.Add(this.txtMSSQLDatabase);
			this.groupBox2.Controls.Add(this.txtMSSQLPassword);
			this.groupBox2.Controls.Add(this.txtMSSQLUsername);
			this.groupBox2.Controls.Add(this.txtMSSQLServer);
			this.groupBox2.Controls.Add(this.lblDatabase);
			this.groupBox2.Controls.Add(this.lblPassword);
			this.groupBox2.Controls.Add(this.lblUsername);
			this.groupBox2.Controls.Add(this.lblServer);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(8, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(560, 152);
			this.groupBox2.TabIndex = 24;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Microsoft SQL Configuration";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(232, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 24);
			this.label1.TabIndex = 16;
			this.label1.Text = "Password are saved in cleartext!";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkUseMSSQL
			// 
			this.chkUseMSSQL.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkUseMSSQL.Location = new System.Drawing.Point(8, 16);
			this.chkUseMSSQL.Name = "chkUseMSSQL";
			this.chkUseMSSQL.TabIndex = 11;
			this.chkUseMSSQL.Text = "Use MSSQL";
			this.chkUseMSSQL.CheckedChanged += new System.EventHandler(this.chkUseMSSQL_CheckedChanged);
			// 
			// txtMSSQLDatabase
			// 
			this.txtMSSQLDatabase.Enabled = false;
			this.txtMSSQLDatabase.Location = new System.Drawing.Point(104, 120);
			this.txtMSSQLDatabase.Name = "txtMSSQLDatabase";
			this.txtMSSQLDatabase.Size = new System.Drawing.Size(120, 20);
			this.txtMSSQLDatabase.TabIndex = 15;
			this.txtMSSQLDatabase.Text = "";
			// 
			// txtMSSQLPassword
			// 
			this.txtMSSQLPassword.Enabled = false;
			this.txtMSSQLPassword.Location = new System.Drawing.Point(104, 96);
			this.txtMSSQLPassword.Name = "txtMSSQLPassword";
			this.txtMSSQLPassword.PasswordChar = '*';
			this.txtMSSQLPassword.Size = new System.Drawing.Size(120, 20);
			this.txtMSSQLPassword.TabIndex = 14;
			this.txtMSSQLPassword.Text = "";
			// 
			// txtMSSQLUsername
			// 
			this.txtMSSQLUsername.Enabled = false;
			this.txtMSSQLUsername.Location = new System.Drawing.Point(104, 72);
			this.txtMSSQLUsername.Name = "txtMSSQLUsername";
			this.txtMSSQLUsername.Size = new System.Drawing.Size(120, 20);
			this.txtMSSQLUsername.TabIndex = 13;
			this.txtMSSQLUsername.Text = "";
			// 
			// txtMSSQLServer
			// 
			this.txtMSSQLServer.Enabled = false;
			this.txtMSSQLServer.Location = new System.Drawing.Point(104, 48);
			this.txtMSSQLServer.Name = "txtMSSQLServer";
			this.txtMSSQLServer.Size = new System.Drawing.Size(120, 20);
			this.txtMSSQLServer.TabIndex = 12;
			this.txtMSSQLServer.Text = "";
			// 
			// lblDatabase
			// 
			this.lblDatabase.Location = new System.Drawing.Point(8, 120);
			this.lblDatabase.Name = "lblDatabase";
			this.lblDatabase.Size = new System.Drawing.Size(64, 16);
			this.lblDatabase.TabIndex = 3;
			this.lblDatabase.Text = "Database";
			// 
			// lblPassword
			// 
			this.lblPassword.Location = new System.Drawing.Point(8, 96);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(72, 16);
			this.lblPassword.TabIndex = 2;
			this.lblPassword.Text = "Password";
			// 
			// lblUsername
			// 
			this.lblUsername.Location = new System.Drawing.Point(8, 72);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(56, 16);
			this.lblUsername.TabIndex = 1;
			this.lblUsername.Text = "Username";
			// 
			// lblServer
			// 
			this.lblServer.Location = new System.Drawing.Point(8, 48);
			this.lblServer.Name = "lblServer";
			this.lblServer.Size = new System.Drawing.Size(40, 16);
			this.lblServer.TabIndex = 0;
			this.lblServer.Text = "Server";
			// 
			// SQL
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 166);
			this.Controls.Add(this.groupBox2);
			this.Name = "SQL";
			this.Text = "SQL Server Configuration";
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		public void Save()
		{
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			profile.SetValue("SQL", "MSSQLUse", chkUseMSSQL.Checked.ToString());
			profile.SetValue("SQL", "MSSQLServer", txtMSSQLServer.Text);
			profile.SetValue("SQL", "MSSQLUsername", txtMSSQLUsername.Text);
			profile.SetValue("SQL", "MSSQLPassword", txtMSSQLPassword.Text);
			profile.SetValue("SQL", "MSSQLDatabase", txtMSSQLDatabase.Text);

			//MessageBox.Show("SQL Settings saved");
		}

		/// <summary>
		/// 
		/// </summary>
		private void getValues() 
		{
			try
			{
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			chkUseMSSQL.Checked = Convert.ToBoolean(profile.GetValue("SQL", "MSSQLUse").ToString());
			txtMSSQLServer.Text = profile.GetValue("SQL", "MSSQLServer").ToString();
			txtMSSQLUsername.Text = profile.GetValue("SQL", "MSSQLUsername").ToString();
			txtMSSQLPassword.Text = profile.GetValue("SQL", "MSSQLPassword").ToString();
			txtMSSQLDatabase.Text = profile.GetValue("SQL", "MSSQLDatabase").ToString();
			}
			catch
			{
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chkUseMSSQL_CheckedChanged(object sender, System.EventArgs e)
		{
			if (!chkUseMSSQL.Checked) 
			{
				txtMSSQLDatabase.Enabled = false;
				txtMSSQLPassword.Enabled = false;
				txtMSSQLServer.Enabled = false;
				txtMSSQLUsername.Enabled = false;
			} 
			else 
			{
				txtMSSQLDatabase.Enabled = true;
				txtMSSQLPassword.Enabled = true;
				txtMSSQLServer.Enabled = true;
				txtMSSQLUsername.Enabled = true;
			}
		}
	}
}
