/*************************************************************************
 *  SMOz (Start Menu Organizer)
 *  Copyright (C) 2006 Nithin Philips
 *
 *  This program is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU General Public License
 *  as published by the Free Software Foundation; either version 2
 *  of the License, or (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation,Inc.,59 Temple Place - Suite 330,Boston,MA 02111-1307, USA.
 *
 *  Author            :  Nithin Philips <spikiermonkey@users.sourceforge.net>
 *  Original FileName :  TemplateEditor.Designer.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

namespace SMOz.UI
{
    partial class TemplateEditor
    {
	   /// <summary>
	   /// Required designer variable.
	   /// </summary>
	   private System.ComponentModel.IContainer components = null;

	   /// <summary>
	   /// Clean up any resources being used.
	   /// </summary>
	   /// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	   protected override void Dispose(bool disposing) {
		  if (disposing && (components != null)) {
			 components.Dispose();
		  }
		  base.Dispose(disposing);
	   }

	   #region Windows Form Designer generated code

	   /// <summary>
	   /// Required method for Designer support - do not modify
	   /// the contents of this method with the code editor.
	   /// </summary>
	   private void InitializeComponent() {
		  this.components = new System.ComponentModel.Container();
		  this._searchQuery = new System.Windows.Forms.TextBox();
		  this._searchLabel = new System.Windows.Forms.Label();
		  this._previewAutoHide = new System.Windows.Forms.CheckBox();
		  this._showPreview = new System.Windows.Forms.CheckBox();
		  this._templateTable = new XPTable.Models.Table();
		  this._contextIgnoreList = new System.Windows.Forms.ContextMenuStrip(this.components);
		  this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		  this._categoryTree = new SMOz.Utilities.TreeViewEx();
		  ((System.ComponentModel.ISupportInitialize)(this._templateTable)).BeginInit();
		  this._contextIgnoreList.SuspendLayout();
		  this.splitContainer1.Panel1.SuspendLayout();
		  this.splitContainer1.Panel2.SuspendLayout();
		  this.splitContainer1.SuspendLayout();
		  this.SuspendLayout();
		  // 
		  // _searchQuery
		  // 
		  this._searchQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		  this._searchQuery.Location = new System.Drawing.Point(461, 318);
		  this._searchQuery.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
		  this._searchQuery.Name = "_searchQuery";
		  this._searchQuery.Size = new System.Drawing.Size(184, 20);
		  this._searchQuery.TabIndex = 3;
		  this._searchQuery.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		  this._searchQuery.Enter += new System.EventHandler(this.textBox1_Enter);
		  this._searchQuery.Leave += new System.EventHandler(this._searchQuery_Leave);
		  this._searchQuery.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
		  // 
		  // _searchLabel
		  // 
		  this._searchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		  this._searchLabel.AutoSize = true;
		  this._searchLabel.BackColor = System.Drawing.SystemColors.Window;
		  this._searchLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
		  this._searchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		  this._searchLabel.ForeColor = System.Drawing.Color.Gray;
		  this._searchLabel.Location = new System.Drawing.Point(598, 321);
		  this._searchLabel.Name = "_searchLabel";
		  this._searchLabel.Size = new System.Drawing.Size(47, 13);
		  this._searchLabel.TabIndex = 4;
		  this._searchLabel.Text = "Search";
		  this._searchLabel.Enter += new System.EventHandler(this.textBox1_Enter);
		  this._searchLabel.Click += new System.EventHandler(this._searchLabel_Click);
		  this._searchLabel.Leave += new System.EventHandler(this._searchQuery_Leave);
		  // 
		  // _previewAutoHide
		  // 
		  this._previewAutoHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		  this._previewAutoHide.AutoSize = true;
		  this._previewAutoHide.Checked = true;
		  this._previewAutoHide.CheckState = System.Windows.Forms.CheckState.Checked;
		  this._previewAutoHide.Location = new System.Drawing.Point(93, 321);
		  this._previewAutoHide.Name = "_previewAutoHide";
		  this._previewAutoHide.Size = new System.Drawing.Size(73, 17);
		  this._previewAutoHide.TabIndex = 9;
		  this._previewAutoHide.Text = "Auto Hide";
		  this._previewAutoHide.UseVisualStyleBackColor = true;
		  this._previewAutoHide.Visible = false;
		  // 
		  // _showPreview
		  // 
		  this._showPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		  this._showPreview.Appearance = System.Windows.Forms.Appearance.Button;
		  this._showPreview.Image = global::SMOz.Properties.Resources.Edit_Find;
		  this._showPreview.Location = new System.Drawing.Point(12, 315);
		  this._showPreview.Name = "_showPreview";
		  this._showPreview.Size = new System.Drawing.Size(75, 24);
		  this._showPreview.TabIndex = 10;
		  this._showPreview.Text = "Preview";
		  this._showPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		  this._showPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		  this._showPreview.UseVisualStyleBackColor = true;
		  this._showPreview.CheckedChanged += new System.EventHandler(this._showPreview_CheckedChanged);
		  // 
		  // _templateTable
		  // 
		  this._templateTable.ContextMenuStrip = this._contextIgnoreList;
		  this._templateTable.Dock = System.Windows.Forms.DockStyle.Fill;
		  this._templateTable.Location = new System.Drawing.Point(0, 0);
		  this._templateTable.Name = "_templateTable";
		  this._templateTable.NoItemsText = "Right Click to add a new pattern.";
		  this._templateTable.Size = new System.Drawing.Size(469, 300);
		  this._templateTable.TabIndex = 11;
		  this._templateTable.Text = "table1";
		  // 
		  // _contextIgnoreList
		  // 
		  this._contextIgnoreList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.deleteToolStripMenuItem});
		  this._contextIgnoreList.Name = "_contextIgnoreList";
		  this._contextIgnoreList.ShowImageMargin = false;
		  this._contextIgnoreList.Size = new System.Drawing.Size(92, 48);
		  this._contextIgnoreList.Opening += new System.ComponentModel.CancelEventHandler(this._contextIgnoreList_Opening);
		  // 
		  // newToolStripMenuItem
		  // 
		  this.newToolStripMenuItem.Name = "newToolStripMenuItem";
		  this.newToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
		  this.newToolStripMenuItem.Text = "New";
		  this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
		  // 
		  // deleteToolStripMenuItem
		  // 
		  this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
		  this.deleteToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
		  this.deleteToolStripMenuItem.Text = "Delete";
		  this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
		  // 
		  // splitContainer1
		  // 
		  this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
				    | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		  this.splitContainer1.Location = new System.Drawing.Point(12, 12);
		  this.splitContainer1.Name = "splitContainer1";
		  // 
		  // splitContainer1.Panel1
		  // 
		  this.splitContainer1.Panel1.Controls.Add(this._categoryTree);
		  // 
		  // splitContainer1.Panel2
		  // 
		  this.splitContainer1.Panel2.Controls.Add(this._templateTable);
		  this.splitContainer1.Size = new System.Drawing.Size(633, 300);
		  this.splitContainer1.SplitterDistance = 160;
		  this.splitContainer1.TabIndex = 13;
		  // 
		  // _categoryTree
		  // 
		  this._categoryTree.AllowDrop = true;
		  this._categoryTree.Dock = System.Windows.Forms.DockStyle.Fill;
		  this._categoryTree.FullRowSelect = true;
		  this._categoryTree.HideSelection = false;
		  this._categoryTree.LabelEdit = true;
		  this._categoryTree.Location = new System.Drawing.Point(0, 0);
		  this._categoryTree.Name = "_categoryTree";
		  this._categoryTree.Size = new System.Drawing.Size(160, 300);
		  this._categoryTree.TabIndex = 1;
		  this._categoryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._categoryTree_AfterSelect);
		  // 
		  // TemplateEditor
		  // 
		  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		  this.ClientSize = new System.Drawing.Size(657, 350);
		  this.Controls.Add(this.splitContainer1);
		  this.Controls.Add(this._previewAutoHide);
		  this.Controls.Add(this._showPreview);
		  this.Controls.Add(this._searchLabel);
		  this.Controls.Add(this._searchQuery);
		  this.MinimizeBox = false;
		  this.Name = "TemplateEditor";
		  this.ShowInTaskbar = false;
		  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		  this.Text = "Template Editor";
		  this.SizeChanged += new System.EventHandler(this.TemplateEditor_SizeChanged);
		  this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TemplateEditor_FormClosing);
		  ((System.ComponentModel.ISupportInitialize)(this._templateTable)).EndInit();
		  this._contextIgnoreList.ResumeLayout(false);
		  this.splitContainer1.Panel1.ResumeLayout(false);
		  this.splitContainer1.Panel2.ResumeLayout(false);
		  this.splitContainer1.ResumeLayout(false);
		  this.ResumeLayout(false);
		  this.PerformLayout();

	   }

	   #endregion

	   private System.Windows.Forms.TextBox _searchQuery;
	   private System.Windows.Forms.Label _searchLabel;
	   private System.Windows.Forms.CheckBox _previewAutoHide;
	   private System.Windows.Forms.CheckBox _showPreview;
	   private XPTable.Models.Table _templateTable;
	   private System.Windows.Forms.ContextMenuStrip _contextIgnoreList;
	   private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
	   private System.Windows.Forms.SplitContainer splitContainer1;
	   private SMOz.Utilities.TreeViewEx _categoryTree;
    }
}