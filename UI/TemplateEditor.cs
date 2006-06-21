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
 *  Original FileName :  TemplateEditor.cs
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

using SMOz.StartMenu;
using System.Text.RegularExpressions;
using SMOz.Template;
using SMOz.Utilities;
using EXControls;
using XPTable.Models;
using XPTable.Editors;
using System.IO;

namespace SMOz.UI
{
    // Note: Edits are live.
    public partial class TemplateEditor : Form
    {
	   StartManager manager;
	   TemplateProvider template;

	   public TemplateEditor(StartManager manager, TemplateProvider template) {
		  InitializeComponent();
		  this.Icon = SMOz.Properties.Resources.Application;

		  this.template = template;
		  this.manager = manager;

		  StartItem[] uncategorized = manager.GetByCategory("");
		  string[] uncategorizedStr = new string[uncategorized.Length];
		  for (int i = 0; i < uncategorized.Length; i++) {
			 uncategorizedStr[i] = uncategorized[i].Name;
		  }

		  InitializeTable(uncategorizedStr, KnownCategories.Instance.ToArray());

		  AddToTable(string.Empty);
	   }

	   Category orphan = new Category("", "");

	   public void InitializeTable(string[] patterns, string[] categories) {
		  this._templateTable.ColumnModel = new ColumnModel();
		  this._templateTable.TableModel = new TableModel();
		  // 0: Name
		  ComboBoxColumn patternCol = new ComboBoxColumn("Pattern");
		  ComboBoxCellEditor patternEditor = new ComboBoxCellEditor();
		  patternEditor.TextChanged += new EventHandler(patternEditor_TextChanged);
		  patternEditor.DropDownStyle = DropDownStyle.DropDown;
//		  patternEditor.Items.AddRange(patterns);
		  patternEditor.AutCompleteAddItems(patterns, AutoCompleteMode.Suggest);
		  patternCol.Editor = patternEditor;
		  patternCol.Editable = true;
		  this._templateTable.ColumnModel.Columns.Add(patternCol);
		  // 1: Type
		  ComboBoxColumn typeCol = new ComboBoxColumn("Type");
		  ComboBoxCellEditor typeEditor = new ComboBoxCellEditor();
		  typeEditor.Items.Add(CategoryItemType.String.ToString());
		  typeEditor.Items.Add(CategoryItemType.WildCard.ToString());
		  typeEditor.Items.Add(CategoryItemType.Regex.ToString());
		  typeCol.Editor = typeEditor;
		  typeCol.Editable = true;
		  this._templateTable.ColumnModel.Columns.Add(typeCol);
		  // 2: Category
		  ComboBoxColumn categoryCol = new ComboBoxColumn("Category");
		  ComboBoxCellEditor categoryEditor = new ComboBoxCellEditor();
		  categoryEditor.DropDownStyle = DropDownStyle.DropDown;
//		  categoryEditor.Items.AddRange(categories);
		  categoryEditor.AutCompleteAddItems(categories, AutoCompleteMode.SuggestAppend);
		  categoryCol.Editor = categoryEditor;
		  categoryCol.Editable = true;
		  this._templateTable.ColumnModel.Columns.Add(categoryCol);

		  AdjustColumnWidth();

		  this._templateTable.BeginEditing += new XPTable.Events.CellEditEventHandler(_templateTable_BeginEditing);
		  this._templateTable.AfterEditing += new XPTable.Events.CellEditEventHandler(_templateTable_AfterEditing);
		  this._templateTable.MouseDown += new System.Windows.Forms.MouseEventHandler(this._templateTable_MouseDown);
		  this._templateTable.AlternatingRowColor = Color.LightBlue;
	   }


	   CategoryItem editingCategoryItem;
	   
	   void patternEditor_TextChanged(object sender, EventArgs e) {
		  if (editingCategoryItem != null) {
			 TextBox senderBox = sender as TextBox;
			 if (senderBox != null) {
				ShowLivePreview(new CategoryItem(senderBox.Text, editingCategoryItem.Type));
			 }
		  }
	   }

	   void _templateTable_BeginEditing(object sender, XPTable.Events.CellEditEventArgs e) {
		  if (e.Column == 0) { // The Pattern column
			 Row currentRow = _templateTable.TableModel.Rows[e.Row];
			 editingCategoryItem = (CategoryItem)currentRow.Tag;
			 Console.WriteLine(editingCategoryItem);
		  }
	   }

