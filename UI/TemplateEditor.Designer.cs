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
		  System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateEditor));
		  this._searchQuery = new System.Windows.Forms.TextBox();
		  this._searchLabel = new System.Windows.Forms.Label();
		  this._previewAutoHide = new System.Windows.Forms.CheckBox();
		  this._showPreview = new System.Windows.Forms.CheckBox();
		  this._templateList = new EXControls.EXListView();
		  this._add = new System.Windows.Forms.Button();
		  this.SuspendLayout();
		  // 
		  // _searchQuery
		  // 
		  this._searchQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		  this._searchQuery.Location = new System.Drawing.Point(461, 10);
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
		  this._searchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		  this._searchLabel.AutoSize = true;
		  this._searchLabel.BackColor = System.Drawing.SystemColors.Window;
		  this._searchLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
		  this._searchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		  this._searchLabel.ForeColor = System.Drawing.Color.Gray;
		  this._searchLabel.Location = new System.Drawing.Point(598, 13);
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
		  this._previewAutoHide.Location = new System.Drawing.Point(93, 334);
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
		  this._showPreview.Location = new System.Drawing.Point(12, 330);
		  this._showPreview.Name = "_showPreview";
		  this._showPreview.Size = new System.Drawing.Size(75, 24);
		  this._showPreview.TabIndex = 10;
		  this._showPreview.Text = "Preview";
		  this._showPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		  this._showPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		  this._showPreview.UseVisualStyleBackColor = true;
		  this._showPreview.Click += new System.EventHandler(this._showPreview_Click);
		  this._showPreview.CheckedChanged += new System.EventHandler(this._showPreview_CheckedChanged);
		  // 
		  // _templateList
		  // 
		  this._templateList.AllowColumnSort = false;
		  this._templateList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
				    | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this._templateList.FullRowSelect = true;
		  this._templateList.Location = new System.Drawing.Point(12, 36);
		  this._templateList.MultiSelect = false;
		  this._templateList.Name = "_templateList";
		  this._templateList.OwnerDraw = true;
		  this._templateList.Size = new System.Drawing.Size(633, 288);
		  this._templateList.TabIndex = 2;
		  this._templateList.UseCompatibleStateImageBehavior = false;
		  this._templateList.View = System.Windows.Forms.View.Details;
		  this._templateList.DoubleClick += new System.EventHandler(this._templateList_DoubleClick);
		  this._templateList.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this._templateList_AfterLabelEdit);
		  this._templateList.KeyUp += new System.Windows.Forms.KeyEventHandler(this._templateList_KeyUp);
		  // 
		  // _add
		  // 
		  this._add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		  this._add.Image = ((System.Drawing.Image)(resources.GetObject("_add.Image")));
		  this._add.Location = new System.Drawing.Point(570, 330);
		  this._add.Name = "_add";
		  this._add.Size = new System.Drawing.Size(75, 25);
		  this._add.TabIndex = 1;
		  this._add.Text = "New";
		  this._add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		  this._add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		  this._add.UseVisualStyleBackColor = true;
		  this._add.Click += new System.EventHandler(this._add_Click);
		  // 
		  // TemplateEditor
		  // 
		  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		  this.ClientSize = new System.Drawing.Size(657, 361);
		  this.Controls.Add(this._showPreview);
		  this.Controls.Add(this._previewAutoHide);
		  this.Controls.Add(this._searchLabel);
		  this.Controls.Add(this._searchQuery);
		  this.Controls.Add(this._add);
		  this.Controls.Add(this._templateList);
		  this.MinimizeBox = false;
		  this.Name = "TemplateEditor";
		  this.ShowInTaskbar = false;
		  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		  this.Text = "Template Editor";
		  this.ResumeLayout(false);
		  this.PerformLayout();

	   }

	   #endregion

	   private EXControls.EXListView _templateList;
	   private System.Windows.Forms.TextBox _searchQuery;
	   private System.Windows.Forms.Label _searchLabel;
	   private System.Windows.Forms.CheckBox _previewAutoHide;
	   private System.Windows.Forms.CheckBox _showPreview;
	   private System.Windows.Forms.Button _add;
    }
}