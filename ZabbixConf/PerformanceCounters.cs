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
using System.Diagnostics;
using Microsoft.Win32;

namespace ZabbixConf
{
	/// <summary>
	/// Summary description for PerformanceCounters.
	/// </summary>
	public class PerformanceCounters : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ListBox lvlAddedCounters;
		private System.Windows.Forms.Button btnAddCounter;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label lblSelectedCounterCategory;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblSelectedCounterName;
		private System.Windows.Forms.Label lblSelectedCounterInstance;
		private System.Windows.Forms.TextBox txtCounterAlias;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// 
		/// </summary>
		public static string PerfCountRegistryKey = "\\PerformanceCounters";
		/// <summary>
		/// 
		/// </summary>
		public PerformanceCounters()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
			this.button2 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.lvlAddedCounters = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnAddCounter = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txtCounterAlias = new System.Windows.Forms.TextBox();
			this.lblSelectedCounterCategory = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.btnClose = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblSelectedCounterName = new System.Windows.Forms.Label();
			this.lblSelectedCounterInstance = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.lvlAddedCounters);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.treeView1);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(560, 512);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Performance Counter Mapping";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(232, 384);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(120, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "Delete";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(232, 184);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(144, 16);
			this.label4.TabIndex = 3;
			this.label4.Text = "Added counters";
			// 
			// lvlAddedCounters
			// 
			this.lvlAddedCounters.Location = new System.Drawing.Point(232, 200);
			this.lvlAddedCounters.Name = "lvlAddedCounters";
			this.lvlAddedCounters.Size = new System.Drawing.Size(320, 173);
			this.lvlAddedCounters.TabIndex = 2;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lblSelectedCounterInstance);
			this.groupBox2.Controls.Add(this.lblSelectedCounterName);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.btnAddCounter);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.txtCounterAlias);
			this.groupBox2.Controls.Add(this.lblSelectedCounterCategory);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new System.Drawing.Point(224, 24);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(328, 144);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Mapping";
			// 
			// btnAddCounter
			// 
			this.btnAddCounter.Location = new System.Drawing.Point(8, 104);
			this.btnAddCounter.Name = "btnAddCounter";
			this.btnAddCounter.TabIndex = 4;
			this.btnAddCounter.Text = "Add";
			this.btnAddCounter.Click += new System.EventHandler(this.btnAddCounter_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "Counter Alias:";
			// 
			// txtCounterAlias
			// 
			this.txtCounterAlias.Location = new System.Drawing.Point(96, 80);
			this.txtCounterAlias.Name = "txtCounterAlias";
			this.txtCounterAlias.Size = new System.Drawing.Size(224, 20);
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
			this.treeView1.Location = new System.Drawing.Point(16, 24);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(200, 472);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(488, 528);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Counter name:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 23);
			this.label5.TabIndex = 6;
			this.label5.Text = "Counter instance:";
			// 
			// lblSelectedCounterName
			// 
			this.lblSelectedCounterName.Location = new System.Drawing.Point(104, 32);
			this.lblSelectedCounterName.Name = "lblSelectedCounterName";
			this.lblSelectedCounterName.Size = new System.Drawing.Size(216, 16);
			this.lblSelectedCounterName.TabIndex = 7;
			// 
			// lblSelectedCounterInstance
			// 
			this.lblSelectedCounterInstance.Location = new System.Drawing.Point(104, 48);
			this.lblSelectedCounterInstance.Name = "lblSelectedCounterInstance";
			this.lblSelectedCounterInstance.Size = new System.Drawing.Size(216, 16);
			this.lblSelectedCounterInstance.TabIndex = 8;
			// 
			// PerformanceCounters
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 558);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.groupBox1);
			this.Name = "PerformanceCounters";
			this.Text = "Performance counter mapping";
			this.Load += new System.EventHandler(this.PerformanceCounters_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PerformanceCounters_Load(object sender, System.EventArgs e)
		{
			TreeNode[] tn = DumpPerformanceCounterToTree();
			if (tn != null) 
				treeView1.Nodes.AddRange(tn);

			treeView1.Sorted = true;
		}
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
		public TreeNode[] DumpPerformanceCounterToTree()
		{
			TreeNode[] tn = null;

			try
			{
				PerformanceCounterCategory[] categories;
				categories = PerformanceCounterCategory.GetCategories();
				
				tn = new TreeNode[categories.Length];

				for (int i=0;i<categories.Length;i++) 
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
								}
								catch
								{
								}
							}
						}
						
						
						t.Nodes.Add(tt);
					}
					tn[i] = t;
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
			return tn;
		}

		/// <summary>
		/// 
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
					lblSelectedCounterCategory.Text = treeView1.SelectedNode.Parent.Parent.Text;
					lblSelectedCounterName.Text = treeView1.SelectedNode.Parent.Text;
					lblSelectedCounterInstance.Text = treeView1.SelectedNode.Text;
				} 
				else 
				{
					lblSelectedCounterCategory.Text = treeView1.SelectedNode.Parent.Text;
					lblSelectedCounterName.Text = treeView1.SelectedNode.Text;
					lblSelectedCounterInstance.Text = "All";
				}
			}
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddCounter_Click(object sender, System.EventArgs e)
		{
			// FIXME Make a check in all counters, to see if there already is a counter by that name
			if (!txtCounterAlias.Text.Equals("")) 
			{
				
				RegistryKey key = Registry.Users.OpenSubKey(ZabbixAgentConf.RegistryRoot + PerfCountRegistryKey,true);

				if (key == null) 
				{
					key = Registry.Users.CreateSubKey(ZabbixAgentConf.RegistryRoot + PerfCountRegistryKey);
				}


				// Find next keyname
				string keyname = key.ValueCount.ToString();
				
				MessageBox.Show(treeView1.SelectedNode.FullPath);
				//key.SetValue(key.ValueCount, treeView1.SelectedNode.FullPath);

				//lvlAddedCounters.Items.Add(
			}
		}
	}
}