	   void _templateTable_AfterEditing(object sender, XPTable.Events.CellEditEventArgs e) {
		  if (preview != null) {
			 // reset preview
			 preview.PreviewList.Items.Clear();
		  }

		  editingCategoryItem = null;
		  Row currentRow = _templateTable.TableModel.Rows[e.Row];
		  CategoryItem currentItem = (CategoryItem)currentRow.Tag;

		  if (e.Column == 0) { // The Pattern column
			 currentItem.Value = e.Cell.Text;
		  } else if (e.Column == 1) { // The Type column
			 currentItem.Type = (CategoryItemType)Enum.Parse(typeof(CategoryItemType), e.Cell.Text);
		  } else if (e.Column == 2) { // The Category column
			 SetCategory(currentItem, e.Cell.Text);
		  }
	   }

	   void SetCategory(CategoryItem item, string formatCategoryName) {
		  Category cat = FindCategory(formatCategoryName);
		  if (cat == null) {
			 Category newCat = Category.FromFormat(formatCategoryName);
			 this.template.Add(newCat);
			 item.Parent = newCat;
			 item.Parent.Add(item);
			 AddCategoryToTree(newCat.Name);
		  } else {
			 item.Parent = cat;
			 item.Parent.Add(item);
		  }
	   }

	   private void ShowLivePreview(CategoryItem categoryItem){
		  if (preview != null) {
			 preview.PreviewList.BeginUpdate();
			 preview.PreviewList.Items.Clear();
			 string star = "";

			 Regex regex = null;
			 try {
				regex = new Regex(categoryItem.Pattern, Utility.REGEX_OPTIONS);
				Console.WriteLine(categoryItem.Pattern);
			 } catch (Exception ex) {
				preview.PreviewList.Items.Clear();
				preview.PreviewList.Items.Add(ex.Message);
				preview.PreviewList.EndUpdate();
				return;
			 }

			 foreach (StartItem item in manager.StartItems) {
				star = item.Name.Contains("\\") ? "*" : "";
				if (regex.Match(item.Name).Success) {
				    preview.PreviewList.Items.Add(star + item.Name);
				}
			 }
			 preview.PreviewList.EndUpdate();
		  }
	   }

	   private void AdjustColumnWidth() {
		  int colWidth = (this._templateTable.Width / 3) - 8;
		  this._templateTable.ColumnModel.Columns[0].Width = colWidth;
		  this._templateTable.ColumnModel.Columns[1].Width = colWidth;
		  this._templateTable.ColumnModel.Columns[2].Width = colWidth;
	   }

	   public static Row CategoryItemToRow(CategoryItem item) {
		  string category = "";
		  if (item.Parent != null) { category = item.Parent.ToFormat(); }
		  Row result = new Row(new string[] { item.Value, item.Type.ToString(), category });
		  result.Tag = item;
		  return result;
	   }


	   private void AddToTable(string nameFilter) {
		  _templateTable.BeginUpdate();
		  _templateTable.TableModel.Rows.Clear();

		  for (int i = -1; i < this.template.Count; i++) {
			 Category current;
			 if (i == -1) {
				if (!string.IsNullOrEmpty(nameFilter)) { continue; }
				current = this.orphan;
			 } else {
				current = this.template[i];
				if ((!string.IsNullOrEmpty(nameFilter)) && (nameFilter != current.Name)) { continue; }
			 }
			 AddCategoryToTree(current.Name);
			 foreach (CategoryItem item in current.Items) {
				_templateTable.TableModel.Rows.Add(CategoryItemToRow(item));
			 }
		  }

		  foreach (Category category in this.template.Categories) {
			 
		  }
		  _templateTable.EndUpdate();
	   }

	   private string[] AddCategoryToTree(string text) {
		  int iconIndex = 0;

		  string[] tree;
		  if (text == "") {
			 tree = new string[] { text };
		  } else {
			 tree = Utility.PathToTree(text);
		  }

		  TreeNode[] nodes;
		  TreeNode node, lastNode = null;
		  // look from 
		  bool orphan = true;
		  for (int j = tree.Length - 1; j >= 0; j--) {
			 nodes = _categoryTree.Nodes.Find(tree[j], true);
			 if (nodes == null || nodes.Length <= 0) {
				// create it
				node = CreateTreeNode(tree[j], iconIndex);
				if (lastNode != null) {
				    node.Nodes.Add(lastNode);
				}
				lastNode = node;
			 } else {
				// tree exists
				orphan = false;
				if (lastNode != null) { nodes[0].Nodes.Add(lastNode); }
				break;
			 }
		  }
		  if (orphan) {
			 _categoryTree.Nodes.Add(lastNode);
		  }

		  return tree;
	   }

	   private TreeNode CreateTreeNode(string text, int iconIndex) {
		  TreeNode node = new TreeNode(text.Substring(text.LastIndexOf(Path.DirectorySeparatorChar) + 1));
		  if (text == "") { node.Text = "All"; }
		  node.ImageIndex = iconIndex;
		  node.SelectedImageIndex = iconIndex;

		  node.Name = text;
		  return node;
	   }

