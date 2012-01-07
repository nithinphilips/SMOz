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
 *  Original FileName :  AboutBox.cs
 *  Created           :  Sun Jan 22 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace Nithin.Philips.Utilities.AboutBox
{
    public partial class AboutBox : Form
    {
	   public AboutBox() {
		  InitializeComponent();
		  this.message = new Label();
		  message.Width = this.messageTabs.ClientSize.Width;
		  message.Dock = DockStyle.Fill;
		  this.Load += new EventHandler(AboutBox_Load);
	   }

	   //>>>>> Don't forget to set AboutBox.Icon property <<<<<<

	   #region Properties

	   private string productName = string.Empty;
	   private string productVersion = string.Empty;
	   private string productCopyright = string.Empty;
	   private string productUrl = string.Empty;
	   private bool standAlone = false;

	   private List<InformationPage> pageCollection;
	   private System.Windows.Forms.Label message;

	   /* Copyright String
	    * ex: (c) 2005 Joe Schmo
	    ---------------------------------------------------------- */
	   public string ProductCopyright {
		  get { return this.productCopyright; }
		  set { this.productCopyright = value; }
	   }

	   /* Name of the product
	    ---------------------------------------------------------- */
	   public new string ProductName {
		  get { return this.productName; }
		  set { this.productName = value; }
	   }

	   /* Version (a string 'Version' is prepended before display)
	    * ex: 0.9.3.4
	    ---------------------------------------------------------- */
	   public new string ProductVersion {
		  get { return this.productVersion; }
		  set { this.productVersion = value; }
	   }

	   /* Pages to show; a page has a name and a message
	    ---------------------------------------------------------- */
	   public List<InformationPage> PageCollection {
		  get { return this.pageCollection; }
		  set { this.pageCollection = value; }
	   }

	   /* Url to the product website; optional
	    * if value is not set, the link is not shown
	    ---------------------------------------------------------- */
	   public string ProductUrl {
		  get { return this.productUrl; }
		  set { this.productUrl = value; }
	   }

	   public bool StandAlone {
		  get { return this.standAlone; }
		  set { this.standAlone = value; }
	   }

	   /* 90x90 (1:1 aspect ratio) image of the product icon;
	    * optional
	    ---------------------------------------------------------- */
	   public Image ProductLargeIcon {
		  get { return this.pictureBox1.Image; }
		  set { this.pictureBox1.Image = value; }
	   } 

	   #endregion

	   #region Methods

	   /* Adds a new tab page
	    ---------------------------------------------------------- */
	   private Crownwood.Magic.Controls.TabPage AddTabPage(string name) {
		  Crownwood.Magic.Controls.TabPage newPage = new Crownwood.Magic.Controls.TabPage(name);
		  this.messageTabs.TabPages.Add(newPage);
		  return newPage;
	   }

	   /* Creates tabs and sets proper values for labels
	    ---------------------------------------------------------- */
	   private void InitUI() {
		  if (pageCollection.Count <= 0) {
			 throw new ArgumentException("There must be at least one page", "PageCollection");
		  }

		  this.Text = "About " + this.ProductName;
		  this.name.Text = this.ProductName;
		  this.version.Text = "Version " + this.ProductVersion;
		  this.copyright.Text = this.ProductCopyright;

		  if (this.ProductUrl != string.Empty) {
			 homepagelink.Show();
			 tipProvider.SetToolTip(homepagelink, this.ProductUrl);
		  }

		  foreach (InformationPage ipage in pageCollection) {
			 AddTabPage(ipage.Name);
		  }

		  message.Text = pageCollection[0].Message;
		  UpdateUI(false);
		  this.CenterToParent();
	   }

	   /* Changes window size to accomodate changes
	    ---------------------------------------------------------- */
	   private void UpdateUI(bool animate) {
		  Graphics g = this.CreateGraphics();
		  SizeF size = g.MeasureString(message.Text, message.Font, message.Width, StringFormat.GenericDefault);
		  messageTabs.Height = messageTabs.TabsAreaRect.Height + (int)(size.Height + 0.5);
		  ResizeHeight(messageTabs.Top + messageTabs.Height + okbutton.Height + 45, animate);
	   }

	   /* Resizes widow and fades-in text
	    ---------------------------------------------------------- */
	   private void ResizeHeight(int height, bool animate) {
		  TargetHeight = height;
		  if (TargetHeight < this.Height) {
			 ResizeExpand = false;
		  } else {
			 ResizeExpand = true;
		  }
		  if ((this.Top + height) > Screen.FromControl(this).WorkingArea.Height) {
			 this.Top = Screen.FromControl(this).WorkingArea.Height - height;
		  }


		  if (animate) {
			 int approxSteps = (int)((((this.Height > height) ? (this.Height - height) : (height - this.Height)) / 5d) - 0.5);
			 fadeInColors = GetGradientColors(Color.Black, Color.LavenderBlush, approxSteps);
			 resizer.Start();
		  } else {
			 this.Height = height;
		  }
	   } 

	   #endregion

	   #region Event Handlers

	   private int TargetHeight;
	   private bool ResizeExpand = true;
	   List<Color> fadeInColors;
	   int colorCount = 0;

	   /* Timer tick during which window is resized and text is 
	    * faded-in
	    ---------------------------------------------------------- */
	   private void resizer_Tick(object sender, System.EventArgs e) {
		  int minInterval = 1;
		  int step = 5;

		  if (resizer.Interval > minInterval) {
			 resizer.Interval = (int)(resizer.Interval * 0.90); // logarithmic resize
		  }

		  if (colorCount < fadeInColors.Count) {
			 message.ForeColor = fadeInColors[colorCount];
		  }

		  if (ResizeExpand) {
			 if (this.Height >= TargetHeight) { goto StopTimer; }
			 this.Height += step;
		  } else {
			 if (this.Height <= TargetHeight) { goto StopTimer; }
			 this.Height -= step;
		  }

		  colorCount++;

		  return;
	   StopTimer:
		  resizer.Stop();
		  resizer.Interval = 50;
		  colorCount = 0;
		  message.ForeColor = fadeInColors[fadeInColors.Count - 1];
		  fadeInColors = null;
	   }

	   /* Shows appropriate message in the currently selected tab
	    ---------------------------------------------------------- */
	   private void messageTabs_SelectionChanged(object sender, System.EventArgs e) {
		  message.Hide();

		  message.ForeColor = Color.Black;
		  messageTabs.SelectedTab.Controls.Add(message);
		  int index = messageTabs.SelectedIndex;
		  message.Text = pageCollection[index].Message;
		  this.Text = "About " + this.ProductName + " - " + pageCollection[index].Name;
		  UpdateUI(true);

		  message.Show();
	   }

	   /* Initializes control on form load
	    ---------------------------------------------------------- */
	   private void AboutBox_Load(object sender, System.EventArgs e) {
		  InitUI();
		  if (this.StandAlone) {
			 this.CenterToScreen();
			 this.ShowInTaskbar = true;
		  }
	   }

	   /* Launches homepage url in default web browser
	    ---------------------------------------------------------- */
	   private void homepagelink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
		  Process.Start(this.productUrl);
	   } 

	   #endregion

	   #region Color Gradient

	   /* Creates <step> number of colors, so that a fade effect 
	    * can be simulated on a label control.
	    ---------------------------------------------------------- */
	   public List<Color> GetGradientColors(Color start, Color end, int count) {
		  List<Color> cList = new List<Color>(count);
		  if (count <= 1) {
			 cList.Add(end);
		  } else {
			 Rectangle rect = new Rectangle(0, 0, count, 1);
			 using (LinearGradientBrush brush = new LinearGradientBrush(new Point(rect.X, rect.Y), new Point(rect.Width, rect.Height), start, end)) {
				using (Bitmap bitmap = new Bitmap(rect.Width, rect.Height)) {
				    using (Graphics g = Graphics.FromImage(bitmap)) {
					   g.FillRectangle(brush, rect);
					   for (int x = 0; x < bitmap.Width; x++) {
						  cList.Add(bitmap.GetPixel(x, 0));
					   }
					   cList.Add(end);
				    }
				}
			 }
		  }
		  return cList;
	   }

	   #endregion
    }
}