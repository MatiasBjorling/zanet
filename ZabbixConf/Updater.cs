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
using System.Xml;
using AMS.Profile;

namespace ZabbixConf
{
	/// <summary>
	/// Summary description for Updater.
	/// </summary>
	public class Updater : System.Windows.Forms.Form, ISettingsHandler
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkEnableUpdater;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 
		/// </summary>
		public Updater()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			this.chkEnableUpdater = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkEnableUpdater);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(560, 232);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Updater";
			// 
			// chkEnableUpdater
			// 
			this.chkEnableUpdater.Location = new System.Drawing.Point(136, 24);
			this.chkEnableUpdater.Name = "chkEnableUpdater";
			this.chkEnableUpdater.Size = new System.Drawing.Size(16, 16);
			this.chkEnableUpdater.TabIndex = 32;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 31;
			this.label2.Text = "Enable Updater";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(16, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(536, 32);
			this.label1.TabIndex = 30;
			this.label1.Text = "Security Risk!: Be sure to only use this with SSH and Passive mode disabled if us" +
				"ed over insecure networks!";
			// 
			// Updater
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 246);
			this.Controls.Add(this.groupBox1);
			this.Name = "Updater";
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
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			profile.SetValue("Updater", "EnableUpdateService", chkEnableUpdater.Checked.ToString());
		}

		/// <summary>
		/// 
		/// </summary>
		private void ReadSettings() 
		{
			try
			{
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			chkEnableUpdater.Checked = Convert.ToBoolean(profile.GetValue("Updater", "EnableUpdateService"));
			}
			catch
			{
			}

		}
}
}


