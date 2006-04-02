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
 *  Original FileName :  NewCategory.cs
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
using System.IO;
using SMOz.Utilities;

namespace SMOz.UI
{
    public partial class NewCategory : Form
    {
	   public NewCategory(string suggested) {
		  InitializeComponent();
		  this.Icon = SMOz.Properties.Resources.Application;

		  this._categoryName.Text = suggested;
	   }

	   public string CategoryName {
		  get { return _categoryName.Text; }
		  set { _categoryName.Text = value; }
	   }

	   private void _ok_Click(object sender, EventArgs e) {
		  // validate
		  if(string.IsNullOrEmpty(this._categoryName.Text)){
			 MessageBox.Show("Sorry, You must enter the name for a new category!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			 this.DialogResult = DialogResult.None;
		  }else{

			 if (!Utility.IsValidPath(this._categoryName.Text)) {
				MessageBox.Show("Sorry, You must enter the valid name for a new category!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.DialogResult = DialogResult.None;
				return;
			 }

			 // Remove leading path seperator
			 if (this._categoryName.Text[0] == Path.DirectorySeparatorChar) {
				this._categoryName.Text = this._categoryName.Text.Substring(1, this._categoryName.Text.Length - 1);
			 }

			 if (string.IsNullOrEmpty(this._categoryName.Text)) {
				MessageBox.Show("Sorry, You must enter the valid name for a new category!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.DialogResult = DialogResult.None;
				return;
			 }


			 // Remove trailing path seperator
			 if (this._categoryName.Text[this._categoryName.Text.Length - 1] == Path.DirectorySeparatorChar) {
				this._categoryName.Text = this._categoryName.Text.Substring(0, this._categoryName.Text.Length - 1);
			 }

			 if (string.IsNullOrEmpty(this._categoryName.Text)) {
				MessageBox.Show("Sorry, You must enter the valid name for a new category!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.DialogResult = DialogResult.None;
				return;
			 }

		  }
	   }
    }
}