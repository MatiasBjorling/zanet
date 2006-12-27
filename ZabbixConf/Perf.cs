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
 * 
 * Used component for xml settings: http://www.codeproject.com/csharp/readwritexmlini.asp
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml;
using AMS.Profile;
using log4net;
using log4net.Config;
using System.Resources;


namespace ZabbixConf
{
	/// <summary>
	/// Summary description for Perf.
	/// </summary>
	public class Perf : System.Windows.Forms.Form, ISettingsHandler
	{
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lblSelectedCounterInstance;
		private System.Windows.Forms.Label lblSelectedCounterName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnAddCounter;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtCounterAlias;
		private System.Windows.Forms.Label lblSelectedCounterCategory;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TreeView treeView1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnCounterDelete;
		/// <summary>
		/// 
		/// </summary>
		public static string PerfCountRegistryKey = "\\PerformanceCounters";
		private System.Windows.Forms.TextBox txtdesc;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblCounter;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lblValue;
		private System.Windows.Forms.Label txtValueType; 
		private ListViewColumnSorter lvwColumnSorter;
		private System.Windows.Forms.Button button1;
		
		private ILog log = log4net.LogManager.GetLogger("net.sourceforge.zabbixconf.perf");

		//XmlConfigurator.Configure(Assembly.GetCallingAssembly().GetManifestResourceStream("ZabbixConfig.SubSystem.log.xml"));


