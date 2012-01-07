/*************************************************************************
 *  SMOz (Start Menu Organizer)
 *  Copyleft (C) 2006 Nithin Philips
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
 *  Original FileName :  InformationPage.cs
 *  Created           :  Sun Jan 22 2006
 *  Description       :  
 *************************************************************************/

using System;

namespace Nithin.Philips.Utilities.AboutBox
{
	/// <summary>
	/// Summary description for InformationPage.
	/// </summary>
	public class InformationPage
	{
		public InformationPage(string name, string message)
		{
			this.name = name;
			this.message = message;
		}

		string name;
		string message;

		public string Name {
			get { return this.name; }
			set { this.name = value; }
		}

		public string Message {
			get { return this.message; }
			set { this.message = value; }
		}


	}
}
