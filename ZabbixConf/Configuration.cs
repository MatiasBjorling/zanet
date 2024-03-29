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
using System.Collections;
using System.Reflection;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Xml;
using log4net.Config;

namespace ZabbixConf
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>

	public class ZabbixAgentConf : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.OpenFileDialog OpenFileFindSSHKey;
		internal System.Windows.Forms.Button btnApply;
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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnRestart;
		private System.Windows.Forms.GLabel label2;
		//private Process p1 = null;
		
		/// <summary>
		/// 
		/// </summary>
		public ZabbixAgentConf()
		{
			InitializeComponent();

            //foreach (string s in Assembly.GetCallingAssembly().GetManifestResourceNames())
            //    Console.WriteLine(s);

			XmlConfigurator.Configure(Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixConf.SubSystem.log_debug.xml"));

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
			this.panel1 = new System.Windows.Forms.Panel();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.label2 = new System.Windows.Forms.GLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.btnRestart = new System.Windows.Forms.Button();
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
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(312, 392);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(248, 23);
			this.label1.TabIndex = 18;
			this.label1.Text = "Restart ZabbixAgent.NET Service after applying.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnRestart
			// 
			this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRestart.Location = new System.Drawing.Point(568, 392);
			this.btnRestart.Name = "btnRestart";
			this.btnRestart.Size = new System.Drawing.Size(104, 23);
			this.btnRestart.TabIndex = 19;
			this.btnRestart.Text = "Restart Service";
			this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
			// 
			// ZabbixAgentConf
			// 
			this.AcceptButton = this.btnApply;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(770, 424);
			this.Controls.Add(this.btnRestart);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
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
		    string directory = Assembly.GetExecutingAssembly().Location;
		    directory = directory.Substring(0, directory.LastIndexOf("\\") + 1);
			form.LoadFromXml(string.Format("{0}\\settings.xml", directory));
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
			try 
			{
				foreach (TreeNode treeNode in treeView1.Nodes)
					SaveRecursive(treeNode);

			} 
			catch (Exception ex) 
			{
				MessageBox.Show(ex.Message);
			}
			MessageBox.Show("Settings saved");
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

		private void btnRestart_Click(object sender, System.EventArgs e)
		{
			System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController("zabbixagentnet");
			try
			{
				if (service.Status.Equals(ServiceControllerStatus.Stopped))
				{
					// Start the service if the current status is stopped.
					DateTime tn = DateTime.Now; 
					MessageBox.Show(tn.ToString() + " service was stopped, starting now");
					service.Start();
					service.WaitForStatus(ServiceControllerStatus.Running);
				} 
				else if(service.Status.Equals(ServiceControllerStatus.StopPending))
				{
					DateTime tn = DateTime.Now; 
					MessageBox.Show(tn.ToString() + " Service is stopping, waiting....");
					service.WaitForStatus(ServiceControllerStatus.Stopped);
					DateTime tp = DateTime.Now; 
					MessageBox.Show(tp.ToString() + " Restarting service.");
					service.Start();
					service.WaitForStatus(ServiceControllerStatus.Running);
				}
				else
				{
					// Stop the service if its status is not set to "Stopped".
					DateTime tn = DateTime.Now; 
					MessageBox.Show(tn.ToString() + " Stopping service.");
					service.Stop();
					service.WaitForStatus(ServiceControllerStatus.Stopped);
					DateTime tp = DateTime.Now; 
					MessageBox.Show(tp.ToString() + " Restarting service.");
					service.Start();
					service.WaitForStatus(ServiceControllerStatus.Running);
					DateTime ts = DateTime.Now;
					MessageBox.Show(ts.ToString() + " Service started successfuly.");
				}  


			}
			catch(System.InvalidOperationException)
			{
				DateTime tn = DateTime.Now; 
				MessageBox.Show(tn.ToString() + " Service does not exist.");
			}
		}
	}
}

