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
 *  Original FileName :  ReviewListItem.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SMOz.UI
{
    public partial class ReviewListItem : UserControl
    {
	   public ReviewListItem()
		  : this("Action", "Source", "Target") { }

	   public ReviewListItem(string action, string source, string target) {
		  InitializeComponent();
		  this._actionName.Text = action;
		  this._source.Text = source;
		  this._target.Text = target;
		  this.Paint += new PaintEventHandler(ReviewListItem_Paint);
	   }

	   void ReviewListItem_Paint(object sender, PaintEventArgs e) {
		  
	   }

	   public string Action {
		  get { return this._actionName.Text; }
		  set { this._actionName.Text = value; }
	   }

	   public string Source {
		  get { return this._source.Text; }
		  set { this._source.Text = value; }
	   }

	   public string Target {
		  get { return this._target.Text; }
		  set { this._target.Text = value; }
	   }

	   private void ReviewListItem_Enter(object sender, EventArgs e) {
		  this.BackColor = SystemColors.HighlightText;
//		  VisualStyleRenderer renderer = new VisualStyleRenderer(new VisualStyleElement.Button());
	   }

	   private void ReviewListItem_Leave(object sender, EventArgs e) {
		  this.BackColor = this.Parent.BackColor;
	   }
    }
}