	   public static EXListViewItem CategoryItemToListItem(CategoryItem item, Category category, ListViewGroup group){
		  EXListViewItem newItem = new EXListViewItem(item.Value);
		  newItem.SubItems.Add(new EXListViewSubItem(item.Type.ToString()));
		  newItem.Name = item.Value;
		  newItem.TagData = item;
		  if (category != null) { newItem.SubItems.Add(new EXListViewSubItem(category.Name)); }
		  if (group != null) { newItem.Group = group; }
		  return newItem;
	   }

	   public static void PersistItemChanges(IEnumerable<EXListViewItem> listItems) {
		  foreach (EXListViewItem listItem in listItems) {
			 CategoryItem catiItem = listItem.TagData as CategoryItem;
			 if (catiItem != null) {
				catiItem.Value = listItem.SubItems[0].Text;
				catiItem.Type = (CategoryItemType)Enum.Parse(typeof(CategoryItemType), listItem.SubItems[1].Text);
			 }
			 Console.WriteLine("{{{0}, {1}}}", catiItem.Value, catiItem.Type);
		  }
	   }

	   Dictionary<string, Category> lookupTable = new Dictionary<string, Category>();

	   private void PopulateLookupTable(TemplateProvider template) {
		  lookupTable.Clear();
		  foreach (Category category in template.Categories) {
			 lookupTable.Add(category.ToFormat(), category);
		  }
	   }

	   public void PersistAllChanges(TemplateProvider template, IEnumerable<EXListViewItem> listItems) {
		  foreach (EXListViewItem listItem in listItems) {
			 CategoryItem catiItem = listItem.TagData as CategoryItem;
			 if (catiItem != null) {
				catiItem.Value = listItem.SubItems[0].Text;
				catiItem.Type = (CategoryItemType)Enum.Parse(typeof(CategoryItemType), listItem.SubItems[1].Text);
				Category category = FindCategory(listItem.SubItems[2].Text);
				if (catiItem.Parent != category) {
				    category.Add(catiItem);
				}
				catiItem.Type = (CategoryItemType)Enum.Parse(typeof(CategoryItemType), listItem.SubItems[1].Text);
			 }
		  }
	   }

	   public Category FindCategoryByName(string name) {
		  foreach (Category category in this.template.Categories) {
			 if (string.Compare(category.Name, name, true) == 0) {
				return category;
			 }
		  }
		  return null;
	   }
	   

	   public Category FindCategory(string format) {
		  foreach (Category category in this.template.Categories) {
			 if (string.Compare(category.ToFormat(), format, true) == 0) {
				return category;
			 }
		  }
		  return null;
	   }

	   private void _templateList_DoubleClick(object sender, EventArgs e) {
	   }

	   private void _templateList_KeyUp(object sender, KeyEventArgs e) {
		  ListView _senderList = sender as ListView;

		  if (e.KeyCode == Keys.Delete) {
			 if ((_senderList.SelectedItems != null) && (_senderList.SelectedItems.Count > 0)) {
//				provider.table.Remove(_senderList.SelectedItems[0].SubItems[0].Text);
				_senderList.Items.Remove(_senderList.SelectedItems[0]);
			 }
		  } else if (e.KeyCode == Keys.F2) {
			 if ((_senderList.SelectedItems != null) && (_senderList.SelectedItems.Count > 0)) {
//				_senderList.SelectedItems[0]();
			 }
		  }
	   }

	   private void textBox1_Enter(object sender, EventArgs e) {
		  _searchLabel.Hide();
	   }

	   private void _searchQuery_Leave(object sender, EventArgs e) {
		  if (string.IsNullOrEmpty(_searchQuery.Text)) {
			 _searchLabel.Show();
		  }
	   }

	   System.Threading.Timer searchTimer;
	   private void textBox1_TextChanged(object sender, EventArgs e) {
		  if (searchTimer != null) {
			 searchTimer.Dispose();
			 searchTimer = null;
		  }
		  
		  searchTimer = new System.Threading.Timer(this.DoSearch, null, 500, System.Threading.Timeout.Infinite);
	   }

	   private void DoSearch(object state) {
		  this.Invoke(new MethodInvoker(this.DoSearch));
	   }

	   private void ResetSearch() {
		  this.Text = "Template Editor";
		  AddToTable(_categoryTree.SelectedNode.Name);
		  _templateTable.NoItemsText = "Right Click to add a new pattern.";
	   }

