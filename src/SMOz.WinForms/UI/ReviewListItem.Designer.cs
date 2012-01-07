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
 *  Original FileName :  ReviewListItem.Designer.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

namespace SMOz.UI
{
    partial class ReviewListItem
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

	   #region Component Designer generated code

	   /// <summary> 
	   /// Required method for Designer support - do not modify 
	   /// the contents of this method with the code editor.
	   /// </summary>
	   private void InitializeComponent() {
		  this._table = new System.Windows.Forms.TableLayoutPanel();
		  this._actionName = new System.Windows.Forms.Label();
		  this._source = new System.Windows.Forms.Label();
		  this._target = new System.Windows.Forms.Label();
		  this._table.SuspendLayout();
		  this.SuspendLayout();
		  // 
		  // _table
		  // 
		  this._table.AutoSize = true;
		  this._table.ColumnCount = 2;
		  this._table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		  this._table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		  this._table.Controls.Add(this._actionName, 0, 0);
		  this._table.Controls.Add(this._source, 1, 0);
		  this._table.Controls.Add(this._target, 1, 1);
		  this._table.Location = new System.Drawing.Point(3, 3);
		  this._table.Name = "_table";
		  this._table.RowCount = 2;
		  this._table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		  this._table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		  this._table.Size = new System.Drawing.Size(421, 43);
		  this._table.TabIndex = 0;
		  this._table.Enter += new System.EventHandler(this.ReviewListItem_Enter);
		  this._table.Leave += new System.EventHandler(this.ReviewListItem_Leave);
		  // 
		  // _actionName
		  // 
		  this._actionName.AutoSize = true;
		  this._actionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		  this._actionName.Location = new System.Drawing.Point(3, 3);
		  this._actionName.Margin = new System.Windows.Forms.Padding(3);
		  this._actionName.Name = "_actionName";
		  this._actionName.Size = new System.Drawing.Size(61, 15);
		  this._actionName.TabIndex = 1;
		  this._actionName.Text = "Rename";
		  this._actionName.Enter += new System.EventHandler(this.ReviewListItem_Enter);
		  this._actionName.Leave += new System.EventHandler(this.ReviewListItem_Leave);
		  // 
		  // _source
		  // 
		  this._source.AutoSize = true;
		  this._source.Location = new System.Drawing.Point(70, 3);
		  this._source.Margin = new System.Windows.Forms.Padding(3);
		  this._source.Name = "_source";
		  this._source.Size = new System.Drawing.Size(41, 13);
		  this._source.TabIndex = 3;
		  this._source.Text = "Source";
		  this._source.Enter += new System.EventHandler(this.ReviewListItem_Enter);
		  this._source.Leave += new System.EventHandler(this.ReviewListItem_Leave);
		  // 
		  // _target
		  // 
		  this._target.AutoSize = true;
		  this._target.Location = new System.Drawing.Point(70, 24);
		  this._target.Margin = new System.Windows.Forms.Padding(3);
		  this._target.Name = "_target";
		  this._target.Size = new System.Drawing.Size(38, 13);
		  this._target.TabIndex = 2;
		  this._target.Text = "Target";
		  this._target.Enter += new System.EventHandler(this.ReviewListItem_Enter);
		  this._target.Leave += new System.EventHandler(this.ReviewListItem_Leave);
		  // 
		  // ReviewListItem
		  // 
		  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		  this.AutoSize = true;
		  this.Controls.Add(this._table);
		  this.Name = "ReviewListItem";
		  this.Size = new System.Drawing.Size(427, 49);
		  this.Enter += new System.EventHandler(this.ReviewListItem_Enter);
		  this.Leave += new System.EventHandler(this.ReviewListItem_Leave);
		  this._table.ResumeLayout(false);
		  this._table.PerformLayout();
		  this.ResumeLayout(false);
		  this.PerformLayout();

	   }

	   #endregion

	   private System.Windows.Forms.TableLayoutPanel _table;
	   private System.Windows.Forms.Label _actionName;
	   private System.Windows.Forms.Label _source;
	   private System.Windows.Forms.Label _target;

    }
}
