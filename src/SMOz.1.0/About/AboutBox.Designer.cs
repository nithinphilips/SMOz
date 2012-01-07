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
 *  Original FileName :  AboutBox.Designer.cs
 *  Created           :  Sun Jan 22 2006
 *  Description       :  
 *************************************************************************/

namespace Nithin.Philips.Utilities.AboutBox
{
    partial class AboutBox
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

	   #region Windows Form Designer generated code

	   /// <summary>
	   /// Required method for Designer support - do not modify
	   /// the contents of this method with the code editor.
	   /// </summary>
	   private void InitializeComponent() {
		  this.components = new System.ComponentModel.Container();
		  this.homepagelink = new System.Windows.Forms.LinkLabel();
		  this.okbutton = new System.Windows.Forms.Button();
		  this.messageTabs = new Crownwood.Magic.Controls.TabControl();
		  this.copyright = new System.Windows.Forms.Label();
		  this.tipProvider = new System.Windows.Forms.ToolTip(this.components);
		  this.version = new System.Windows.Forms.Label();
		  this.name = new System.Windows.Forms.Label();
		  this.pictureBox1 = new System.Windows.Forms.PictureBox();
		  this.resizer = new System.Windows.Forms.Timer(this.components);
		  ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
		  this.SuspendLayout();
		  // 
		  // homepagelink
		  // 
		  this.homepagelink.ActiveLinkColor = System.Drawing.Color.RosyBrown;
		  this.homepagelink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		  this.homepagelink.FlatStyle = System.Windows.Forms.FlatStyle.System;
		  this.homepagelink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
		  this.homepagelink.LinkColor = System.Drawing.Color.LemonChiffon;
		  this.homepagelink.Location = new System.Drawing.Point(12, 141);
		  this.homepagelink.Name = "homepagelink";
		  this.homepagelink.Size = new System.Drawing.Size(96, 16);
		  this.homepagelink.TabIndex = 25;
		  this.homepagelink.TabStop = true;
		  this.homepagelink.Text = "Visit Home page";
		  this.homepagelink.Visible = false;
		  this.homepagelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.homepagelink_LinkClicked);
		  // 
		  // okbutton
		  // 
		  this.okbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		  this.okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
		  this.okbutton.FlatStyle = System.Windows.Forms.FlatStyle.System;
		  this.okbutton.Location = new System.Drawing.Point(431, 136);
		  this.okbutton.Name = "okbutton";
		  this.okbutton.Size = new System.Drawing.Size(75, 23);
		  this.okbutton.TabIndex = 24;
		  this.okbutton.Text = "OK";
		  // 
		  // messageTabs
		  // 
		  this.messageTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this.messageTabs.BackColor = System.Drawing.Color.Black;
		  this.messageTabs.BoldSelectedPage = true;
		  this.messageTabs.ButtonActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
		  this.messageTabs.ButtonInactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
		  this.messageTabs.ForeColor = System.Drawing.Color.LavenderBlush;
		  this.messageTabs.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways;
		  this.messageTabs.Location = new System.Drawing.Point(12, 106);
		  this.messageTabs.Name = "messageTabs";
		  this.messageTabs.PositionTop = true;
		  this.messageTabs.ShrinkPagesToFit = false;
		  this.messageTabs.Size = new System.Drawing.Size(494, 24);
		  this.messageTabs.TabIndex = 23;
		  this.messageTabs.TextColor = System.Drawing.Color.LavenderBlush;
		  this.messageTabs.SelectionChanged += new System.EventHandler(this.messageTabs_SelectionChanged);
		  // 
		  // copyright
		  // 
		  this.copyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this.copyright.FlatStyle = System.Windows.Forms.FlatStyle.System;
		  this.copyright.Location = new System.Drawing.Point(108, 88);
		  this.copyright.Name = "copyright";
		  this.copyright.Size = new System.Drawing.Size(398, 14);
		  this.copyright.TabIndex = 22;
		  this.copyright.Text = "(C) Year Author";
		  // 
		  // version
		  // 
		  this.version.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this.version.FlatStyle = System.Windows.Forms.FlatStyle.System;
		  this.version.Location = new System.Drawing.Point(108, 72);
		  this.version.Name = "version";
		  this.version.Size = new System.Drawing.Size(398, 16);
		  this.version.TabIndex = 21;
		  this.version.Text = "Version 0.0.0.0";
		  // 
		  // name
		  // 
		  this.name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this.name.FlatStyle = System.Windows.Forms.FlatStyle.System;
		  this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		  this.name.Location = new System.Drawing.Point(108, 39);
		  this.name.Name = "name";
		  this.name.Size = new System.Drawing.Size(398, 33);
		  this.name.TabIndex = 20;
		  this.name.Text = "Product Name";
		  // 
		  // pictureBox1
		  // 
		  this.pictureBox1.Location = new System.Drawing.Point(12, 12);
		  this.pictureBox1.Name = "pictureBox1";
		  this.pictureBox1.Size = new System.Drawing.Size(90, 90);
		  this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		  this.pictureBox1.TabIndex = 19;
		  this.pictureBox1.TabStop = false;
		  // 
		  // resizer
		  // 
		  this.resizer.Interval = 50;
		  this.resizer.Tick += new System.EventHandler(this.resizer_Tick);
		  // 
		  // AboutBox
		  // 
		  this.AcceptButton = this.okbutton;
		  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		  this.BackColor = System.Drawing.Color.Black;
		  this.CancelButton = this.okbutton;
		  this.ClientSize = new System.Drawing.Size(518, 164);
		  this.Controls.Add(this.homepagelink);
		  this.Controls.Add(this.okbutton);
		  this.Controls.Add(this.messageTabs);
		  this.Controls.Add(this.copyright);
		  this.Controls.Add(this.version);
		  this.Controls.Add(this.name);
		  this.Controls.Add(this.pictureBox1);
		  this.ForeColor = System.Drawing.Color.White;
		  this.HelpButton = true;
		  this.MaximizeBox = false;
		  this.MinimizeBox = false;
		  this.Name = "AboutBox";
		  this.ShowInTaskbar = false;
		  this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
		  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		  this.Text = "AboutBox";
		  ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
		  this.ResumeLayout(false);

	   }

	   #endregion

	   private System.Windows.Forms.LinkLabel homepagelink;
	   private Crownwood.Magic.Controls.TabControl messageTabs;
	   private System.Windows.Forms.Label copyright;
	   private System.Windows.Forms.ToolTip tipProvider;
	   private System.Windows.Forms.Label version;
	   private System.Windows.Forms.Label name;
	   private System.Windows.Forms.PictureBox pictureBox1;
	   private System.Windows.Forms.Timer resizer;
	   public System.Windows.Forms.Button okbutton;
    }
}