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
 *  Original FileName :  MatchPreview.Designer.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

namespace SMOz.UI
{
    partial class MatchPreview
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
		  this.PreviewList = new System.Windows.Forms.ListBox();
		  this.SuspendLayout();
		  // 
		  // PreviewList
		  // 
		  this.PreviewList.FormattingEnabled = true;
		  this.PreviewList.Location = new System.Drawing.Point(12, 12);
		  this.PreviewList.Name = "PreviewList";
		  this.PreviewList.Size = new System.Drawing.Size(300, 212);
		  this.PreviewList.TabIndex = 1;
		  // 
		  // MatchPreview
		  // 
		  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		  this.ClientSize = new System.Drawing.Size(324, 240);
		  this.Controls.Add(this.PreviewList);
		  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
		  this.Name = "MatchPreview";
		  this.Text = "Preview";
		  this.ResumeLayout(false);

	   }

	   #endregion

	   public System.Windows.Forms.ListBox PreviewList;
    }
}