		/// <summary>
		/// Initialize, add listview columnheaders, add listview sort and retrieve saved settings
		/// </summary>
		public Perf()
		{
		
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			listView1.View = View.Details;
			listView1.Columns.Add("Key", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("Counter", 200, HorizontalAlignment.Left);
			lvwColumnSorter = new ListViewColumnSorter();
			this.listView1.ListViewItemSorter = lvwColumnSorter;
			readsettings();


			log.Debug("test");

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>

		private void InitializeComponent()
		{
			this.listView1 = new System.Windows.Forms.ListView();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtValueType = new System.Windows.Forms.Label();
			this.lblValue = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lblCounter = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtdesc = new System.Windows.Forms.TextBox();
			this.lblSelectedCounterInstance = new System.Windows.Forms.Label();
			this.lblSelectedCounterName = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnAddCounter = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txtCounterAlias = new System.Windows.Forms.TextBox();
			this.lblSelectedCounterCategory = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnCounterDelete = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(8, 16);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(336, 106);
			this.listView1.TabIndex = 7;
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvColumnClick);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(8, 333);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(200, 16);
			this.progressBar1.TabIndex = 5;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtValueType);
			this.groupBox2.Controls.Add(this.lblValue);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.lblCounter);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.txtdesc);
			this.groupBox2.Controls.Add(this.lblSelectedCounterInstance);
			this.groupBox2.Controls.Add(this.lblSelectedCounterName);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.btnAddCounter);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.txtCounterAlias);
			this.groupBox2.Controls.Add(this.lblSelectedCounterCategory);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(215, 2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(353, 190);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Key - Counter Mapping";
			// 
			// txtValueType
			// 
			this.txtValueType.Location = new System.Drawing.Point(208, 131);
			this.txtValueType.Name = "txtValueType";
			this.txtValueType.Size = new System.Drawing.Size(128, 16);
			this.txtValueType.TabIndex = 13;
			// 
			// lblValue
			// 
			this.lblValue.Location = new System.Drawing.Point(57, 135);
			this.lblValue.Name = "lblValue";
			this.lblValue.Size = new System.Drawing.Size(135, 13);
			this.lblValue.TabIndex = 12;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 135);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 16);
			this.label6.TabIndex = 11;
			this.label6.Text = "Value";
			// 
			// lblCounter
			// 
			this.lblCounter.Location = new System.Drawing.Point(58, 108);
			this.lblCounter.Name = "lblCounter";
			this.lblCounter.Size = new System.Drawing.Size(286, 15);
			this.lblCounter.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 108);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Counter:";
			// 
			// txtdesc
			// 
			this.txtdesc.Location = new System.Drawing.Point(8, 16);
			this.txtdesc.Multiline = true;
			this.txtdesc.Name = "txtdesc";
			this.txtdesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtdesc.Size = new System.Drawing.Size(336, 83);
			this.txtdesc.TabIndex = 6;
			this.txtdesc.Text = "";
			// 
			// lblSelectedCounterInstance
			// 
			this.lblSelectedCounterInstance.Location = new System.Drawing.Point(104, 48);
			this.lblSelectedCounterInstance.Name = "lblSelectedCounterInstance";
			this.lblSelectedCounterInstance.Size = new System.Drawing.Size(216, 16);
			this.lblSelectedCounterInstance.TabIndex = 8;
			// 
			// lblSelectedCounterName
			// 
			this.lblSelectedCounterName.Location = new System.Drawing.Point(104, 32);
			this.lblSelectedCounterName.Name = "lblSelectedCounterName";
			this.lblSelectedCounterName.Size = new System.Drawing.Size(216, 16);
			this.lblSelectedCounterName.TabIndex = 7;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 23);
			this.label5.TabIndex = 6;
			this.label5.Text = "Counter instance:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Counter name:";
			// 
			// btnAddCounter
			// 
			this.btnAddCounter.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddCounter.Location = new System.Drawing.Point(304, 158);
			this.btnAddCounter.Name = "btnAddCounter";
			this.btnAddCounter.Size = new System.Drawing.Size(40, 23);
			this.btnAddCounter.TabIndex = 4;
			this.btnAddCounter.Text = "Add";
			this.btnAddCounter.Click += new System.EventHandler(this.btnAddCounter_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 162);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 16);
			this.label3.TabIndex = 3;
			this.label3.Text = "Key:";
			// 
			// txtCounterAlias
			// 
			this.txtCounterAlias.Location = new System.Drawing.Point(40, 160);
			this.txtCounterAlias.Name = "txtCounterAlias";
			this.txtCounterAlias.Size = new System.Drawing.Size(255, 20);
			this.txtCounterAlias.TabIndex = 2;
			this.txtCounterAlias.Text = "";
			// 
			// lblSelectedCounterCategory
			// 
			this.lblSelectedCounterCategory.Location = new System.Drawing.Point(104, 16);
			this.lblSelectedCounterCategory.Name = "lblSelectedCounterCategory";
			this.lblSelectedCounterCategory.Size = new System.Drawing.Size(216, 16);
			this.lblSelectedCounterCategory.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Counter category:";
			// 
			// treeView1
			// 
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(8, 8);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(200, 320);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.button1);
			this.groupBox3.Controls.Add(this.btnCounterDelete);
			this.groupBox3.Controls.Add(this.listView1);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(216, 197);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(352, 154);
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Mapped Items";
			// 
			// btnCounterDelete
			// 
			this.btnCounterDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCounterDelete.Location = new System.Drawing.Point(289, 126);
			this.btnCounterDelete.Name = "btnCounterDelete";
			this.btnCounterDelete.Size = new System.Drawing.Size(56, 23);
			this.btnCounterDelete.TabIndex = 4;
			this.btnCounterDelete.Text = "Delete";
			this.btnCounterDelete.Click += new System.EventHandler(this.btnCounterDelete_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(112, 128);
			this.button1.Name = "button1";
			this.button1.TabIndex = 8;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Perf
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 358);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.progressBar1);
			this.Name = "Perf";
			this.Text = "Performance counter mapping";
			this.Load += new System.EventHandler(this.Perf_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		/// <summary>
		/// Form load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Perf_Load(object sender, System.EventArgs e)
		{
			BackgroundWorker backgroundWorker = new BackgroundWorker();
			backgroundWorker.WorkerReportsProgress = true;
			backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorkerProgressChanged);
			backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorkerRunWorkerCompleted);
			backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorkerDoWork);
			backgroundWorker.RunWorkerAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
		{
		
			BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
			TreeNode[] tn = null;
			PerformanceCounterCategory[] categories = PerformanceCounterCategory.GetCategories();
			tn = new TreeNode[categories.Length];
			for (int i = 0; i < categories.Length; i++)
			{
				// Root
				TreeNode t = new TreeNode(categories[i].CategoryName);

				InstanceDataCollectionCollection instanceDataCollectionCollection = categories[i].ReadCategory();

				foreach (DictionaryEntry instanceDataCollectionEntry in instanceDataCollectionCollection)
				{
					InstanceDataCollection idc = (InstanceDataCollection)instanceDataCollectionEntry.Value;
					TreeNode tt = new TreeNode();
					tt.Text = idc.CounterName;
                    if (idc.Count > 1)
					{
						foreach (DictionaryEntry instanceDataEntry in (InstanceDataCollection)instanceDataCollectionEntry.Value)
						{
							try
							{
								tt.Nodes.Add(((InstanceData)instanceDataEntry.Value).InstanceName);
								log.Debug(((InstanceData)instanceDataEntry.Value).InstanceName);
							}
							catch
							{
							}
						}
					}
					t.Nodes.Add(tt);
				}
				tn[i] = t;
				double progressPercentage = ((double)i + 1) / categories.Length * 100d;
				backgroundWorker.ReportProgress((int)progressPercentage);
		
			}
			e.Result = tn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error == null)
			{
				treeView1.BeginUpdate();
				treeView1.Nodes.AddRange((TreeNode[])e.Result);
				treeView1.Sorted = true;
				treeView1.EndUpdate();
			
			}
			else
			{
				MessageBox.Show(e.Error.Message);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
		}

		/// <summary>
		/// Retrieve counterinfo on treeview_afterselect
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (treeView1.SelectedNode.Parent != null)
			{
				// Instance??
				if (treeView1.SelectedNode.Parent.Parent != null)
				{
					try
					{
						lblCounter.Text = @"\";
						lblCounter.Text += treeView1.SelectedNode.Parent.Parent.Text;
						lblCounter.Text += "(";
						lblCounter.Text += treeView1.SelectedNode.Text;
						lblCounter.Text += @")\";
						lblCounter.Text += treeView1.SelectedNode.Parent.Text;
						PerformanceCounter myCounter = new PerformanceCounter();
						myCounter.CategoryName = treeView1.SelectedNode.Parent.Parent.Text;
						myCounter.CounterName = treeView1.SelectedNode.Parent.Text;
						myCounter.InstanceName = treeView1.SelectedNode.Text;
						txtdesc.Text = myCounter.CounterHelp.ToString();
						string result;
						if(myCounter.CounterType.ToString().StartsWith("NumberOfItems"))
						{
							result = myCounter.RawValue.ToString();
							lblValue.Text = result;
							txtValueType.Text = "long";
						}
						else
						{
							// Retrieve a sample.
							CounterSample sample1 = myCounter.NextSample();
							// Wait some interval of time here and retrieve
							// a second sample.
							System.Threading.Thread.Sleep(1000);
							CounterSample sample2 = myCounter.NextSample();
							// Pass both samples to the Calculate method.
							result = CounterSample.Calculate(sample1, sample2).ToString();
							lblValue.Text = result;
							txtValueType.Text = "float";
						}					
					}
					catch (Exception ex)
					{
						//MessageBox.Show(ex.ToString());
					
					}
				}
				else
				{
					try
					{
					lblCounter.Text = @"\";
					lblCounter.Text += treeView1.SelectedNode.Parent.Text;
					lblCounter.Text += @"\";
					lblCounter.Text += treeView1.SelectedNode.Text;
					//lblCounter.Text += "All";
					PerformanceCounter myCounter = new PerformanceCounter();
					myCounter.CategoryName = treeView1.SelectedNode.Parent.Text;
					myCounter.CounterName = treeView1.SelectedNode.Text;
					txtdesc.Text = myCounter.CounterHelp.ToString();
						txtValueType.Text = myCounter.NextValue().ToString();
						string result;
						if(myCounter.CounterType.ToString().StartsWith("NumberOfItems"))
						{
							result = myCounter.NextSample().RawValue.ToString();
							lblValue.Text = result;
							txtValueType.Text = "long";
						}
						else
						{
							// Retrieve a sample.
							CounterSample sample1 = myCounter.NextSample();
							// Wait some interval of time here and retrieve
							// a second sample.
							System.Threading.Thread.Sleep(1000);
							CounterSample sample2 = myCounter.NextSample();
							// Pass both samples to the Calculate method.
							result = CounterSample.Calculate(sample1, sample2).ToString();
							lblValue.Text = result;
							txtValueType.Text = "float";
						}
					}
					catch (Exception ex)
					{
						//MessageBox.Show(ex.ToString());
					}
				}
			}
		}

		/// <summary>
		/// Add key to listview
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddCounter_Click(object sender, System.EventArgs e)
		{
			// Check if CounterAlias was supplied
			if (!txtCounterAlias.Text.Equals("")) 
			{
				// Check if supplied CounterAlias is not already in the KeyList
				if (checkKey(txtCounterAlias.Text)) 
				{
					MessageBox.Show("Key already exists, please change your key alias", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			
				ListViewItem lSingleItem; 
				lSingleItem = listView1.Items.Add(txtCounterAlias.Text);
				lSingleItem.SubItems.Add(lblCounter.Text);	
				txtCounterAlias.Text = "";
			}
			else
			{
				MessageBox.Show("Please insert a key alias !", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		/// <summary>
		/// Delete key from listview and ZabbixConf.exe.xml
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
 		private void btnCounterDelete_Click(object sender, System.EventArgs e)
		{
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			DialogResult dr = MessageBox.Show("Are you sure you want to delete the selected items?", "Attention!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if ( dr == DialogResult.Cancel ) 
				return;
			else
				foreach (ListViewItem lvItem in listView1.SelectedItems)
				{
					profile.RemoveEntry("PerfCounters", lvItem.Text);		
					lvItem.Remove();
				}
		}

		/// <summary>
		/// Save added counters to ZabbixConf.exe.xml
		/// </summary>
		public void Save()
		{
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			for( int keyIndex = 0; keyIndex < listView1.Items.Count; keyIndex++ )
			{
				ListViewItem keyAlias = listView1.Items[keyIndex];
				ListViewItem.ListViewSubItem perfCounter = listView1.Items[keyIndex].SubItems[1];
				profile.SetValue("PerfCounters", keyAlias.Text, perfCounter.Text);
			}
		}
		
		/// <summary>
		/// Retrieve settings from ZabbixConf.exe.xml
		/// </summary>
		public void readsettings()
		{
			try
			{
			Xml profile = new Xml(ZabbixAgentConf.ConfigFile);
			string[] performanceKeys = profile.GetEntryNames("PerfCounters");
			for( int itemIndex = 0; itemIndex < performanceKeys.Length; itemIndex++ )
				{
				ListViewItem lSingleItem; 
				lSingleItem = listView1.Items.Add(performanceKeys[itemIndex]);
				string performanceCounter = profile.GetValue("PerfCounters", performanceKeys[itemIndex]).ToString();
				lSingleItem.SubItems.Add(performanceCounter);
				}
			}
			catch
			{
			}

		}

		/// <summary>
		/// Allow key alias edit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//private void lvEdit(object sender, System.EventArgs e)
		//{
		//	listView1.LabelEdit = true;
		//}
		
		/// <summary>
		/// Check if key already exists in keylist
		/// </summary>
		/// <param name="keyToCheck"></param>
		/// <returns>true/false</returns>
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
		/// Provide sorting for the added counters listview
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		private void button1_Click(object sender, System.EventArgs e)
		{

		}

	}
}