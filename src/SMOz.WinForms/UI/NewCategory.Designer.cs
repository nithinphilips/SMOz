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
 *  Original FileName :  NewCategory.Designer.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

namespace SMOz.UI
{
    partial class NewCategory
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
		  this._categoryName = new System.Windows.Forms.TextBox();
		  this._cancel = new System.Windows.Forms.Button();
		  this._ok = new System.Windows.Forms.Button();
		  this.pictureBox1 = new System.Windows.Forms.PictureBox();
		  ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
		  this.SuspendLayout();
		  // 
		  // _categoryName
		  // 
		  this._categoryName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this._categoryName.Location = new System.Drawing.Point(34, 12);
		  this._categoryName.Name = "_categoryName";
		  this._categoryName.Size = new System.Drawing.Size(324, 20);
		  this._categoryName.TabIndex = 0;
		  // 
		  // _cancel
		  // 
		  this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		  this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		  this._cancel.Location = new System.Drawing.Point(202, 38);
		  this._cancel.Name = "_cancel";
		  this._cancel.Size = new System.Drawing.Size(75, 23);
		  this._cancel.TabIndex = 1;
		  this._cancel.Text = "Cancel";
		  this._cancel.UseVisualStyleBackColor = true;
		  // 
		  // _ok
		  // 
		  this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		  this._ok.DialogResult = System.Windows.Forms.DialogResult.OK;
		  this._ok.Location = new System.Drawing.Point(283, 38);
		  this._ok.Name = "_ok";
		  this._ok.Size = new System.Drawing.Size(75, 23);
		  this._ok.TabIndex = 2;
		  this._ok.Text = "OK";
		  this._ok.UseVisualStyleBackColor = true;
		  this._ok.Click += new System.EventHandler(this._ok_Click);
		  // 
		  // pictureBox1
		  // 
		  this.pictureBox1.Image = global::SMOz.Properties.Resources.File_NewCategory;
		  this.pictureBox1.Location = new System.Drawing.Point(12, 14);
		  this.pictureBox1.Name = "pictureBox1";
		  this.pictureBox1.Size = new System.Drawing.Size(16, 16);
		  this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
		  this.pictureBox1.TabIndex = 3;
		  this.pictureBox1.TabStop = false;
		  // 
		  // NewCategory
		  // 
		  this.AcceptButton = this._ok;
		  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		  this.CancelButton = this._cancel;
		  this.ClientSize = new System.Drawing.Size(370, 69);
		  this.Controls.Add(this.pictureBox1);
		  this.Controls.Add(this._ok);
		  this.Controls.Add(this._cancel);
		  this.Controls.Add(this._categoryName);
		  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
		  this.Name = "NewCategory";
		  this.Opacity = 0.9;
		  this.ShowIcon = false;
		  this.ShowInTaskbar = false;
		  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		  this.Text = "Create New Category";
		  ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
		  this.ResumeLayout(false);
		  this.PerformLayout();

	   }

	   #endregion

	   private System.Windows.Forms.TextBox _categoryName;
	   private System.Windows.Forms.Button _cancel;
	   private System.Windows.Forms.Button _ok;
	   private System.Windows.Forms.PictureBox pictureBox1;
    }
}