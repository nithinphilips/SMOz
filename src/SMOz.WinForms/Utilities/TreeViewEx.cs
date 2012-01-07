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
 *  Original FileName :  TreeViewEx.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SMOz.Utilities
{

    public class TreeViewEx : TreeView
    {
	   public TreeViewEx() {
		  this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		  this.SetStyle(ControlStyles.DoubleBuffer, true);
	   }

	   protected override void WndProc(ref Message messg) {
		  // turn the erase background message into a null message
		  if ((int)0x0014 == messg.Msg) //if message is is erase background
		  {
			 return;
			 //			 messg.Msg = (int) 0x0000; //reset message to null
		  }
		  base.WndProc(ref messg);
	   }

    }
}
