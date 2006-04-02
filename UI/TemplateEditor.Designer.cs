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
		  this._templateList = new System.Windows.Forms.ListView();
		  this._patternCol = new System.Windows.Forms.ColumnHeader();
		  this._typeCol = new System.Windows.Forms.ColumnHeader();
		  this._pattern = new System.Windows.Forms.ComboBox();
		  this._category = new System.Windows.Forms.ComboBox();
		  this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		  this._searchQuery = new System.Windows.Forms.TextBox();
		  this._searchLabel = new System.Windows.Forms.Label();
		  this._previewAutoHide = new System.Windows.Forms.CheckBox();
		  this.pictureBox1 = new System.Windows.Forms.PictureBox();
		  this._add = new System.Windows.Forms.Button();
		  this._showPreview = new System.Windows.Forms.CheckBox();
		  this.splitContainer1.Panel1.SuspendLayout();
		  this.splitContainer1.Panel2.SuspendLayout();
		  this.splitContainer1.SuspendLayout();
		  ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
		  this.SuspendLayout();
		  // 
		  // _templateList
		  // 
		  this._templateList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
				    | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this._templateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._patternCol,
            this._typeCol});
		  this._templateList.FullRowSelect = true;
		  this._templateList.LabelEdit = true;
		  this._templateList.Location = new System.Drawing.Point(12, 42);
		  this._templateList.MultiSelect = false;
		  this._templateList.Name = "_templateList";
		  this._templateList.Size = new System.Drawing.Size(633, 285);
		  this._templateList.TabIndex = 2;
		  this._templateList.UseCompatibleStateImageBehavior = false;
		  this._templateList.View = System.Windows.Forms.View.Details;
		  this._templateList.DoubleClick += new System.EventHandler(this._templateList_DoubleClick);
		  this._templateList.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this._templateList_AfterLabelEdit);
		  this._templateList.KeyUp += new System.Windows.Forms.KeyEventHandler(this._templateList_KeyUp);
		  // 
		  // _patternCol
		  // 
		  this._patternCol.Text = "Pattern";
		  this._patternCol.Width = 492;
		  // 
		  // _typeCol
		  // 
		  this._typeCol.Text = "Type";
		  this._typeCol.Width = 114;
		  // 
		  // _pattern
		  // 
		  this._pattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this._pattern.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
		  this._pattern.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		  this._pattern.FormattingEnabled = true;
		  this._pattern.Location = new System.Drawing.Point(3, 3);
		  this._pattern.Name = "_pattern";
		  this._pattern.Size = new System.Drawing.Size(271, 21);
		  this._pattern.TabIndex = 0;
		  this._pattern.Leave += new System.EventHandler(this._pattern_Leave);
		  this._pattern.Enter += new System.EventHandler(this._pattern_Enter);
		  this._pattern.TextChanged += new System.EventHandler(this._pattern_TextChanged);
		  // 
		  // _category
		  // 
		  this._category.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this._category.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		  this._category.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		  this._category.FormattingEnabled = true;
		  this._category.Location = new System.Drawing.Point(3, 3);
		  this._category.Name = "_category";
		  this._category.Size = new System.Drawing.Size(265, 21);
		  this._category.TabIndex = 0;
		  // 
		  // splitContainer1
		  // 
		  this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this.splitContainer1.Location = new System.Drawing.Point(12, 10);
		  this.splitContainer1.Name = "splitContainer1";
		  // 
		  // splitContainer1.Panel1
		  // 
		  this.splitContainer1.Panel1.Controls.Add(this._pattern);
		  // 
		  // splitContainer1.Panel2
		  // 
		  this.splitContainer1.Panel2.Controls.Add(this._category);
		  this.splitContainer1.Size = new System.Drawing.Size(552, 26);
		  this.splitContainer1.SplitterDistance = 277;
		  this.splitContainer1.TabIndex = 0;
		  // 
		  // _searchQuery
		  // 
		  this._searchQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		  this._searchQuery.Location = new System.Drawing.Point(454, 333);
		  this._searchQuery.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
		  this._searchQuery.Name = "_searchQuery";
		  this._searchQuery.Size = new System.Drawing.Size(191, 20);
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
		  this._searchLabel.Location = new System.Drawing.Point(589, 336);
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
		  // pictureBox1
		  // 
		  this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		  this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
		  this.pictureBox1.Image = global::SMOz.Properties.Resources.Edit_Find_Big;
		  this.pictureBox1.Location = new System.Drawing.Point(455, 334);
		  this.pictureBox1.Name = "pictureBox1";
		  this.pictureBox1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
		  this.pictureBox1.Size = new System.Drawing.Size(21, 18);
		  this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		  this.pictureBox1.TabIndex = 7;
		  this.pictureBox1.TabStop = false;
		  // 
		  // _add
		  // 
		  this._add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		  this._add.Image = ((System.Drawing.Image)(resources.GetObject("_add.Image")));
		  this._add.Location = new System.Drawing.Point(570, 10);
		  this._add.Name = "_add";
		  this._add.Size = new System.Drawing.Size(75, 25);
		  this._add.TabIndex = 1;
		  this._add.Text = "Add";
		  this._add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		  this._add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		  this._add.UseVisualStyleBackColor = true;
		  this._add.Click += new System.EventHandler(this._add_Click);
		  // 
		  // _showPreview
		  // 
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
		  // TemplateEditor
		  // 
		  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		  this.ClientSize = new System.Drawing.Size(657, 361);
		  this.Controls.Add(this._showPreview);
		  this.Controls.Add(this._previewAutoHide);
		  this.Controls.Add(this.pictureBox1);
		  this.Controls.Add(this._searchLabel);
		  this.Controls.Add(this._searchQuery);
		  this.Controls.Add(this.splitContainer1);
		  this.Controls.Add(this._add);
		  this.Controls.Add(this._templateList);
		  this.Name = "TemplateEditor";
		  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		  this.Text = "Template Editor";
		  this.splitContainer1.Panel1.ResumeLayout(false);
		  this.splitContainer1.Panel2.ResumeLayout(false);
		  this.splitContainer1.ResumeLayout(false);
		  ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
		  this.ResumeLayout(false);
		  this.PerformLayout();

	   }

	   #endregion

	   private System.Windows.Forms.ListView _templateList;
	   private System.Windows.Forms.ColumnHeader _patternCol;
	   private System.Windows.Forms.ColumnHeader _typeCol;
	   private System.Windows.Forms.Button _add;
	   private System.Windows.Forms.ComboBox _pattern;
	   private System.Windows.Forms.ComboBox _category;
	   private System.Windows.Forms.SplitContainer splitContainer1;
	   private System.Windows.Forms.TextBox _searchQuery;
	   private System.Windows.Forms.Label _searchLabel;
	   private System.Windows.Forms.PictureBox pictureBox1;
	   private System.Windows.Forms.CheckBox _previewAutoHide;
	   private System.Windows.Forms.CheckBox _showPreview;
    }
}