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
 *  Original FileName :  ReviewChanges.Designer.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

namespace SMOz.UI
{
    partial class ReviewChanges
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
		  this._ok = new System.Windows.Forms.Button();
		  this._table = new System.Windows.Forms.TableLayoutPanel();
		  this.label1 = new System.Windows.Forms.Label();
		  this._cancel = new System.Windows.Forms.Button();
		  this._list = new System.Windows.Forms.ListView();
		  this._action = new System.Windows.Forms.ColumnHeader();
		  this._comment = new System.Windows.Forms.ColumnHeader();
		  this.SuspendLayout();
		  // 
		  // _ok
		  // 
		  this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		  this._ok.DialogResult = System.Windows.Forms.DialogResult.OK;
		  this._ok.Location = new System.Drawing.Point(372, 238);
		  this._ok.Name = "_ok";
		  this._ok.Size = new System.Drawing.Size(75, 23);
		  this._ok.TabIndex = 1;
		  this._ok.Text = "Continue";
		  this._ok.UseVisualStyleBackColor = true;
		  // 
		  // _table
		  // 
		  this._table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
				    | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this._table.AutoScroll = true;
		  this._table.BackColor = System.Drawing.SystemColors.Window;
		  this._table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
		  this._table.ColumnCount = 1;
		  this._table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
		  this._table.ForeColor = System.Drawing.SystemColors.WindowText;
		  this._table.Location = new System.Drawing.Point(12, 35);
		  this._table.Name = "_table";
		  this._table.RowCount = 1;
		  this._table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
		  this._table.Size = new System.Drawing.Size(434, 197);
		  this._table.TabIndex = 2;
		  // 
		  // label1
		  // 
		  this.label1.Location = new System.Drawing.Point(12, 5);
		  this.label1.Name = "label1";
		  this.label1.Size = new System.Drawing.Size(437, 27);
		  this.label1.TabIndex = 3;
		  this.label1.Text = "SMOz is about to apply the following changes to your Start Menu.";
		  // 
		  // _cancel
		  // 
		  this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		  this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		  this._cancel.Location = new System.Drawing.Point(291, 238);
		  this._cancel.Name = "_cancel";
		  this._cancel.Size = new System.Drawing.Size(75, 23);
		  this._cancel.TabIndex = 4;
		  this._cancel.Text = "Cancel";
		  this._cancel.UseVisualStyleBackColor = true;
		  // 
		  // _list
		  // 
		  this._list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
				    | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this._list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._action,
            this._comment});
		  this._list.FullRowSelect = true;
		  this._list.Location = new System.Drawing.Point(12, 35);
		  this._list.Name = "_list";
		  this._list.Size = new System.Drawing.Size(434, 197);
		  this._list.TabIndex = 5;
		  this._list.UseCompatibleStateImageBehavior = false;
		  this._list.View = System.Windows.Forms.View.Details;
		  // 
		  // _action
		  // 
		  this._action.Text = "Action";
		  this._action.Width = 65;
		  // 
		  // _comment
		  // 
		  this._comment.Text = "Comment";
		  this._comment.Width = 346;
		  // 
		  // ReviewChanges
		  // 
		  this.AcceptButton = this._ok;
		  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		  this.CancelButton = this._cancel;
		  this.ClientSize = new System.Drawing.Size(458, 271);
		  this.Controls.Add(this._list);
		  this.Controls.Add(this._cancel);
		  this.Controls.Add(this.label1);
		  this.Controls.Add(this._table);
		  this.Controls.Add(this._ok);
		  this.MinimizeBox = false;
		  this.Name = "ReviewChanges";
		  this.ShowInTaskbar = false;
		  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		  this.Text = "Review Changes";
		  this.Load += new System.EventHandler(this.ReviewChanges_Load);
		  this.ResumeLayout(false);

	   }

	   #endregion

	   private System.Windows.Forms.TableLayoutPanel _table;
	   private System.Windows.Forms.Label label1;
	   private System.Windows.Forms.Button _cancel;
	   private System.Windows.Forms.ListView _list;
	   private System.Windows.Forms.ColumnHeader _action;
	   private System.Windows.Forms.ColumnHeader _comment;
	   public System.Windows.Forms.Button _ok;
    }
}