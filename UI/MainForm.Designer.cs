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
 *  Original FileName :  MainForm.Designer.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

namespace SMOz.UI
{
    partial class MainForm
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
		  System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
		  this.menuStrip1 = new System.Windows.Forms.MenuStrip();
		  this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.openTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.saveTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.saveTemplateAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.saveCurrentConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
		  this.mergeTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
		  this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
		  this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
		  this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
		  this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.categoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.addCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.templateEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.applyTemplateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		  this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
		  this.applyChangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.cleanupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.associateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.validateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		  this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this._statusStrip = new System.Windows.Forms.StatusStrip();
		  this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
		  this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		  this._searchOptions = new System.Windows.Forms.ToolStripDropDownButton();
		  this._searchOptionsContext = new System.Windows.Forms.ContextMenuStrip(this.components);
		  this.selectedCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
		  this.regexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this._searchText = new System.Windows.Forms.ToolStripTextBox();
		  this._newTemplate = new System.Windows.Forms.ToolStripButton();
		  this._open = new System.Windows.Forms.ToolStripButton();
		  this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		  this._cutButton = new System.Windows.Forms.ToolStripButton();
		  this._pasteButton = new System.Windows.Forms.ToolStripButton();
		  this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		  this._undoButton = new System.Windows.Forms.ToolStripSplitButton();
		  this._redoButton = new System.Windows.Forms.ToolStripSplitButton();
		  this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		  this._toggleView = new System.Windows.Forms.ToolStripSplitButton();
		  this.tileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.iconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.smallIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		  this._applyChangesButton = new System.Windows.Forms.ToolStripButton();
		  this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		  this._itemList = new System.Windows.Forms.ListView();
		  this._colName = new System.Windows.Forms.ColumnHeader();
		  this._colLocation = new System.Windows.Forms.ColumnHeader();
		  this._itemListContext = new System.Windows.Forms.ContextMenuStrip(this.components);
		  this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
		  this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		  this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
		  this.convertToCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this._categoryTreeContext = new System.Windows.Forms.ContextMenuStrip(this.components);
		  this.newCategoryHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
		  this.applyTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this._categoryTree = new SMOz.Utilities.TreeViewEx();
		  this._applyTemplateButton = new System.Windows.Forms.ToolStripButton();
		  this.menuStrip1.SuspendLayout();
		  this._statusStrip.SuspendLayout();
		  this.toolStrip1.SuspendLayout();
		  this._searchOptionsContext.SuspendLayout();
		  this.splitContainer1.Panel1.SuspendLayout();
		  this.splitContainer1.Panel2.SuspendLayout();
		  this.splitContainer1.SuspendLayout();
		  this._itemListContext.SuspendLayout();
		  this._categoryTreeContext.SuspendLayout();
		  this.SuspendLayout();
		  // 
		  // menuStrip1
		  // 
		  this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.categoryToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
		  this.menuStrip1.Location = new System.Drawing.Point(0, 0);
		  this.menuStrip1.Name = "menuStrip1";
		  this.menuStrip1.Size = new System.Drawing.Size(685, 24);
		  this.menuStrip1.TabIndex = 0;
		  this.menuStrip1.Text = "menuStrip1";
		  // 
		  // fileToolStripMenuItem
		  // 
		  this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTemplateToolStripMenuItem,
            this.saveTemplateToolStripMenuItem,
            this.saveTemplateAsToolStripMenuItem,
            this.saveCurrentConfigurationToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mergeTemplateToolStripMenuItem,
            this.toolStripMenuItem4,
            this.exitToolStripMenuItem});
		  this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
		  this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
		  this.fileToolStripMenuItem.Text = "File";
		  // 
		  // openTemplateToolStripMenuItem
		  // 
		  this.openTemplateToolStripMenuItem.Image = global::SMOz.Properties.Resources.File_Open;
		  this.openTemplateToolStripMenuItem.Name = "openTemplateToolStripMenuItem";
		  this.openTemplateToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
		  this.openTemplateToolStripMenuItem.Text = "Open Template";
		  this.openTemplateToolStripMenuItem.Click += new System.EventHandler(this.openTemplateToolStripMenuItem_Click);
		  // 
		  // saveTemplateToolStripMenuItem
		  // 
		  this.saveTemplateToolStripMenuItem.Name = "saveTemplateToolStripMenuItem";
		  this.saveTemplateToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
		  this.saveTemplateToolStripMenuItem.Text = "Save Template";
		  // 
		  // saveTemplateAsToolStripMenuItem
		  // 
		  this.saveTemplateAsToolStripMenuItem.Image = global::SMOz.Properties.Resources.File_Save;
		  this.saveTemplateAsToolStripMenuItem.Name = "saveTemplateAsToolStripMenuItem";
		  this.saveTemplateAsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
		  this.saveTemplateAsToolStripMenuItem.Text = "Save Template As...";
		  // 
		  // saveCurrentConfigurationToolStripMenuItem
		  // 
		  this.saveCurrentConfigurationToolStripMenuItem.Name = "saveCurrentConfigurationToolStripMenuItem";
		  this.saveCurrentConfigurationToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
		  this.saveCurrentConfigurationToolStripMenuItem.Text = "Save Current Configuration...";
		  this.saveCurrentConfigurationToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentConfigurationToolStripMenuItem_Click);
		  // 
		  // toolStripMenuItem1
		  // 
		  this.toolStripMenuItem1.Name = "toolStripMenuItem1";
		  this.toolStripMenuItem1.Size = new System.Drawing.Size(226, 6);
		  // 
		  // mergeTemplateToolStripMenuItem
		  // 
		  this.mergeTemplateToolStripMenuItem.Name = "mergeTemplateToolStripMenuItem";
		  this.mergeTemplateToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
		  this.mergeTemplateToolStripMenuItem.Text = "Merge Templates";
		  // 
		  // toolStripMenuItem4
		  // 
		  this.toolStripMenuItem4.Name = "toolStripMenuItem4";
		  this.toolStripMenuItem4.Size = new System.Drawing.Size(226, 6);
		  // 
		  // exitToolStripMenuItem
		  // 
		  this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
		  this.exitToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
		  this.exitToolStripMenuItem.Text = "Exit";
		  this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
		  // 
		  // editToolStripMenuItem
		  // 
		  this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripMenuItem2,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem3,
            this.selectAllToolStripMenuItem,
            this.toolStripMenuItem10,
            this.preferencesToolStripMenuItem});
		  this.editToolStripMenuItem.Name = "editToolStripMenuItem";
		  this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
		  this.editToolStripMenuItem.Text = "Edit";
		  // 
		  // undoToolStripMenuItem
		  // 
		  this.undoToolStripMenuItem.Image = global::SMOz.Properties.Resources.Edit_Undo;
		  this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
		  this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
		  this.undoToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
		  this.undoToolStripMenuItem.Text = "Undo";
		  this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
		  // 
		  // redoToolStripMenuItem
		  // 
		  this.redoToolStripMenuItem.Image = global::SMOz.Properties.Resources.Edit_Redo;
		  this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
		  this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
		  this.redoToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
		  this.redoToolStripMenuItem.Text = "Redo";
		  this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
		  // 
		  // toolStripMenuItem2
		  // 
		  this.toolStripMenuItem2.Name = "toolStripMenuItem2";
		  this.toolStripMenuItem2.Size = new System.Drawing.Size(164, 6);
		  // 
		  // cutToolStripMenuItem
		  // 
		  this.cutToolStripMenuItem.Image = global::SMOz.Properties.Resources.Edit_Cut;
		  this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
		  this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
		  this.cutToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
		  this.cutToolStripMenuItem.Text = "Cut";
		  this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
		  // 
		  // pasteToolStripMenuItem
		  // 
		  this.pasteToolStripMenuItem.Image = global::SMOz.Properties.Resources.Edit_Paste;
		  this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
		  this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
		  this.pasteToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
		  this.pasteToolStripMenuItem.Text = "Paste";
		  this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
		  // 
		  // deleteToolStripMenuItem
		  // 
		  this.deleteToolStripMenuItem.Image = global::SMOz.Properties.Resources.Edit_Delete;
		  this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
		  this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
		  this.deleteToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
		  this.deleteToolStripMenuItem.Text = "Delete";
		  this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
		  // 
		  // toolStripMenuItem3
		  // 
		  this.toolStripMenuItem3.Name = "toolStripMenuItem3";
		  this.toolStripMenuItem3.Size = new System.Drawing.Size(164, 6);
		  // 
		  // selectAllToolStripMenuItem
		  // 
		  this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
		  this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
		  this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
		  this.selectAllToolStripMenuItem.Text = "Select All";
		  this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
		  // 
		  // toolStripMenuItem10
		  // 
		  this.toolStripMenuItem10.Name = "toolStripMenuItem10";
		  this.toolStripMenuItem10.Size = new System.Drawing.Size(164, 6);
		  // 
		  // preferencesToolStripMenuItem
		  // 
		  this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
		  this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
		  this.preferencesToolStripMenuItem.Text = "Preferences";
		  this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
		  // 
		  // categoryToolStripMenuItem
		  // 
		  this.categoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCategoryToolStripMenuItem});
		  this.categoryToolStripMenuItem.Name = "categoryToolStripMenuItem";
		  this.categoryToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
		  this.categoryToolStripMenuItem.Text = "Category";
		  // 
		  // addCategoryToolStripMenuItem
		  // 
		  this.addCategoryToolStripMenuItem.Image = global::SMOz.Properties.Resources.File_NewCategory;
		  this.addCategoryToolStripMenuItem.Name = "addCategoryToolStripMenuItem";
		  this.addCategoryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
		  this.addCategoryToolStripMenuItem.Text = "Add Category";
		  this.addCategoryToolStripMenuItem.Click += new System.EventHandler(this.addCategoryToolStripMenuItem_Click);
		  // 
		  // toolsToolStripMenuItem
		  // 
		  this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.templateEditorToolStripMenuItem,
            this.applyTemplateToolStripMenuItem1,
            this.toolStripMenuItem8,
            this.applyChangesToolStripMenuItem,
            this.cleanupToolStripMenuItem,
            this.associateToolStripMenuItem});
		  this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
		  this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
		  this.toolsToolStripMenuItem.Text = "Tools";
		  // 
		  // templateEditorToolStripMenuItem
		  // 
		  this.templateEditorToolStripMenuItem.Name = "templateEditorToolStripMenuItem";
		  this.templateEditorToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
		  this.templateEditorToolStripMenuItem.Text = "Edit Template";
		  this.templateEditorToolStripMenuItem.Click += new System.EventHandler(this.templateEditorToolStripMenuItem_Click);
		  // 
		  // applyTemplateToolStripMenuItem1
		  // 
		  this.applyTemplateToolStripMenuItem1.Image = global::SMOz.Properties.Resources.Tools_Organize;
		  this.applyTemplateToolStripMenuItem1.Name = "applyTemplateToolStripMenuItem1";
		  this.applyTemplateToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
		  this.applyTemplateToolStripMenuItem1.Text = "Apply Template";
		  this.applyTemplateToolStripMenuItem1.Click += new System.EventHandler(this.applyTemplateToolStripMenuItem1_Click);
		  // 
		  // toolStripMenuItem8
		  // 
		  this.toolStripMenuItem8.Name = "toolStripMenuItem8";
		  this.toolStripMenuItem8.Size = new System.Drawing.Size(156, 6);
		  // 
		  // applyChangesToolStripMenuItem
		  // 
		  this.applyChangesToolStripMenuItem.Name = "applyChangesToolStripMenuItem";
		  this.applyChangesToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
		  this.applyChangesToolStripMenuItem.Text = "Apply Changes";
		  this.applyChangesToolStripMenuItem.Click += new System.EventHandler(this.applyChangesToolStripMenuItem_Click);
		  // 
		  // cleanupToolStripMenuItem
		  // 
		  this.cleanupToolStripMenuItem.Image = global::SMOz.Properties.Resources.Tools_Cleanup;
		  this.cleanupToolStripMenuItem.Name = "cleanupToolStripMenuItem";
		  this.cleanupToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
		  this.cleanupToolStripMenuItem.Text = "Cleanup";
		  this.cleanupToolStripMenuItem.Click += new System.EventHandler(this.cleanupToolStripMenuItem_Click);
		  // 
		  // associateToolStripMenuItem
		  // 
		  this.associateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.validateToolStripMenuItem});
		  this.associateToolStripMenuItem.Name = "associateToolStripMenuItem";
		  this.associateToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
		  this.associateToolStripMenuItem.Text = "Associate";
		  this.associateToolStripMenuItem.Click += new System.EventHandler(this.associateToolStripMenuItem_Click);
		  // 
		  // validateToolStripMenuItem
		  // 
		  this.validateToolStripMenuItem.Name = "validateToolStripMenuItem";
		  this.validateToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
		  this.validateToolStripMenuItem.Text = "Validate";
		  this.validateToolStripMenuItem.Click += new System.EventHandler(this.validateToolStripMenuItem_Click);
		  // 
		  // helpToolStripMenuItem
		  // 
		  this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
		  this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
		  this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
		  this.helpToolStripMenuItem.Text = "Help";
		  // 
		  // helpToolStripMenuItem1
		  // 
		  this.helpToolStripMenuItem1.Image = global::SMOz.Properties.Resources.Help_Help;
		  this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
		  this.helpToolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
		  this.helpToolStripMenuItem1.Text = "Help";
		  // 
		  // aboutToolStripMenuItem
		  // 
		  this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
		  this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
		  this.aboutToolStripMenuItem.Text = "About";
		  // 
		  // _statusStrip
		  // 
		  this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusLabel});
		  this._statusStrip.Location = new System.Drawing.Point(0, 413);
		  this._statusStrip.Name = "_statusStrip";
		  this._statusStrip.Size = new System.Drawing.Size(685, 22);
		  this._statusStrip.TabIndex = 1;
		  this._statusStrip.Text = "statusStrip1";
		  // 
		  // _statusLabel
		  // 
		  this._statusLabel.Name = "_statusLabel";
		  this._statusLabel.Size = new System.Drawing.Size(38, 17);
		  this._statusLabel.Text = "Ready";
		  // 
		  // toolStrip1
		  // 
		  this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._searchOptions,
            this._searchText,
            this._newTemplate,
            this._open,
            this.toolStripSeparator2,
            this._cutButton,
            this._pasteButton,
            this.toolStripSeparator3,
            this._undoButton,
            this._redoButton,
            this.toolStripSeparator1,
            this._toggleView,
            this.toolStripSeparator4,
            this._applyTemplateButton,
            this._applyChangesButton});
		  this.toolStrip1.Location = new System.Drawing.Point(0, 24);
		  this.toolStrip1.Name = "toolStrip1";
		  this.toolStrip1.Size = new System.Drawing.Size(685, 25);
		  this.toolStrip1.TabIndex = 2;
		  this.toolStrip1.Text = "toolStrip1";
		  // 
		  // _searchOptions
		  // 
		  this._searchOptions.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
		  this._searchOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
		  this._searchOptions.DropDown = this._searchOptionsContext;
		  this._searchOptions.Image = ((System.Drawing.Image)(resources.GetObject("_searchOptions.Image")));
		  this._searchOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
		  this._searchOptions.Name = "_searchOptions";
		  this._searchOptions.Size = new System.Drawing.Size(13, 22);
		  this._searchOptions.Text = "Search Options";
		  // 
		  // _searchOptionsContext
		  // 
		  this._searchOptionsContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedCategoryToolStripMenuItem,
            this.allToolStripMenuItem,
            this.toolStripMenuItem5,
            this.regexToolStripMenuItem});
		  this._searchOptionsContext.Name = "_searchOptionsContext";
		  this._searchOptionsContext.OwnerItem = this._searchOptions;
		  this._searchOptionsContext.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
		  this._searchOptionsContext.Size = new System.Drawing.Size(175, 76);
		  // 
		  // selectedCategoryToolStripMenuItem
		  // 
		  this.selectedCategoryToolStripMenuItem.Checked = true;
		  this.selectedCategoryToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
		  this.selectedCategoryToolStripMenuItem.Name = "selectedCategoryToolStripMenuItem";
		  this.selectedCategoryToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
		  this.selectedCategoryToolStripMenuItem.Text = "Selected Category";
		  this.selectedCategoryToolStripMenuItem.Click += new System.EventHandler(this.selectedCategoryToolStripMenuItem_Click);
		  // 
		  // allToolStripMenuItem
		  // 
		  this.allToolStripMenuItem.Name = "allToolStripMenuItem";
		  this.allToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
		  this.allToolStripMenuItem.Text = "All";
		  this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
		  // 
		  // toolStripMenuItem5
		  // 
		  this.toolStripMenuItem5.Name = "toolStripMenuItem5";
		  this.toolStripMenuItem5.Size = new System.Drawing.Size(171, 6);
		  // 
		  // regexToolStripMenuItem
		  // 
		  this.regexToolStripMenuItem.CheckOnClick = true;
		  this.regexToolStripMenuItem.Name = "regexToolStripMenuItem";
		  this.regexToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
		  this.regexToolStripMenuItem.Text = "Regex";
		  // 
		  // _searchText
		  // 
		  this._searchText.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
		  this._searchText.Name = "_searchText";
		  this._searchText.Size = new System.Drawing.Size(150, 25);
		  this._searchText.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		  this._searchText.TextChanged += new System.EventHandler(this._searchText_TextChanged);
		  // 
		  // _newTemplate
		  // 
		  this._newTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		  this._newTemplate.Image = global::SMOz.Properties.Resources.File_New;
		  this._newTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
		  this._newTemplate.Name = "_newTemplate";
		  this._newTemplate.Size = new System.Drawing.Size(23, 22);
		  this._newTemplate.Text = "New";
		  // 
		  // _open
		  // 
		  this._open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		  this._open.Image = global::SMOz.Properties.Resources.File_Open;
		  this._open.ImageTransparentColor = System.Drawing.Color.Magenta;
		  this._open.Name = "_open";
		  this._open.Size = new System.Drawing.Size(23, 22);
		  this._open.Text = "Open";
		  this._open.Click += new System.EventHandler(this.openTemplateToolStripMenuItem_Click);
		  // 
		  // toolStripSeparator2
		  // 
		  this.toolStripSeparator2.Name = "toolStripSeparator2";
		  this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
		  // 
		  // _cutButton
		  // 
		  this._cutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		  this._cutButton.Image = global::SMOz.Properties.Resources.Edit_Cut;
		  this._cutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
		  this._cutButton.Name = "_cutButton";
		  this._cutButton.Size = new System.Drawing.Size(23, 22);
		  this._cutButton.Text = "Cut";
		  this._cutButton.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
		  // 
		  // _pasteButton
		  // 
		  this._pasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		  this._pasteButton.Image = global::SMOz.Properties.Resources.Edit_Paste;
		  this._pasteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
		  this._pasteButton.Name = "_pasteButton";
		  this._pasteButton.Size = new System.Drawing.Size(23, 22);
		  this._pasteButton.Text = "Paste";
		  this._pasteButton.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
		  // 
		  // toolStripSeparator3
		  // 
		  this.toolStripSeparator3.Name = "toolStripSeparator3";
		  this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
		  // 
		  // _undoButton
		  // 
		  this._undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		  this._undoButton.Image = global::SMOz.Properties.Resources.Edit_Undo;
		  this._undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
		  this._undoButton.Name = "_undoButton";
		  this._undoButton.Size = new System.Drawing.Size(32, 22);
		  this._undoButton.Text = "Undo";
		  this._undoButton.ButtonClick += new System.EventHandler(this.undoToolStripMenuItem_Click);
		  // 
		  // _redoButton
		  // 
		  this._redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		  this._redoButton.Image = global::SMOz.Properties.Resources.Edit_Redo;
		  this._redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
		  this._redoButton.Name = "_redoButton";
		  this._redoButton.Size = new System.Drawing.Size(32, 22);
		  this._redoButton.Text = "Redo";
		  this._redoButton.ButtonClick += new System.EventHandler(this.redoToolStripMenuItem_Click);
		  // 
		  // toolStripSeparator1
		  // 
		  this.toolStripSeparator1.Name = "toolStripSeparator1";
		  this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
		  // 
		  // _toggleView
		  // 
		  this._toggleView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		  this._toggleView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileToolStripMenuItem,
            this.iconsToolStripMenuItem,
            this.smallIconsToolStripMenuItem,
            this.detailsToolStripMenuItem});
		  this._toggleView.Image = global::SMOz.Properties.Resources.Edit_View;
		  this._toggleView.ImageTransparentColor = System.Drawing.Color.Magenta;
		  this._toggleView.Name = "_toggleView";
		  this._toggleView.Size = new System.Drawing.Size(32, 22);
		  this._toggleView.Text = "View";
		  this._toggleView.ButtonClick += new System.EventHandler(this._toggleView_ButtonClick);
		  // 
		  // tileToolStripMenuItem
		  // 
		  this.tileToolStripMenuItem.Name = "tileToolStripMenuItem";
		  this.tileToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
		  this.tileToolStripMenuItem.Text = "Tile";
		  this.tileToolStripMenuItem.Click += new System.EventHandler(this.tileToolStripMenuItem_Click);
		  // 
		  // iconsToolStripMenuItem
		  // 
		  this.iconsToolStripMenuItem.Name = "iconsToolStripMenuItem";
		  this.iconsToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
		  this.iconsToolStripMenuItem.Text = "Icons";
		  this.iconsToolStripMenuItem.Click += new System.EventHandler(this.iconsToolStripMenuItem_Click);
		  // 
		  // smallIconsToolStripMenuItem
		  // 
		  this.smallIconsToolStripMenuItem.Name = "smallIconsToolStripMenuItem";
		  this.smallIconsToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
		  this.smallIconsToolStripMenuItem.Text = "List";
		  this.smallIconsToolStripMenuItem.Click += new System.EventHandler(this.smallIconsToolStripMenuItem_Click);
		  // 
		  // detailsToolStripMenuItem
		  // 
		  this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
		  this.detailsToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
		  this.detailsToolStripMenuItem.Text = "Details";
		  this.detailsToolStripMenuItem.Click += new System.EventHandler(this.detailsToolStripMenuItem_Click);
		  // 
		  // toolStripSeparator4
		  // 
		  this.toolStripSeparator4.Name = "toolStripSeparator4";
		  this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
		  // 
		  // _applyChangesButton
		  // 
		  this._applyChangesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		  this._applyChangesButton.Image = global::SMOz.Properties.Resources.Tools_Run;
		  this._applyChangesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
		  this._applyChangesButton.Name = "_applyChangesButton";
		  this._applyChangesButton.Size = new System.Drawing.Size(23, 22);
		  this._applyChangesButton.Text = "Apply Changes";
		  this._applyChangesButton.Click += new System.EventHandler(this.applyChangesToolStripMenuItem_Click);
		  // 
		  // splitContainer1
		  // 
		  this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		  this.splitContainer1.Location = new System.Drawing.Point(0, 49);
		  this.splitContainer1.Name = "splitContainer1";
		  // 
		  // splitContainer1.Panel1
		  // 
		  this.splitContainer1.Panel1.Controls.Add(this._categoryTree);
		  // 
		  // splitContainer1.Panel2
		  // 
		  this.splitContainer1.Panel2.Controls.Add(this._itemList);
		  this.splitContainer1.Size = new System.Drawing.Size(685, 364);
		  this.splitContainer1.SplitterDistance = 187;
		  this.splitContainer1.TabIndex = 3;
		  // 
		  // _itemList
		  // 
		  this._itemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._colName,
            this._colLocation});
		  this._itemList.ContextMenuStrip = this._itemListContext;
		  this._itemList.Dock = System.Windows.Forms.DockStyle.Fill;
		  this._itemList.FullRowSelect = true;
		  this._itemList.LabelEdit = true;
		  this._itemList.Location = new System.Drawing.Point(0, 0);
		  this._itemList.Name = "_itemList";
		  this._itemList.Size = new System.Drawing.Size(494, 364);
		  this._itemList.TabIndex = 0;
		  this._itemList.TileSize = new System.Drawing.Size(200, 56);
		  this._itemList.UseCompatibleStateImageBehavior = false;
		  this._itemList.View = System.Windows.Forms.View.Tile;
		  this._itemList.DoubleClick += new System.EventHandler(this._itemList_DoubleClick);
		  this._itemList.SelectedIndexChanged += new System.EventHandler(this._itemList_SelectedIndexChanged);
		  this._itemList.KeyDown += new System.Windows.Forms.KeyEventHandler(this._itemList_KeyDown);
		  this._itemList.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this._itemList_AfterLabelEdit);
		  this._itemList.KeyUp += new System.Windows.Forms.KeyEventHandler(this._itemList_KeyUp);
		  this._itemList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this._itemList_ItemDrag);
		  this._itemList.MouseDown += new System.Windows.Forms.MouseEventHandler(this._itemList_MouseDown);
		  // 
		  // _colName
		  // 
		  this._colName.Text = "Name";
		  // 
		  // _colLocation
		  // 
		  this._colLocation.Text = "Location";
		  // 
		  // _itemListContext
		  // 
		  this._itemListContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripMenuItem7,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem1,
            this.toolStripMenuItem9,
            this.convertToCategoryToolStripMenuItem});
		  this._itemListContext.Name = "_itemListContext";
		  this._itemListContext.ShowImageMargin = false;
		  this._itemListContext.Size = new System.Drawing.Size(159, 104);
		  this._itemListContext.Opening += new System.ComponentModel.CancelEventHandler(this._itemListContext_Opening);
		  // 
		  // openToolStripMenuItem
		  // 
		  this.openToolStripMenuItem.Name = "openToolStripMenuItem";
		  this.openToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
		  this.openToolStripMenuItem.Text = "Locate";
		  this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
		  // 
		  // toolStripMenuItem7
		  // 
		  this.toolStripMenuItem7.Name = "toolStripMenuItem7";
		  this.toolStripMenuItem7.Size = new System.Drawing.Size(155, 6);
		  // 
		  // renameToolStripMenuItem
		  // 
		  this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
		  this.renameToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
		  this.renameToolStripMenuItem.Text = "Rename";
		  this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
		  // 
		  // deleteToolStripMenuItem1
		  // 
		  this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
		  this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
		  this.deleteToolStripMenuItem1.Text = "Delete";
		  this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
		  // 
		  // toolStripMenuItem9
		  // 
		  this.toolStripMenuItem9.Name = "toolStripMenuItem9";
		  this.toolStripMenuItem9.Size = new System.Drawing.Size(155, 6);
		  // 
		  // convertToCategoryToolStripMenuItem
		  // 
		  this.convertToCategoryToolStripMenuItem.Name = "convertToCategoryToolStripMenuItem";
		  this.convertToCategoryToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
		  this.convertToCategoryToolStripMenuItem.Text = "Convert to category";
		  this.convertToCategoryToolStripMenuItem.Click += new System.EventHandler(this.convertToCategoryToolStripMenuItem_Click);
		  // 
		  // _categoryTreeContext
		  // 
		  this._categoryTreeContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCategoryHereToolStripMenuItem,
            this.hideToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.toolStripMenuItem6,
            this.applyTemplateToolStripMenuItem});
		  this._categoryTreeContext.Name = "_categoryTreeContext";
		  this._categoryTreeContext.ShowImageMargin = false;
		  this._categoryTreeContext.Size = new System.Drawing.Size(156, 98);
		  this._categoryTreeContext.Opening += new System.ComponentModel.CancelEventHandler(this._categoryTreeContext_Opening);
		  // 
		  // newCategoryHereToolStripMenuItem
		  // 
		  this.newCategoryHereToolStripMenuItem.Name = "newCategoryHereToolStripMenuItem";
		  this.newCategoryHereToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
		  this.newCategoryHereToolStripMenuItem.Text = "New Category Here";
		  this.newCategoryHereToolStripMenuItem.Click += new System.EventHandler(this.newCategoryHereToolStripMenuItem_Click);
		  // 
		  // hideToolStripMenuItem
		  // 
		  this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
		  this.hideToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
		  this.hideToolStripMenuItem.Text = "Hide";
		  this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
		  // 
		  // removeToolStripMenuItem
		  // 
		  this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
		  this.removeToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
		  this.removeToolStripMenuItem.Text = "Remove";
		  this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
		  // 
		  // toolStripMenuItem6
		  // 
		  this.toolStripMenuItem6.Name = "toolStripMenuItem6";
		  this.toolStripMenuItem6.Size = new System.Drawing.Size(152, 6);
		  // 
		  // applyTemplateToolStripMenuItem
		  // 
		  this.applyTemplateToolStripMenuItem.Name = "applyTemplateToolStripMenuItem";
		  this.applyTemplateToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
		  this.applyTemplateToolStripMenuItem.Text = "Apply Template";
		  this.applyTemplateToolStripMenuItem.Click += new System.EventHandler(this.applyTemplateToolStripMenuItem_Click);
		  // 
		  // _categoryTree
		  // 
		  this._categoryTree.AllowDrop = true;
		  this._categoryTree.Dock = System.Windows.Forms.DockStyle.Fill;
		  this._categoryTree.FullRowSelect = true;
		  this._categoryTree.HideSelection = false;
		  this._categoryTree.LabelEdit = true;
		  this._categoryTree.Location = new System.Drawing.Point(0, 0);
		  this._categoryTree.Name = "_categoryTree";
		  this._categoryTree.Size = new System.Drawing.Size(187, 364);
		  this._categoryTree.TabIndex = 0;
		  this._categoryTree.DragDrop += new System.Windows.Forms.DragEventHandler(this._categoryTree_DragDrop);
		  this._categoryTree.DragOver += new System.Windows.Forms.DragEventHandler(this._categoryTree_DragOver);
		  this._categoryTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this._categoryTree_AfterLabelEdit);
		  this._categoryTree.DragLeave += new System.EventHandler(this._categoryTree_DragLeave);
		  this._categoryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._categoryTree_AfterSelect);
		  this._categoryTree.DragEnter += new System.Windows.Forms.DragEventHandler(this._categoryTree_DragEnter);
		  this._categoryTree.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this._categoryTree_BeforeLabelEdit);
		  this._categoryTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this._categoryTree_KeyDown);
		  this._categoryTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this._categoryTree_MouseDown);
		  // 
		  // _applyTemplateButton
		  // 
		  this._applyTemplateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		  this._applyTemplateButton.Image = global::SMOz.Properties.Resources.Tools_Organize;
		  this._applyTemplateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
		  this._applyTemplateButton.Name = "_applyTemplateButton";
		  this._applyTemplateButton.Size = new System.Drawing.Size(23, 22);
		  this._applyTemplateButton.Text = "Apply Template";
		  this._applyTemplateButton.Click += new System.EventHandler(this.applyTemplateToolStripMenuItem1_Click);
		  // 
		  // MainForm
		  // 
		  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		  this.ClientSize = new System.Drawing.Size(685, 435);
		  this.Controls.Add(this.splitContainer1);
		  this.Controls.Add(this.toolStrip1);
		  this.Controls.Add(this._statusStrip);
		  this.Controls.Add(this.menuStrip1);
		  this.MainMenuStrip = this.menuStrip1;
		  this.Name = "MainForm";
		  this.Text = "SMOz";
		  this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
		  this.menuStrip1.ResumeLayout(false);
		  this.menuStrip1.PerformLayout();
		  this._statusStrip.ResumeLayout(false);
		  this._statusStrip.PerformLayout();
		  this.toolStrip1.ResumeLayout(false);
		  this.toolStrip1.PerformLayout();
		  this._searchOptionsContext.ResumeLayout(false);
		  this.splitContainer1.Panel1.ResumeLayout(false);
		  this.splitContainer1.Panel2.ResumeLayout(false);
		  this.splitContainer1.ResumeLayout(false);
		  this._itemListContext.ResumeLayout(false);
		  this._categoryTreeContext.ResumeLayout(false);
		  this.ResumeLayout(false);
		  this.PerformLayout();

	   }

	   #endregion

	   private System.Windows.Forms.MenuStrip menuStrip1;
	   private System.Windows.Forms.StatusStrip _statusStrip;
	   private System.Windows.Forms.ToolStrip toolStrip1;
	   private System.Windows.Forms.SplitContainer splitContainer1;
	   private SMOz.Utilities.TreeViewEx _categoryTree;
	   private System.Windows.Forms.ListView _itemList;
	   private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem openTemplateToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem saveTemplateToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem saveTemplateAsToolStripMenuItem;
	   private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
	   private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
	   private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
	   private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
	   private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
	   private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem templateEditorToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem mergeTemplateToolStripMenuItem;
	   private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
	   private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
	   private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
	   private System.Windows.Forms.ToolStripTextBox _searchText;
	   private System.Windows.Forms.ToolStripStatusLabel _statusLabel;
	   private System.Windows.Forms.ColumnHeader _colName;
	   private System.Windows.Forms.ColumnHeader _colLocation;
	   private System.Windows.Forms.ToolStripDropDownButton _searchOptions;
	   private System.Windows.Forms.ToolStripMenuItem applyChangesToolStripMenuItem;
	   private System.Windows.Forms.ContextMenuStrip _searchOptionsContext;
	   private System.Windows.Forms.ToolStripMenuItem selectedCategoryToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
	   private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
	   private System.Windows.Forms.ToolStripMenuItem regexToolStripMenuItem;
	   private System.Windows.Forms.ContextMenuStrip _categoryTreeContext;
	   private System.Windows.Forms.ToolStripMenuItem newCategoryHereToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
	   private System.Windows.Forms.ContextMenuStrip _itemListContext;
	   private System.Windows.Forms.ToolStripMenuItem convertToCategoryToolStripMenuItem;
	   private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
	   private System.Windows.Forms.ToolStripMenuItem applyTemplateToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem applyTemplateToolStripMenuItem1;
	   private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
	   private System.Windows.Forms.ToolStripMenuItem cleanupToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
	   private System.Windows.Forms.ToolStripSplitButton _toggleView;
	   private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem tileToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem saveCurrentConfigurationToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem iconsToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem smallIconsToolStripMenuItem;
	   private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
	   private System.Windows.Forms.ToolStripMenuItem categoryToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem addCategoryToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
	   private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
	   private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	   private System.Windows.Forms.ToolStripButton _open;
	   private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	   private System.Windows.Forms.ToolStripButton _newTemplate;
	   private System.Windows.Forms.ToolStripButton _cutButton;
	   private System.Windows.Forms.ToolStripButton _pasteButton;
	   private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
	   private System.Windows.Forms.ToolStripSplitButton _undoButton;
	   private System.Windows.Forms.ToolStripSplitButton _redoButton;
	   private System.Windows.Forms.ToolStripButton _applyChangesButton;
	   private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
	   private System.Windows.Forms.ToolStripMenuItem associateToolStripMenuItem;
	   private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem;
	   private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
	   private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
	   private System.Windows.Forms.ToolStripButton _applyTemplateButton;
    }
}