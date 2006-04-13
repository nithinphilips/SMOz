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
		  this._ignoreList = new EXControls.EXListView();
		  this._contextIgnoreList = new System.Windows.Forms.ContextMenuStrip(this.components);
		  this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this._cancel = new System.Windows.Forms.Button();
		  this._ok = new System.Windows.Forms.Button();
		  this._tabContainer.SuspendLayout();
		  this._tabIgnoreList.SuspendLayout();
		  this._contextIgnoreList.SuspendLayout();
		  this.SuspendLayout();
		  // 
		  // _tabContainer
		  // 
		  this._tabContainer.Controls.Add(this._tabIgnoreList);
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
    }
}