using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using AMS.Profile;
using log4net;

namespace ZabbixConf
{
	/// <summary>
	/// Summary description for UserParams.
	/// </summary>
	public class UserParams : System.Windows.Forms.Form, ISettingsHandler
	{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnCounterDelete;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox txtCommandOptions;
		private System.Windows.Forms.Label lblCommandOptions;
		private System.Windows.Forms.Button btnAddUserParam;
		private System.Windows.Forms.TextBox txtUserParamAlias;
		private System.Windows.Forms.TextBox txtCommand;
		private System.Windows.Forms.Button btnCommandFile;
		private System.Windows.Forms.FolderBrowserDialog fbd;
		private System.Windows.Forms.OpenFileDialog ofd;
		private ListViewColumnSorter lvwColumnSorter;


		/// <summary>
		/// 
		/// </summary>
		public UserParams()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			listView1.View = View.Details;
			listView1.Columns.Add("Key", 150, HorizontalAlignment.Left);
			listView1.Columns.Add("Command", 390, HorizontalAlignment.Left);
			lvwColumnSorter = new ListViewColumnSorter();
			this.listView1.ListViewItemSorter = lvwColumnSorter;
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
			this.btnAddUserParam = new System.Windows.Forms.Button();
			this.txtUserParamAlias = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnCounterDelete = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnCommandFile = new System.Windows.Forms.Button();
			this.txtCommandOptions = new System.Windows.Forms.TextBox();
			this.lblCommandOptions = new System.Windows.Forms.Label();
			this.txtCommand = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.ofd = new System.Windows.Forms.OpenFileDialog();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnAddUserParam
			// 
			this.btnAddUserParam.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddUserParam.Location = new System.Drawing.Point(512, 128);
			this.btnAddUserParam.Name = "btnAddUserParam";
			this.btnAddUserParam.Size = new System.Drawing.Size(40, 23);
			this.btnAddUserParam.TabIndex = 11;
			this.btnAddUserParam.Text = "Add";
			this.btnAddUserParam.Click += new System.EventHandler(this.btnAddUserParam_Click);
			// 
			// txtUserParamAlias
			// 
			this.txtUserParamAlias.Location = new System.Drawing.Point(136, 24);
			this.txtUserParamAlias.Name = "txtUserParamAlias";
			this.txtUserParamAlias.Size = new System.Drawing.Size(255, 20);
			this.txtUserParamAlias.TabIndex = 9;
			this.txtUserParamAlias.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 16);
			this.label3.TabIndex = 10;
			this.label3.Text = "Key:";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.btnCounterDelete);
			this.groupBox3.Controls.Add(this.listView1);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(8, 168);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(560, 176);
			this.groupBox3.TabIndex = 12;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Mapped Items";
			// 
			// btnCounterDelete
			// 
			this.btnCounterDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCounterDelete.Location = new System.Drawing.Point(496, 148);
			this.btnCounterDelete.Name = "btnCounterDelete";
			this.btnCounterDelete.Size = new System.Drawing.Size(56, 23);
			this.btnCounterDelete.TabIndex = 4;
			this.btnCounterDelete.Text = "Delete";
			// 
			// listView1
			// 
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 16);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(544, 130);
			this.listView1.TabIndex = 7;
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvColumnClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnCommandFile);
			this.groupBox1.Controls.Add(this.txtCommandOptions);
			this.groupBox1.Controls.Add(this.lblCommandOptions);
			this.groupBox1.Controls.Add(this.txtCommand);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtUserParamAlias);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.btnAddUserParam);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 7);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(560, 155);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Add User Parameter";
			// 
			// btnCommandFile
			// 
			this.btnCommandFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCommandFile.Location = new System.Drawing.Point(393, 57);
			this.btnCommandFile.Name = "btnCommandFile";
			this.btnCommandFile.Size = new System.Drawing.Size(26, 19);
			this.btnCommandFile.TabIndex = 28;
			this.btnCommandFile.Text = "...";
			this.btnCommandFile.Click += new System.EventHandler(this.btnCommandFile_Click);
			// 
			// txtCommandOptions
			// 
			this.txtCommandOptions.Location = new System.Drawing.Point(136, 88);
			this.txtCommandOptions.Name = "txtCommandOptions";
			this.txtCommandOptions.Size = new System.Drawing.Size(255, 20);
			this.txtCommandOptions.TabIndex = 14;
			this.txtCommandOptions.Text = "";
			// 
			// lblCommandOptions
			// 
			this.lblCommandOptions.Location = new System.Drawing.Point(8, 88);
			this.lblCommandOptions.Name = "lblCommandOptions";
			this.lblCommandOptions.Size = new System.Drawing.Size(112, 16);
			this.lblCommandOptions.TabIndex = 15;
			this.lblCommandOptions.Text = "Command Option";
			// 
			// txtCommand
			// 
			this.txtCommand.Location = new System.Drawing.Point(136, 56);
			this.txtCommand.Name = "txtCommand";
			this.txtCommand.Size = new System.Drawing.Size(255, 20);
			this.txtCommand.TabIndex = 12;
			this.txtCommand.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "Command";
			// 
			// UserParams
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 358);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox3);
			this.Name = "UserParams";
			this.Text = "User Parameters";
			this.groupBox3.ResumeLayout(false);
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
			for( int UserParamIndex = 0; UserParamIndex < listView1.Items.Count; UserParamIndex++ )
			{
				ListViewItem keyAlias = listView1.Items[UserParamIndex];
				ListViewItem.ListViewSubItem UserCommand = listView1.Items[UserParamIndex].SubItems[1];
				profile.SetValue("UserParameters", keyAlias.Text, UserCommand.Text);
			}

		}

		/// <summary>
		/// 
		/// </summary>
		public void ReadSettings()
		{
			try
			{
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			string[] UserParamKeys = profile.GetEntryNames("UserParameters");
			for( int itemIndex = 0; itemIndex < UserParamKeys.Length; itemIndex++ )
				{
				ListViewItem lSingleItem; 
				lSingleItem = listView1.Items.Add(UserParamKeys[itemIndex]);
				string UserParams = profile.GetValue("UserParameters", UserParamKeys[itemIndex]).ToString();
				lSingleItem.SubItems.Add(UserParams);
				}
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
		private void btnAddUserParam_Click(object sender, System.EventArgs e)
		{
			if (!txtUserParamAlias.Text.Equals("")) 
			{
				// Check if supplied CounterAlias is not already in the KeyList
				if (checkKey(txtUserParamAlias.Text)) 
				{
					MessageBox.Show("Key already exists, please change your key alias", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			
				ListViewItem lSingleItem; 
				lSingleItem = listView1.Items.Add(txtUserParamAlias.Text);
				lSingleItem.SubItems.Add(txtCommand.Text + " " + txtCommandOptions.Text);	
				txtUserParamAlias.Text = "";
				txtCommand.Text = "";
				txtCommandOptions.Text = "";
			}
			else
			{
				MessageBox.Show("Please insert a key alias !", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="keyToCheck"></param>
		/// <returns></returns>
		public bool checkKey(string keyToCheck)
		{
			for( int keyIndex = 0; keyIndex < listView1.Items.Count; keyIndex++ )
			{
				ListViewItem keyAlias = listView1.Items[keyIndex];
				if(keyToCheck == keyAlias.Text)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCommandFile_Click(object sender, System.EventArgs e)
		{
			if(this.ofd.ShowDialog()== DialogResult.OK) 
			{ 
				this.txtCommand.Text = this.ofd.FileName; 
			} 

		}

		private void lvColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Determine if clicked column is already the column that is being sorted.
			if ( e.Column == lvwColumnSorter.SortColumn )
			{
				// Reverse the current sort direction for this column.
				if (lvwColumnSorter.Order == SortOrder.Ascending)
				{
					lvwColumnSorter.Order = SortOrder.Descending;
				}
				else
				{
					lvwColumnSorter.Order = SortOrder.Ascending;
				}
			}
			else
			{
				// Set the column number that is to be sorted; default to ascending.
				lvwColumnSorter.SortColumn = e.Column;
				lvwColumnSorter.Order = SortOrder.Ascending;
			}
			// Perform the sort with these new sort options.
			this.listView1.Sort();
		}



	}
}
