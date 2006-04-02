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
 *  Original FileName :  ReviewChanges.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMOz.UI
{
    public partial class ReviewChanges : Form
    {
	   public ReviewChanges(bool list) {
		  InitializeComponent();
		  this.Icon = SMOz.Properties.Resources.Application;

		  this._table.RowStyles.Clear();

		  if (list) {
			 this._table.Hide();
			 this._list.Show();
		  } else {
			 this._list.Hide();
			 this._table.Show();
		  }
	   }

	   Font actionFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
	   Font commentFont = ListView.DefaultFont;

	   public void Add(string action, string comment) {
		  ListViewItem item = new ListViewItem(new string[] { action, comment });
		  item.UseItemStyleForSubItems = false;
		  item.SubItems[0].Font = actionFont;
		  item.SubItems[1].Font = commentFont;
		  _list.Items.Add(item);
	   }

	   public void Add(string action, string source, string target) {
		  ReviewListItem reviewItem = new ReviewListItem(action, source, target);
		  reviewItem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		  this._table.Controls.Add(reviewItem);
		  if (this._table.RowStyles.Count == 1) {
			 reviewItem.Select();
		  }
	   }

	   private void ReviewChanges_Load(object sender, EventArgs e) {
		  if (this._list.Visible) {
			 this._list.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			 this._list.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			 int width = _list.Columns[0].Width + _list.Columns[1].Width + 20 + 36;
			 if (this.Width < width) {
				this.Width = width;
			 }
		  }
	   }

	   public string Message {
		  get { return label1.Text; }
		  set { label1.Text = value; }
	   }
    }
}