	   private void DoSearch() {
		  _templateTable.BeginUpdate();

		  if (string.IsNullOrEmpty(_searchQuery.Text)) {
			 ResetSearch();
		  } else {
			 this.Text = "Template Editor - Search";
			 _templateTable.NoItemsText = "No results found for '" + _searchQuery.Text + "'.";
			 Regex regex = new Regex(".*" + Regex.Escape(_searchQuery.Text) + ".*", RegexOptions.IgnoreCase);
			 _templateTable.TableModel.Rows.Clear();

			 for (int i = -1; i < this.template.Count; i++) {
				Category category = null;
				if (i == -1) { category = this.orphan; } else { category = this.template[i]; }
				foreach (CategoryItem item in category.Items) {
				    if (regex.Match(item.Value).Success) {
					   _templateTable.TableModel.Rows.Add(CategoryItemToRow(item));
				    }
				}
			 }

			 foreach (Category category in this.template.Categories) {
				
			 }
		  }
		  _templateTable.EndUpdate();
	   }

	   private void _templateList_AfterLabelEdit(object sender, LabelEditEventArgs e) {
		  ListView lsender = sender as ListView;
		  if (string.IsNullOrEmpty(e.Label)) {
			 e.CancelEdit = true;
			 return;
		  }
		  string item = lsender.Items[e.Item].Text;
//		  string category = provider.table[item];
//		  provider.table.Remove(item);
//		  provider.Add(e.Label, category);
	   }

	   private void _searchLabel_Click(object sender, EventArgs e) {
		  _searchQuery.Focus();
	   }

	   private void _pattern_Enter(object sender, EventArgs e) {
		  if ((preview != null) && _previewAutoHide.Checked) {
			 preview.Show();
//			 _pattern.Focus();
		  }
	   }

	   private void _pattern_Leave(object sender, EventArgs e) {
		  if ((preview != null) && _previewAutoHide.Checked){
			 preview.Hide();
		  }
	   }

	   MatchPreview preview;

	   private void _showPreview_CheckedChanged(object sender, EventArgs e) {
		  if (_showPreview.Checked) {
			 if (preview == null) {
				preview = new MatchPreview();
				preview.FormClosed += new FormClosedEventHandler(preview_FormClosed);
				preview.Show(this);
				_previewAutoHide.Show();
			 } else {
				preview.BringToFront();
			 }
		  } else {
			 if (preview != null) { preview.Close(); }
		  }
	   }

	   void preview_FormClosed(object sender, FormClosedEventArgs e) {
		  preview = null;
		  _showPreview.Checked = false;
		  _previewAutoHide.Show();
	   }

	   Row clickedItem;

	   private void newToolStripMenuItem_Click(object sender, EventArgs e) {
		  Category category = null;
		  if ((_categoryTree.SelectedNode != null) && (_categoryTree.SelectedNode.Name != "")) {
			 category = FindCategoryByName(_categoryTree.SelectedNode.Name);
			 if (category == null) {
				// this may be a middle node;
				Category newCat = Category.FromFormat(_categoryTree.SelectedNode.Name);
				this.template.Add(newCat);
//				AddCategoryToTree(newCat.Name);  // not necessary
				category = newCat;
			 }
		  } else {
			 category = orphan;
		  }
		  CategoryItem item = new CategoryItem();
		  item.Value = "Pattern " + (category.Items.Count + 1);
		  category.Add(item);
		  _templateTable.TableModel.Rows.Add(CategoryItemToRow(item));
	   }

	   private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
		  if (clickedItem != null) {
			 CategoryItem item = clickedItem.Tag as CategoryItem;
			 item.Parent.Remove(item);
			 _templateTable.TableModel.Rows.Remove(clickedItem);
			 clickedItem = null;
		  }
	   }

	   private void _contextIgnoreList_Opening(object sender, CancelEventArgs e) {
		  if (clickedItem != null) {
			 newToolStripMenuItem.Visible = true;
			 deleteToolStripMenuItem.Visible = true;
		  } else {
			 newToolStripMenuItem.Visible = true;
			 deleteToolStripMenuItem.Visible = false;
		  }
	   }

	   private void _categoryTree_AfterSelect(object sender, TreeViewEventArgs e) {
		  ResetSearch();
		  _searchQuery.Text = "";

		  AddToTable(e.Node.Name);
	   }

	   private void TemplateEditor_SizeChanged(object sender, EventArgs e) {
		  AdjustColumnWidth();
	   }

	   private void TemplateEditor_FormClosing(object sender, FormClosingEventArgs e) {
		  if (this.orphan.Count > 0) {
			 if (MessageBox.Show("Some of the patterns you've created are not assigned a category. These patterns will be saved.\nDo you still want to close?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No) {
				e.Cancel = true;	
			 }
		  }
	   }

	   private void _templateTable_MouseDown(object sender, MouseEventArgs e) {
		  if (e.Button == MouseButtons.Right) {
			 int rowIndex = _templateTable.RowIndexAt(e.X, e.Y);
			 if (rowIndex >= 0) {
				clickedItem = _templateTable.TableModel.Rows[rowIndex];
			 }
		  }
	   }

    }
}