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


namespace ZabbixConf
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>

	public class ZabbixAgentConf : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.OpenFileDialog OpenFileFindSSHKey;
		internal System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Label label17;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// 
		/// </summary>
		public static string RegistryRoot = ".DEFAULT\\Software\\Zabbix\\MonitoringAgent\\v1";
		public const string ConfigFile = "ZabbixAgent.xml";
		/// <summary>
		/// 
		/// </summary>
		public static string PuttyKeyGlobal = ".DEFAULT\\Software\\SimonTatham\\PuTTY\\SshHostKeys";
		/// <summary>
		/// 
		/// </summary>
		public static string PuttyKeyRoot = "Software\\SimonTatham\\PuTTY\\SshHostKeys";
		private Hashtable ht = new Hashtable(20);
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GLabel label2;
		//private Process p1 = null;
		
		/// <summary>
		/// 
		/// </summary>
		public ZabbixAgentConf()
		{
			InitializeComponent();

			//getValues();
			AssemblyInfo ainfo = new AssemblyInfo(this.GetType());
			string version = ainfo.Version;
			this.Text = this.Text + " - " + ainfo.Version;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ZabbixAgentConf));
			this.OpenFileFindSSHKey = new System.Windows.Forms.OpenFileDialog();
			this.btnApply = new System.Windows.Forms.Button();
			this.label17 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.GLabel();
			this.SuspendLayout();
			// 
			// btnApply
			// 
			this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnApply.Location = new System.Drawing.Point(680, 392);
			this.btnApply.Name = "btnApply";
			this.btnApply.TabIndex = 15;
			this.btnApply.Text = "Apply";
			this.btnApply.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// label17
			// 
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label17.ForeColor = System.Drawing.Color.RoyalBlue;
			this.label17.Location = new System.Drawing.Point(8, 392);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(224, 16);
			this.label17.TabIndex = 15;
			this.label17.Text = "Debug information is found in C:\\agent.log";
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(176, 32);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(590, 352);
			this.panel1.TabIndex = 17;
			// 
			// treeView1
			// 
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(9, 8);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(160, 376);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(592, 392);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 18;
			this.btnCancel.Text = "Cancel";
			// 
			// label2
			// 
			this.label2.BeginColor = System.Drawing.Color.Red;
			this.label2.EndColor = System.Drawing.SystemColors.Control;
			this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.label2.Location = new System.Drawing.Point(176, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(584, 23);
			this.label2.TabIndex = 0;
			// 
			// ZabbixAgentConf
			// 
			this.AcceptButton = this.btnApply;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(770, 424);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HelpButton = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ZabbixAgentConf";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ZabbixAgent.NET Configuration";
			this.ResumeLayout(false);

		}
		#endregion
		#region Fields
		Form  _activeForm;
		#endregion Fields

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.EnableVisualStyles();
			Application.DoEvents();
			ZabbixAgentConf form = new ZabbixAgentConf();
			form.LoadFromXml("settings.xml");
			Application.Run(form);

		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		public void LoadFromXml(string filename)
			{
				treeView1.BeginUpdate();
				try
				{
					XmlDocument xmlDocument = new XmlDocument();

					xmlDocument.Load(filename);

					foreach (XmlNode xmlNode in xmlDocument.SelectNodes("//Settings/Category"))
						RecursiveLoadFromXml(xmlNode, null);
				}
				catch
				{
				}

				treeView1.ExpandAll();
				treeView1.EndUpdate();
			}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xmlNode"></param>
		/// <param name="parentTreeNode"></param>
		private void RecursiveLoadFromXml(XmlNode xmlNode, TreeNode parentTreeNode)
		{
			TreeNode treeNode = new TreeNode();

			treeNode.Text = xmlNode.Attributes["Name"].Value;
			treeNode.Tag = xmlNode.Attributes["Form"].Value;

			if (parentTreeNode == null)
			{
				treeView1.Nodes.Add(treeNode);
			}
			else
			{
				parentTreeNode.Nodes.Add(treeNode);
			}

			foreach (XmlNode childXmlNode in xmlNode.SelectNodes("Category"))
				RecursiveLoadFromXml(childXmlNode, treeNode);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode selectedTreeNode = e.Node;

			if(selectedTreeNode.Tag is string)
			{
				Type type = Type.GetType((string)selectedTreeNode.Tag);

				if (type != null)
				{
					if(type.IsSubclassOf(typeof(Form)))
					{
						Form form = (Form)Activator.CreateInstance(type);

						form.FormBorderStyle = FormBorderStyle.None;
						form.TopLevel = false;
						form.Parent = panel1;
						form.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

						// save the form back to the node so that we can reuse this instance
						// if the node becomes selected again
						selectedTreeNode.Tag = form;

						SetActiveForm(form);
					}
				}
			}
			else if(selectedTreeNode.Tag is Form)
			{
				Form form = (Form)selectedTreeNode.Tag;

				if (form != _activeForm)
					SetActiveForm(form);
					form.HelpButton = true;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="form"></param>
		private void SetActiveForm(Form form)
		{
			if (_activeForm != null)
				_activeForm.Hide();

			_activeForm = form;
			_activeForm.Show();

			label2.Text = _activeForm.Text;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			foreach (TreeNode treeNode in treeView1.Nodes)
				SaveRecursive(treeNode);

		} 

		/// <summary>
		/// 
		/// </summary>
		/// <param name="treeNode"></param>
		private void SaveRecursive(TreeNode treeNode)
		{
			ISettingsHandler settingsHandler = treeNode.Tag as ISettingsHandler;
 
			if (settingsHandler != null)
				settingsHandler.Save();
 
			foreach (TreeNode childTreeNode in treeNode.Nodes)
				SaveRecursive(childTreeNode);
		}




		}
//		private void btnApply_Click(object sender, System.EventArgs e)
//		{
//			SaveValues();
//			MessageBox.Show("Saved. Restart ZabbixAgent.NET service for enabling the changes.");
//		}
//
//
//		private void getValues() 
//		{
//			RegistryKey key = Registry.Users.OpenSubKey(RegistryRoot);
//
//			if (key != null) 
//			{
//				foreach(string val in key.GetValueNames()) 
//				{
//					//MessageBox.Show(key.GetValue(val).GetType().ToString());
//					ht.Add(val, key.GetValue(val));
//				}			
//
//				// General
//				txtServerHost.Text = (string)ht["ServerHost"];
//				txtRemotePort.Text = (string)ht["ServerPort"];
//				//cmbDebugLevel.SelectedValue = (string)ht["DebugLevel"];
//				txtCustomerGroup.Text = (string)ht["CustomerGroup"];
//				txtUpdateService.Text = (string)ht["UpdateServiceLink"];
//
//				// SSH
//				txtLocalPort.Text = (string)ht["LocalBoundPort"];
//				txtSSHUser.Text = (string)ht["SSHUser"];
//				txtKeyPath.Text = (string)ht["SSHPrivateKeyPath"];
//
//				// MSSQL
//				txtMSSQLServer.Text = (string)ht["MSSQLServer"];
//				txtMSSQLUsername.Text = (string)ht["MSSQLUsername"];
//				txtMSSQLPassword.Text = (string)ht["MSSQLPassword"];
//				txtMSSQLDatabase.Text = (string)ht["MSSQLDatabase"];
//
//				if ((int)ht["UseSSH"] == 1) 
//					chkUseSSH.Checked = true;
//
//				if ((int)ht["UseMSSQL"] == 1)
//					chkUseMSSQL.Checked = true;
//
//				
//			}
//			txtHostname.Text = System.Net.Dns.GetHostByName("LocalHost").HostName;
//	}
//
//		private void SaveValues() 
//		{
//			RegistryKey key = Registry.Users.OpenSubKey(RegistryRoot,true);
//
//			if (key == null) 
//			{
//				key = Registry.Users.CreateSubKey(RegistryRoot);
//			}
//
//			key.SetValue("CustomerGroup", txtCustomerGroup.Text);
//			//key.SetValue("DebugLevel", cmbDebugLevel.SelectedValue.ToString());
//			key.SetValue("LocalBoundPort", txtLocalPort.Text);
//			key.SetValue("MSSQLDatabase", txtMSSQLDatabase.Text);
//			key.SetValue("MSSQLPassword", txtMSSQLPassword.Text);
//			key.SetValue("MSSQLUsername", txtMSSQLUsername.Text);
//			key.SetValue("MSSQLServer", txtMSSQLServer.Text);
//			key.SetValue("ServerHost", txtServerHost.Text);
//			key.SetValue("ServerPort", txtRemotePort.Text);
//			key.SetValue("SSHPrivateKeyPath", txtKeyPath.Text);
//			key.SetValue("SSHUser", txtSSHUser.Text);
//			key.SetValue("UpdateServiceLink", txtUpdateService.Text);
//			
//			if (chkUseMSSQL.Checked)
//				key.SetValue("UseMSSQL", 1);
//			else 
//				key.SetValue("UseMSSQL", 0);
//
//			if (chkUseSSH.Checked)
//				key.SetValue("UseSSH", 1);
//			else
//				key.SetValue("UseSSH", 0);
//		}
//
//		private void btnTest_Click(object sender, System.EventArgs e)
//		{
//			if (!File.Exists(Environment.GetEnvironmentVariable("SystemRoot") + "\\plink.exe"))
//				SavePlinkToDisk();
//			// Start Zabbix
//			p1 = new Process();
//			p1.StartInfo.UseShellExecute = true;
//			p1.EnableRaisingEvents = true;
//			p1.StartInfo.Arguments = "-l " + txtSSHUser.Text + " -L " + txtLocalPort.Text +":localhost:" + txtRemotePort.Text +" -i " + txtKeyPath.Text + " " + txtServerHost.Text;
//			p1.StartInfo.FileName = Environment.GetEnvironmentVariable("SystemRoot") + "\\plink.exe";
//			p1.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
//			p1.Exited += new EventHandler(p1_Exited);
//			p1.Start();
//		}
//
//		private void SavePlinkToDisk() 
//		{
//			try 
//			{
//				Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixConf.SubSystem.plink.exe");
//
//				if (stream != null) 
//				{
//					FileStream write = new FileStream(Environment.GetEnvironmentVariable("SystemRoot") + "\\plink.exe",FileMode.Create);
//
//					int Length = 256;
//					Byte [] buffer = new Byte[Length];
//
//
//					int bytesRead = stream.Read(buffer,0,Length);
//					// write the required bytes
//					while( bytesRead > 0 )
//					{
//						write.Write(buffer,0,bytesRead);
//						bytesRead = stream.Read(buffer,0,Length);
//					}
//					stream.Close();
//					write.Close();
//				}
//			} 
//			catch (IOException ex)
//			{
//				
//			}
//		}
//
//		private void p1_Exited(object sender, System.EventArgs e)
//		{
//			if (p1.HasExited) 
//			{
//				FindHostKey();
//			}
//		}
//
//		private void FindHostKey() 
//		{
//			RegistryKey key = Registry.CurrentUser.OpenSubKey(PuttyKeyRoot);
//			//RegistryKey key = Registry.Users.OpenSubKey(PuttyKeyRoot);
//
//			// Find newly generated key.
//			string keyname = "";
//			string keyvalue = "";
//			
//			if (key != null) 
//			{
//				foreach(string val in key.GetValueNames()) 
//				{
//					if (val.EndsWith(txtServerHost.Text)) 
//					{
//						keyname = val;
//						keyvalue = (string)key.GetValue(val);
//						break;
//					}
//				}	
//				
//				if (!keyname.Equals("") && !txtServerHost.Text.Equals("")) 
//				{
//					
//					RegistryKey globalkey = Registry.Users.OpenSubKey(PuttyKeyGlobal,true);
//
//					if (globalkey == null) 
//						globalkey = Registry.Users.CreateSubKey(PuttyKeyGlobal);
//					
//					globalkey.SetValue(keyname, keyvalue);
//					txtHostKey.Text = keyvalue;
//				} 
//				else 
//				{
//					txtHostKey.Text = "Not found";
//				}
//			}
//		}
//
//		private void txtServerHost_TextChanged(object sender, System.EventArgs e)
//		{
//			FindHostKey();
//		}
//
//		private void btnFindKey_Click(object sender, System.EventArgs e)
//		{
//			OpenFileFindSSHKey.Filter ="Private SSH Key (*.ppk)|*.ppk|All Files (*.*)|*.*";
//			DialogResult res = OpenFileFindSSHKey.ShowDialog();
//
//			if (res.ToString().Equals("OK")) 
//			{
//				txtKeyPath.Text = OpenFileFindSSHKey.FileName;
//			}
//			
//		}
//
//		private void chkUseSSH_CheckedChanged(object sender, System.EventArgs e)
//		{
//			if (!chkUseSSH.Checked) 
//			{
//				txtSSHUser.Enabled = false;
//				txtKeyPath.Enabled = false;
//				txtLocalPort.Enabled = false;
//				btnFindKey.Enabled = false;
//				btnTest.Enabled = false;
//			} 
//			else 
//			{
//				txtSSHUser.Enabled = true;
//				txtKeyPath.Enabled = true;
//				txtLocalPort.Enabled = true;
//				btnFindKey.Enabled = true;
//				btnTest.Enabled = true;
//			}
//		}
//
//		private void chkUseMSSQL_CheckedChanged(object sender, System.EventArgs e)
//		{
//			if (!chkUseMSSQL.Checked) 
//			{
//				txtMSSQLDatabase.Enabled = false;
//				txtMSSQLPassword.Enabled = false;
//				txtMSSQLServer.Enabled = false;
//				txtMSSQLUsername.Enabled = false;
//			} 
//			else 
//			{
//				txtMSSQLDatabase.Enabled = true;
//				txtMSSQLPassword.Enabled = true;
//				txtMSSQLServer.Enabled = true;
//				txtMSSQLUsername.Enabled = true;
//			}
//		}
//
//		private void btnPerfCountersOpen_Click(object sender, System.EventArgs e)
//		{
//			PerformanceCounters pc = new PerformanceCounters();
//			pc.Show();	
//		}

	}

