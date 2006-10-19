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
	/// Summary description for Logging.
	/// </summary>
	public class Logging : System.Windows.Forms.Form, ISettingsHandler
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cmbDebugLevel;
		private System.Windows.Forms.TextBox txtLogFile;
		private System.Windows.Forms.FolderBrowserDialog fbd;
		private System.Windows.Forms.Button btnLogFile;
		private System.Windows.Forms.Label lblLogFile;
		private System.Windows.Forms.Label lblDebugLevel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 
		/// </summary>
		public Logging()
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
			this.btnLogFile = new System.Windows.Forms.Button();
			this.txtLogFile = new System.Windows.Forms.TextBox();
			this.lblLogFile = new System.Windows.Forms.Label();
			this.cmbDebugLevel = new System.Windows.Forms.ComboBox();
			this.lblDebugLevel = new System.Windows.Forms.Label();
			this.fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnLogFile);
			this.groupBox1.Controls.Add(this.txtLogFile);
			this.groupBox1.Controls.Add(this.lblLogFile);
			this.groupBox1.Controls.Add(this.cmbDebugLevel);
			this.groupBox1.Controls.Add(this.lblDebugLevel);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(560, 152);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Logging ( Not implemented yet. Logfile is called agent.log and are in the workdir" +
				". )";
			// 
			// btnLogFile
			// 
			this.btnLogFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnLogFile.Location = new System.Drawing.Point(386, 53);
			this.btnLogFile.Name = "btnLogFile";
			this.btnLogFile.Size = new System.Drawing.Size(26, 19);
			this.btnLogFile.TabIndex = 27;
			this.btnLogFile.Text = "...";
			this.btnLogFile.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtLogFile
			// 
			this.txtLogFile.Location = new System.Drawing.Point(135, 52);
			this.txtLogFile.Name = "txtLogFile";
			this.txtLogFile.Size = new System.Drawing.Size(248, 20);
			this.txtLogFile.TabIndex = 26;
			this.txtLogFile.Text = "";
			// 
			// lblLogFile
			// 
			this.lblLogFile.Location = new System.Drawing.Point(8, 55);
			this.lblLogFile.Name = "lblLogFile";
			this.lblLogFile.Size = new System.Drawing.Size(100, 16);
			this.lblLogFile.TabIndex = 25;
			this.lblLogFile.Text = "Logfile";
			// 
			// cmbDebugLevel
			// 
			this.cmbDebugLevel.Items.AddRange(new object[] {
															   "DEBUG",
															   "INFO",
															   "ERROR",
															   "OFF"});
			this.cmbDebugLevel.Location = new System.Drawing.Point(136, 24);
			this.cmbDebugLevel.Name = "cmbDebugLevel";
			this.cmbDebugLevel.Size = new System.Drawing.Size(121, 21);
			this.cmbDebugLevel.TabIndex = 24;
			// 
			// lblDebugLevel
			// 
			this.lblDebugLevel.Location = new System.Drawing.Point(8, 32);
			this.lblDebugLevel.Name = "lblDebugLevel";
			this.lblDebugLevel.Size = new System.Drawing.Size(80, 16);
			this.lblDebugLevel.TabIndex = 23;
			this.lblDebugLevel.Text = "Debug Level";
			// 
			// Logging
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 166);
			this.Controls.Add(this.groupBox1);
			this.Name = "Logging";
			this.Text = "Logging";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, System.EventArgs e)
		{
		{ 
			if(this.fbd.ShowDialog()== DialogResult.OK) 
			{ 
				this.txtLogFile.Text = this.fbd.SelectedPath + "\\zabbix_agent.log"; 
			} 
		} 
		}
		
		/// <summary>
		/// Save the values
		/// </summary>
		public void Save()
		{
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			//profile.SetValue("Logging", "DebugLevel", cmbDebugLevel.SelectedItem.ToString());
			//profile.SetValue("Logging", "LogFile", txtLogFile.Text);
		}

		private void ReadSettings() 
		{
			try
			{
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			cmbDebugLevel.SelectedItem = profile.GetValue("Logging", "DebugLevel");
			txtLogFile.Text = profile.GetValue("Logging", "LogFile").ToString();
			}
			catch
			{
			}

		}

	}
}
