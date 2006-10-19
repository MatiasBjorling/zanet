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
		private System.Windows.Forms.TextBox txtCustomerGroup;
		private System.Windows.Forms.TextBox txtUpdateService;
		internal System.Windows.Forms.Label lblCustGroup;
		private System.Windows.Forms.Label lblUpdService;
		private System.Windows.Forms.Label label1;
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
			this.txtUpdateService = new System.Windows.Forms.TextBox();
			this.txtCustomerGroup = new System.Windows.Forms.TextBox();
			this.lblCustGroup = new System.Windows.Forms.Label();
			this.lblUpdService = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtUpdateService);
			this.groupBox1.Controls.Add(this.txtCustomerGroup);
			this.groupBox1.Controls.Add(this.lblCustGroup);
			this.groupBox1.Controls.Add(this.lblUpdService);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(560, 152);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Updater";
			// 
			// txtUpdateService
			// 
			this.txtUpdateService.Location = new System.Drawing.Point(135, 54);
			this.txtUpdateService.Name = "txtUpdateService";
			this.txtUpdateService.Size = new System.Drawing.Size(256, 20);
			this.txtUpdateService.TabIndex = 29;
			this.txtUpdateService.Text = "";
			// 
			// txtCustomerGroup
			// 
			this.txtCustomerGroup.Location = new System.Drawing.Point(135, 25);
			this.txtCustomerGroup.Name = "txtCustomerGroup";
			this.txtCustomerGroup.Size = new System.Drawing.Size(152, 20);
			this.txtCustomerGroup.TabIndex = 26;
			this.txtCustomerGroup.Text = "";
			// 
			// lblCustGroup
			// 
			this.lblCustGroup.Location = new System.Drawing.Point(14, 30);
			this.lblCustGroup.Name = "lblCustGroup";
			this.lblCustGroup.Size = new System.Drawing.Size(96, 16);
			this.lblCustGroup.TabIndex = 25;
			this.lblCustGroup.Text = "Customer Group:";
			// 
			// lblUpdService
			// 
			this.lblUpdService.Location = new System.Drawing.Point(14, 59);
			this.lblUpdService.Name = "lblUpdService";
			this.lblUpdService.Size = new System.Drawing.Size(88, 16);
			this.lblUpdService.TabIndex = 27;
			this.lblUpdService.Text = "Update Service";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(536, 64);
			this.label1.TabIndex = 30;
			this.label1.Text = @"The way the update service work is by using a web service that receive Customer group and agent version number. Those will be sendt to the Update service link and it will answer back with link to a new agent binary or OK if agent is the correct version number. At the moment there is no official implementation of this, but a WSDL can be given on request for implementing.";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// Updater
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 166);
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
			profile.SetValue("Updater", "CustomerGroup", txtCustomerGroup.Text);
			profile.SetValue("Updater", "UpdateService", txtUpdateService.Text);
		}

		/// <summary>
		/// 
		/// </summary>
		private void ReadSettings() 
		{
			try
			{
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			txtCustomerGroup.Text = profile.GetValue("Updater", "CustomerGroup").ToString();
			txtUpdateService.Text = profile.GetValue("Updater", "UpdateService").ToString();
			}
			catch
			{
			}

		}

		private void label1_Click(object sender, System.EventArgs e)
		{
		
		}

	}

}
