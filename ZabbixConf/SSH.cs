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
	/// Summary description for SSH.
	/// </summary>
	public class SSH : System.Windows.Forms.Form, ISettingsHandler
	{
		internal System.Windows.Forms.GroupBox GroupBox1;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.TextBox txtHostKey;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox txtSSHUser;
		internal System.Windows.Forms.TextBox txtKeyPath;
		internal System.Windows.Forms.Button btnFindKey;
		internal System.Windows.Forms.Label lblForceConnection;
		internal System.Windows.Forms.TextBox txtLocalPort;
		internal System.Windows.Forms.CheckBox chkUseSSH;
		/// <summary>
		/// 
		/// </summary>
		public static string PuttyKeyGlobal = ".DEFAULT\\Software\\SimonTatham\\PuTTY\\SshHostKeys";
		/// <summary>
		/// 
		/// </summary>
		public static string PuttyKeyRoot = "Software\\SimonTatham\\PuTTY\\SshHostKeys";
		private Hashtable ht = new Hashtable(20);
		private System.Windows.Forms.Label lblServerKey;
		private System.Windows.Forms.Label lblUser;
		internal System.Windows.Forms.Label lblPrivateKey;
		internal System.Windows.Forms.Label lblLocalPort;
		private System.Windows.Forms.OpenFileDialog OpenFileFindSSHKey;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtSSHServer;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtSSHServerPort;
		private System.Windows.Forms.TextBox txtSSHServerPassword;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RadioButton rbChkUsePassword;
		private System.Windows.Forms.RadioButton rbChkUsePrivateKey;
		
		private Process p1 = null;
		private System.Windows.Forms.Label label5;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 
		/// </summary>
		public SSH()
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
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtSSHServerPassword = new System.Windows.Forms.TextBox();
			this.rbChkUsePassword = new System.Windows.Forms.RadioButton();
			this.rbChkUsePrivateKey = new System.Windows.Forms.RadioButton();
			this.txtSSHServerPort = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtSSHServer = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnTest = new System.Windows.Forms.Button();
			this.txtHostKey = new System.Windows.Forms.TextBox();
			this.lblServerKey = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.txtSSHUser = new System.Windows.Forms.TextBox();
			this.lblUser = new System.Windows.Forms.Label();
			this.lblPrivateKey = new System.Windows.Forms.Label();
			this.txtKeyPath = new System.Windows.Forms.TextBox();
			this.btnFindKey = new System.Windows.Forms.Button();
			this.lblForceConnection = new System.Windows.Forms.Label();
			this.lblLocalPort = new System.Windows.Forms.Label();
			this.txtLocalPort = new System.Windows.Forms.TextBox();
			this.chkUseSSH = new System.Windows.Forms.CheckBox();
			this.OpenFileFindSSHKey = new System.Windows.Forms.OpenFileDialog();
			this.GroupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// GroupBox1
			// 
			this.GroupBox1.Controls.Add(this.label5);
			this.GroupBox1.Controls.Add(this.label4);
			this.GroupBox1.Controls.Add(this.label3);
			this.GroupBox1.Controls.Add(this.txtSSHServerPassword);
			this.GroupBox1.Controls.Add(this.rbChkUsePassword);
			this.GroupBox1.Controls.Add(this.rbChkUsePrivateKey);
			this.GroupBox1.Controls.Add(this.txtSSHServerPort);
			this.GroupBox1.Controls.Add(this.label2);
			this.GroupBox1.Controls.Add(this.txtSSHServer);
			this.GroupBox1.Controls.Add(this.label1);
			this.GroupBox1.Controls.Add(this.btnTest);
			this.GroupBox1.Controls.Add(this.txtHostKey);
			this.GroupBox1.Controls.Add(this.lblServerKey);
			this.GroupBox1.Controls.Add(this.label18);
			this.GroupBox1.Controls.Add(this.txtSSHUser);
			this.GroupBox1.Controls.Add(this.lblUser);
			this.GroupBox1.Controls.Add(this.lblPrivateKey);
			this.GroupBox1.Controls.Add(this.txtKeyPath);
			this.GroupBox1.Controls.Add(this.btnFindKey);
			this.GroupBox1.Controls.Add(this.lblForceConnection);
			this.GroupBox1.Controls.Add(this.lblLocalPort);
			this.GroupBox1.Controls.Add(this.txtLocalPort);
			this.GroupBox1.Controls.Add(this.chkUseSSH);
			this.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GroupBox1.Location = new System.Drawing.Point(8, 8);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(560, 312);
			this.GroupBox1.TabIndex = 11;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "SSH Configuration";
			// 
			// label5
			// 
			this.label5.ForeColor = System.Drawing.Color.Red;
			this.label5.Location = new System.Drawing.Point(312, 168);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(176, 23);
			this.label5.TabIndex = 22;
			this.label5.Text = "Password are stored in cleartext.";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 168);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 24);
			this.label4.TabIndex = 21;
			this.label4.Text = "Authentication:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 192);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.TabIndex = 20;
			this.label3.Text = "SSH Password";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtSSHServerPassword
			// 
			this.txtSSHServerPassword.Enabled = false;
			this.txtSSHServerPassword.Location = new System.Drawing.Point(104, 192);
			this.txtSSHServerPassword.Name = "txtSSHServerPassword";
			this.txtSSHServerPassword.PasswordChar = '*';
			this.txtSSHServerPassword.TabIndex = 7;
			this.txtSSHServerPassword.Text = "";
			// 
			// rbChkUsePassword
			// 
			this.rbChkUsePassword.Enabled = false;
			this.rbChkUsePassword.Location = new System.Drawing.Point(216, 168);
			this.rbChkUsePassword.Name = "rbChkUsePassword";
			this.rbChkUsePassword.Size = new System.Drawing.Size(96, 24);
			this.rbChkUsePassword.TabIndex = 6;
			this.rbChkUsePassword.Tag = "Test";
			this.rbChkUsePassword.Text = "Use Password";
			// 
			// rbChkUsePrivateKey
			// 
			this.rbChkUsePrivateKey.Checked = true;
			this.rbChkUsePrivateKey.Enabled = false;
			this.rbChkUsePrivateKey.Location = new System.Drawing.Point(104, 168);
			this.rbChkUsePrivateKey.Name = "rbChkUsePrivateKey";
			this.rbChkUsePrivateKey.TabIndex = 5;
			this.rbChkUsePrivateKey.TabStop = true;
			this.rbChkUsePrivateKey.Tag = "Test";
			this.rbChkUsePrivateKey.Text = "Use private key";
			this.rbChkUsePrivateKey.CheckedChanged += new System.EventHandler(this.rbChkUsePrivateKey_CheckedChanged);
			// 
			// txtSSHServerPort
			// 
			this.txtSSHServerPort.Enabled = false;
			this.txtSSHServerPort.Location = new System.Drawing.Point(104, 96);
			this.txtSSHServerPort.Name = "txtSSHServerPort";
			this.txtSSHServerPort.Size = new System.Drawing.Size(48, 20);
			this.txtSSHServerPort.TabIndex = 2;
			this.txtSSHServerPort.Text = "10051";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 16);
			this.label2.TabIndex = 15;
			this.label2.Text = "Server bound port:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtSSHServer
			// 
			this.txtSSHServer.Enabled = false;
			this.txtSSHServer.Location = new System.Drawing.Point(104, 72);
			this.txtSSHServer.Name = "txtSSHServer";
			this.txtSSHServer.TabIndex = 1;
			this.txtSSHServer.Text = "";
			this.txtSSHServer.TextChanged += new System.EventHandler(this.txtSSHServer_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "SSH Server:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnTest
			// 
			this.btnTest.Enabled = false;
			this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnTest.Location = new System.Drawing.Point(104, 248);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(240, 23);
			this.btnTest.TabIndex = 11;
			this.btnTest.Text = "Test Connection and accept host key";
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// txtHostKey
			// 
			this.txtHostKey.Location = new System.Drawing.Point(104, 272);
			this.txtHostKey.Name = "txtHostKey";
			this.txtHostKey.ReadOnly = true;
			this.txtHostKey.Size = new System.Drawing.Size(240, 20);
			this.txtHostKey.TabIndex = 12;
			this.txtHostKey.TabStop = false;
			this.txtHostKey.Text = "";
			// 
			// lblServerKey
			// 
			this.lblServerKey.Location = new System.Drawing.Point(8, 272);
			this.lblServerKey.Name = "lblServerKey";
			this.lblServerKey.Size = new System.Drawing.Size(88, 32);
			this.lblServerKey.TabIndex = 11;
			this.lblServerKey.Text = "SSH Server Public Key";
			this.lblServerKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(8, 16);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(344, 32);
			this.label18.TabIndex = 10;
			this.label18.Text = "Creates a SSH port forward to monitoring server. Need a private key and a user on" +
				" Zabbix server.";
			// 
			// txtSSHUser
			// 
			this.txtSSHUser.Enabled = false;
			this.txtSSHUser.Location = new System.Drawing.Point(104, 144);
			this.txtSSHUser.Name = "txtSSHUser";
			this.txtSSHUser.Size = new System.Drawing.Size(96, 20);
			this.txtSSHUser.TabIndex = 4;
			this.txtSSHUser.Text = "";
			// 
			// lblUser
			// 
			this.lblUser.Location = new System.Drawing.Point(8, 144);
			this.lblUser.Name = "lblUser";
			this.lblUser.Size = new System.Drawing.Size(88, 16);
			this.lblUser.TabIndex = 8;
			this.lblUser.Text = "SSH User:";
			this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPrivateKey
			// 
			this.lblPrivateKey.Location = new System.Drawing.Point(8, 216);
			this.lblPrivateKey.Name = "lblPrivateKey";
			this.lblPrivateKey.Size = new System.Drawing.Size(96, 16);
			this.lblPrivateKey.TabIndex = 7;
			this.lblPrivateKey.Text = "SSH Private Key:";
			this.lblPrivateKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtKeyPath
			// 
			this.txtKeyPath.Enabled = false;
			this.txtKeyPath.Location = new System.Drawing.Point(104, 216);
			this.txtKeyPath.Name = "txtKeyPath";
			this.txtKeyPath.Size = new System.Drawing.Size(176, 20);
			this.txtKeyPath.TabIndex = 9;
			this.txtKeyPath.Text = "";
			// 
			// btnFindKey
			// 
			this.btnFindKey.Enabled = false;
			this.btnFindKey.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFindKey.Location = new System.Drawing.Point(288, 216);
			this.btnFindKey.Name = "btnFindKey";
			this.btnFindKey.Size = new System.Drawing.Size(56, 24);
			this.btnFindKey.TabIndex = 10;
			this.btnFindKey.Text = "Find key";
			this.btnFindKey.Visible = false;
			this.btnFindKey.Click += new System.EventHandler(this.btnFindKey_Click);
			// 
			// lblForceConnection
			// 
			this.lblForceConnection.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblForceConnection.Location = new System.Drawing.Point(88, 48);
			this.lblForceConnection.Name = "lblForceConnection";
			this.lblForceConnection.Size = new System.Drawing.Size(264, 24);
			this.lblForceConnection.TabIndex = 3;
			this.lblForceConnection.Text = "Notice: Will connect though SSH server before connection to Zabbix server. ";
			this.lblForceConnection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblLocalPort
			// 
			this.lblLocalPort.Location = new System.Drawing.Point(8, 120);
			this.lblLocalPort.Name = "lblLocalPort";
			this.lblLocalPort.Size = new System.Drawing.Size(96, 16);
			this.lblLocalPort.TabIndex = 2;
			this.lblLocalPort.Text = "Local bound port:";
			this.lblLocalPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtLocalPort
			// 
			this.txtLocalPort.Enabled = false;
			this.txtLocalPort.Location = new System.Drawing.Point(104, 120);
			this.txtLocalPort.Name = "txtLocalPort";
			this.txtLocalPort.Size = new System.Drawing.Size(48, 20);
			this.txtLocalPort.TabIndex = 3;
			this.txtLocalPort.Text = "10051";
			// 
			// chkUseSSH
			// 
			this.chkUseSSH.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkUseSSH.Location = new System.Drawing.Point(8, 48);
			this.chkUseSSH.Name = "chkUseSSH";
			this.chkUseSSH.Size = new System.Drawing.Size(80, 24);
			this.chkUseSSH.TabIndex = 5;
			this.chkUseSSH.Text = "Use SSH";
			this.chkUseSSH.CheckedChanged += new System.EventHandler(this.chkUseSSH_CheckedChanged);
			// 
			// OpenFileFindSSHKey
			// 
			this.OpenFileFindSSHKey.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileFindSSHKey_FileOk);
			// 
			// SSH
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 326);
			this.Controls.Add(this.GroupBox1);
			this.Name = "SSH";
			this.Text = "SSH Configuration";
			this.GroupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		public void Save()
		{
			try 
			{
				Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			
				// Required
			
				profile.SetValue("SSH", "Use", chkUseSSH.Checked.ToString());
				profile.SetValue("SSH", "Server", txtSSHServer.Text);
				profile.SetValue("SSH", "LocalPort", txtLocalPort.Text);
				profile.SetValue("SSH", "ServerPort", txtSSHServerPort.Text);
//				MessageBox.Show(txtSSHUser.Text);
				profile.SetValue("SSH", "User", txtSSHUser.Text);

				// Either key or password 
				if (rbChkUsePrivateKey.Checked) 
				{
					profile.SetValue("SSH", "UsePrivateKey", "True");
					profile.SetValue("SSH", "KeyPath", txtKeyPath.Text);
				} 
				else 
				{
					profile.SetValue("SSH", "UsePrivateKey", "False");
					profile.SetValue("SSH", "Password", txtSSHServerPassword.Text);
				}
			} 
			catch (Exception ex) 
			{
				MessageBox.Show("SSH: " + ex.Message);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void getValues() 
		{
			try
			{
				Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
				chkUseSSH.Checked = Convert.ToBoolean(profile.GetValue("SSH", "Use"));

				
				if (chkUseSSH.Checked) 
				{
					if (profile.HasEntry("SSH", "LocalPort"))
						txtLocalPort.Text = (string)profile.GetValue("SSH", "LocalPort");
					
					if (profile.HasEntry("SSH", "ServerPort"))
						txtSSHServerPort.Text = profile.GetValue("SSH", "ServerPort").ToString();
					
					if (profile.HasEntry("SSH", "User"))
						txtSSHUser.Text = profile.GetValue("SSH", "User").ToString();
					
					if (profile.HasEntry("SSH", "Password")) 
						txtSSHServerPassword.Text = profile.GetValue("SSH", "Password").ToString();
					
					if (profile.HasEntry("SSH", "Server"))
						txtSSHServer.Text = (string)profile.GetValue("SSH", "Server");
					
					// Key or Password?
					if (profile.HasEntry("SSH", "UsePrivateKey")) 
					{
						if (Convert.ToBoolean(profile.GetValue("SSH", "UsePrivateKey"))) 
						{
							//MessageBox.Show("fds");
							rbChkUsePrivateKey.Checked = true;
							if (profile.HasEntry("SSH", "KeyPath"))
								txtKeyPath.Text = profile.GetValue("SSH", "KeyPath").ToString();
						}
						else
						{
							rbChkUsePassword.Checked = true;
							if (profile.HasEntry("SSH", "Password"))
								txtSSHServerPassword.Text = profile.GetValue("SSH", "Password").ToString();
						}
					}
				}
			} catch (Exception ex) { MessageBox.Show(ex.Message);}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chkUseSSH_CheckedChanged(object sender, System.EventArgs e)
		{
			if (!chkUseSSH.Checked) 
			{
				txtSSHUser.Enabled = false;
				txtKeyPath.Enabled = false;
				txtLocalPort.Enabled = false;
				btnFindKey.Enabled = false;
				btnTest.Enabled = false;
				txtSSHServer.Enabled = false;
				txtSSHServerPort.Enabled = false;
				txtSSHServerPassword.Enabled = false;
				rbChkUsePassword.Enabled = false;
				rbChkUsePrivateKey.Enabled = false;
			} 
			else 
			{
				txtSSHServer.Enabled = true;
				txtSSHServerPort.Enabled = true;
				txtSSHServerPassword.Enabled = false;
				txtSSHUser.Enabled = true;
				txtKeyPath.Enabled = true;
				txtLocalPort.Enabled = true;
				btnFindKey.Enabled = true;
				btnTest.Enabled = true;
				rbChkUsePassword.Enabled = true;
				rbChkUsePrivateKey.Enabled = true;
			}
		}

		private void btnFindKey_Click(object sender, System.EventArgs e)
		{
			OpenFileFindSSHKey.Filter ="Private SSH Key (*.ppk)|*.ppk|All Files (*.*)|*.*";
			DialogResult res = OpenFileFindSSHKey.ShowDialog();

			if (res.ToString().Equals("OK")) 
			{
				txtKeyPath.Text = OpenFileFindSSHKey.FileName;
			}
		}

		private void btnTest_Click(object sender, System.EventArgs e)
		{
			if (!File.Exists(Environment.GetEnvironmentVariable("SystemRoot") + "\\plink.exe"))
				SavePlinkToDisk();
			// Start Zabbix
			p1 = new Process();
			p1.StartInfo.UseShellExecute = true;
			p1.EnableRaisingEvents = true;
			p1.StartInfo.Arguments = "-l " + txtSSHUser.Text + " -L " + txtLocalPort.Text +":localhost:" + txtSSHServerPort.Text +" -i " + txtKeyPath.Text + " " + txtSSHServer.Text;
			p1.StartInfo.FileName = Environment.GetEnvironmentVariable("SystemRoot") + "\\plink.exe";
			p1.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
			p1.Exited += new EventHandler(p1_Exited);
			p1.Start();
		}

		private void FindHostKey() 
		{
			RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(PuttyKeyRoot);
			//RegistryKey key = Registry.Users.OpenSubKey(PuttyKeyRoot);

			// Find newly generated key.
			string keyname = "";
			string keyvalue = "";
			
			if (key != null) 
			{
				foreach(string val in key.GetValueNames()) 
				{
					if (val.EndsWith(txtSSHServer.Text)) 
					{
						keyname = val;
						keyvalue = (string)key.GetValue(val);
						break;
					}
				}	

				if (!keyname.Equals("") && !txtSSHServer.Text.Equals("")) 
				{
					
					RegistryKey globalkey = Microsoft.Win32.Registry.Users.OpenSubKey(PuttyKeyGlobal,true);

					if (globalkey == null) 
						globalkey = Microsoft.Win32.Registry.Users.CreateSubKey(PuttyKeyGlobal);
					
					globalkey.SetValue(keyname, keyvalue);
					txtHostKey.Text = keyvalue;
				} 
				else 
				{
					txtHostKey.Text = "Not found";
				}
			}
		}

		private void rbChkUsePrivateKey_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rbChkUsePrivateKey.Checked) 
			{
				txtSSHServerPassword.Enabled = false;
				txtKeyPath.Enabled = true;
			} 
			else 
			{
				txtSSHServerPassword.Enabled = true;
				txtKeyPath.Enabled = false;
			}
		}

		private void txtSSHServer_TextChanged(object sender, System.EventArgs e)
		{
			FindHostKey();
		}
	
		private void SavePlinkToDisk() 
		{
			try 
			{
				Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixConf.SubSystem.plink.exe");

				if (stream != null) 
				{
					FileStream write = new FileStream(Environment.GetEnvironmentVariable("SystemRoot") + "\\plink.exe",FileMode.Create);

					int Length = 256;
					Byte [] buffer = new Byte[Length];


					int bytesRead = stream.Read(buffer,0,Length);
					// write the required bytes
					while( bytesRead > 0 )
					{
						write.Write(buffer,0,bytesRead);
						bytesRead = stream.Read(buffer,0,Length);
					}
					stream.Close();
					write.Close();
				}
			} 
			catch (IOException ex)
			{
				
			}
		}

		private void p1_Exited(object sender, System.EventArgs e)
		{
			if (p1.HasExited) 
			{
				FindHostKey();
			}
		}

		private void OpenFileFindSSHKey_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
		
		}
	}
}