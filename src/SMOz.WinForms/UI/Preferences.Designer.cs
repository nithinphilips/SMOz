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
 *  Original FileName :  Preferences.Designer.cs
 *  Created           :  Wed Apr 12 2006
 *  Description       :  
 *************************************************************************/

namespace SMOz.UI
{
    partial class Preferences
    {
	   /// <summary>
	   /// Required designer variable.
	   /// </summary>
	   private System.ComponentModel.IContainer components = null;

	   /// <summary>
	   /// Clean up any resources being used.
	   /// </summary>
	   /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
		  this._tabContainer = new System.Windows.Forms.TabControl();
		  this._tabIgnoreList = new System.Windows.Forms.TabPage();
		  this._contextIgnoreList = new System.Windows.Forms.ContextMenuStrip(this.components);
		  this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.tabPage1 = new System.Windows.Forms.TabPage();
		  this.btRemovePath = new System.Windows.Forms.Button();
		  this.btAddPath = new System.Windows.Forms.Button();
		  this.lbLocalDir = new System.Windows.Forms.CheckBox();
		  this.lbUserDir = new System.Windows.Forms.CheckBox();
		  this.lstAddtPaths = new System.Windows.Forms.ListView();
		  this._cancel = new System.Windows.Forms.Button();
		  this._ok = new System.Windows.Forms.Button();
		  this._ignoreList = new EXControls.EXListView();
		  this.colPath = new System.Windows.Forms.ColumnHeader();
		  this._tabContainer.SuspendLayout();
		  this._tabIgnoreList.SuspendLayout();
		  this._contextIgnoreList.SuspendLayout();
		  this.tabPage1.SuspendLayout();
		  this.SuspendLayout();
		  // 
		  // _tabContainer
		  // 
		  this._tabContainer.Controls.Add(this._tabIgnoreList);
		  this._tabContainer.Controls.Add(this.tabPage1);
		  this._tabContainer.Location = new System.Drawing.Point(12, 12);
		  this._tabContainer.Name = "_tabContainer";
		  this._tabContainer.SelectedIndex = 0;
		  this._tabContainer.Size = new System.Drawing.Size(510, 274);
		  this._tabContainer.TabIndex = 0;
		  // 
		  // _tabIgnoreList
		  // 
		  this._tabIgnoreList.Controls.Add(this._ignoreList);
		  this._tabIgnoreList.Location = new System.Drawing.Point(4, 22);
		  this._tabIgnoreList.Name = "_tabIgnoreList";
		  this._tabIgnoreList.Padding = new System.Windows.Forms.Padding(3);
		  this._tabIgnoreList.Size = new System.Drawing.Size(502, 248);
		  this._tabIgnoreList.TabIndex = 1;
		  this._tabIgnoreList.Text = "Ignore List";
		  this._tabIgnoreList.UseVisualStyleBackColor = true;
		  // 
		  // _contextIgnoreList
		  // 
		  this._contextIgnoreList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.deleteToolStripMenuItem});
		  this._contextIgnoreList.Name = "_contextIgnoreList";
		  this._contextIgnoreList.ShowImageMargin = false;
		  this._contextIgnoreList.Size = new System.Drawing.Size(81, 48);
		  this._contextIgnoreList.Opening += new System.ComponentModel.CancelEventHandler(this._contextIgnoreList_Opening);
		  // 
		  // newToolStripMenuItem
		  // 
		  this.newToolStripMenuItem.Name = "newToolStripMenuItem";
		  this.newToolStripMenuItem.Size = new System.Drawing.Size(80, 22);
		  this.newToolStripMenuItem.Text = "New";
		  this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
		  // 
		  // deleteToolStripMenuItem
		  // 
		  this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
		  this.deleteToolStripMenuItem.Size = new System.Drawing.Size(80, 22);
		  this.deleteToolStripMenuItem.Text = "Delete";
		  this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
		  // 
		  // tabPage1
		  // 
		  this.tabPage1.Controls.Add(this.btRemovePath);
		  this.tabPage1.Controls.Add(this.btAddPath);
		  this.tabPage1.Controls.Add(this.lbLocalDir);
		  this.tabPage1.Controls.Add(this.lbUserDir);
		  this.tabPage1.Controls.Add(this.lstAddtPaths);
		  this.tabPage1.Location = new System.Drawing.Point(4, 22);
		  this.tabPage1.Name = "tabPage1";
		  this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		  this.tabPage1.Size = new System.Drawing.Size(502, 248);
		  this.tabPage1.TabIndex = 2;
		  this.tabPage1.Text = "Paths";
		  this.tabPage1.UseVisualStyleBackColor = true;
		  // 
		  // btRemovePath
		  // 
		  this.btRemovePath.Image = global::SMOz.Properties.Resources.list_remove;
		  this.btRemovePath.Location = new System.Drawing.Point(466, 97);
		  this.btRemovePath.Name = "btRemovePath";
		  this.btRemovePath.Size = new System.Drawing.Size(30, 26);
		  this.btRemovePath.TabIndex = 4;
		  this.btRemovePath.UseVisualStyleBackColor = true;
		  this.btRemovePath.Click += new System.EventHandler(this.btRemovePath_Click);
		  // 
		  // btAddPath
		  // 
		  this.btAddPath.Image = global::SMOz.Properties.Resources.list_add;
		  this.btAddPath.Location = new System.Drawing.Point(466, 65);
		  this.btAddPath.Name = "btAddPath";
		  this.btAddPath.Size = new System.Drawing.Size(30, 26);
		  this.btAddPath.TabIndex = 3;
		  this.btAddPath.UseVisualStyleBackColor = true;
		  this.btAddPath.Click += new System.EventHandler(this.btAddPath_Click);
		  // 
		  // lbLocalDir
		  // 
		  this.lbLocalDir.AutoSize = true;
		  this.lbLocalDir.Location = new System.Drawing.Point(13, 39);
		  this.lbLocalDir.Name = "lbLocalDir";
		  this.lbLocalDir.Size = new System.Drawing.Size(140, 17);
		  this.lbLocalDir.TabIndex = 2;
		  this.lbLocalDir.Text = "Your Local path is: \"C..\"";
		  // 
		  // lbUserDir
		  // 
		  this.lbUserDir.AutoSize = true;
		  this.lbUserDir.Location = new System.Drawing.Point(13, 15);
		  this.lbUserDir.Name = "lbUserDir";
		  this.lbUserDir.Size = new System.Drawing.Size(136, 17);
		  this.lbUserDir.TabIndex = 1;
		  this.lbUserDir.Text = "Your User path is: \"C..\"";
		  // 
		  // lstAddtPaths
		  // 
		  this.lstAddtPaths.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPath});
		  this.lstAddtPaths.Location = new System.Drawing.Point(16, 65);
		  this.lstAddtPaths.Name = "lstAddtPaths";
		  this.lstAddtPaths.Size = new System.Drawing.Size(444, 168);
		  this.lstAddtPaths.TabIndex = 0;
		  this.lstAddtPaths.UseCompatibleStateImageBehavior = false;
		  this.lstAddtPaths.View = System.Windows.Forms.View.Details;
		  // 
		  // _cancel
		  // 
		  this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		  this._cancel.Location = new System.Drawing.Point(362, 292);
		  this._cancel.Name = "_cancel";
		  this._cancel.Size = new System.Drawing.Size(75, 23);
		  this._cancel.TabIndex = 1;
		  this._cancel.Text = "Cancel";
		  this._cancel.UseVisualStyleBackColor = true;
		  // 
		  // _ok
		  // 
		  this._ok.DialogResult = System.Windows.Forms.DialogResult.OK;
		  this._ok.Location = new System.Drawing.Point(443, 292);
		  this._ok.Name = "_ok";
		  this._ok.Size = new System.Drawing.Size(75, 23);
		  this._ok.TabIndex = 2;
		  this._ok.Text = "OK";
		  this._ok.UseVisualStyleBackColor = true;
		  this._ok.Click += new System.EventHandler(this._ok_Click);
		  // 
		  // _ignoreList
		  // 
		  this._ignoreList.AllowColumnSort = false;
		  this._ignoreList.ContextMenuStrip = this._contextIgnoreList;
		  this._ignoreList.FullRowSelect = true;
		  this._ignoreList.Location = new System.Drawing.Point(6, 6);
		  this._ignoreList.Name = "_ignoreList";
		  this._ignoreList.OwnerDraw = true;
		  this._ignoreList.Size = new System.Drawing.Size(490, 236);
		  this._ignoreList.TabIndex = 0;
		  this._ignoreList.UseCompatibleStateImageBehavior = false;
		  this._ignoreList.View = System.Windows.Forms.View.Details;
		  this._ignoreList.SelectedIndexChanged += new System.EventHandler(this._ignoreList_SelectedIndexChanged);
		  this._ignoreList.MouseUp += new System.Windows.Forms.MouseEventHandler(this._ignoreList_MouseUp);
		  // 
		  // colPath
		  // 
		  this.colPath.Text = "Path";
		  this.colPath.Width = 417;
		  // 
		  // Preferences
		  // 
		  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		  this.ClientSize = new System.Drawing.Size(534, 325);
		  this.Controls.Add(this._ok);
		  this.Controls.Add(this._cancel);
		  this.Controls.Add(this._tabContainer);
		  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		  this.MaximizeBox = false;
		  this.MinimizeBox = false;
		  this.Name = "Preferences";
		  this.ShowInTaskbar = false;
		  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		  this.Text = "Preferences";
		  this._tabContainer.ResumeLayout(false);
		  this._tabIgnoreList.ResumeLayout(false);
		  this._contextIgnoreList.ResumeLayout(false);
		  this.tabPage1.ResumeLayout(false);
		  this.tabPage1.PerformLayout();
		  this.ResumeLayout(false);

	   }

	   #endregion

	   private System.Windows.Forms.TabControl _tabContainer;
	   private System.Windows.Forms.TabPage _tabIgnoreList;
	   private System.Windows.Forms.Button _cancel;
	   private System.Windows.Forms.Button _ok;
	   private EXControls.EXListView _ignoreList;
	   private System.Windows.Forms.ContextMenuStrip _contextIgnoreList;
	   private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
	   private System.Windows.Forms.TabPage tabPage1;
	   private System.Windows.Forms.CheckBox lbLocalDir;
	   private System.Windows.Forms.CheckBox lbUserDir;
	   private System.Windows.Forms.ListView lstAddtPaths;
	   private System.Windows.Forms.Button btAddPath;
	   private System.Windows.Forms.Button btRemovePath;
	   private System.Windows.Forms.ColumnHeader colPath;
    }
}