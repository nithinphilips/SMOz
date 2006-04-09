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

namespace SMOz.UI
{
    public partial class TemplateEditor : Form
    {
	   StartManager manager;
	   IEnumerable<Category> categories;

	   ComboBox _pattern;	  // start item value
	   ComboBox _type;		  // start item type
	   ComboBox _category;	  // start item category

	   public TemplateEditor(StartManager manager, IEnumerable<Category> categories) {
		  InitializeComponent();
		  Initialize();

		  this.manager = manager;
		  this.categories = categories;

		  StartItem[] uncategorized = manager.GetByCategory("");
		  string[] uncategorizedStr = new string[uncategorized.Length];
		  for (int i = 0; i < uncategorized.Length; i++) {
			 uncategorizedStr[i] = uncategorized[i].Name;
		  }
		  this._pattern.Items.AddRange(uncategorizedStr);
		  this._category.Items.AddRange(KnownCategories.Instance.ToArray());

		  AddToListView();

		  if (this._templateList.Items.Count > 0) {
			 this._templateList.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			 this._templateList.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			 this._templateList.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
		  }
	   }

	   public static ComboBox CreateTypeComboBox() {
		  ComboBox _type = new ComboBox();
		  _type.Items.Add(CategoryItemType.String.ToString());
		  _type.Items.Add(CategoryItemType.WildCard.ToString());
		  _type.Items.Add(CategoryItemType.Regex.ToString());
		  _type.DropDownHeight = 40;
		  _type.DropDownStyle = ComboBoxStyle.Simple;
		  return _type;
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

	   public Category FindCategory(string format) {
		  Category result = lookupTable[format];
		  if (result == null) {
			 result = Category.FromFormat(format);
		  }
		  return result;
	   }


	   private void Initialize() {
		  this.Icon = SMOz.Properties.Resources.Application;

		  _pattern = new ComboBox();
		  _pattern.AutoCompleteSource = AutoCompleteSource.ListItems;
		  _pattern.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

		  _category = new ComboBox();
		  _category.AutoCompleteSource = AutoCompleteSource.ListItems;
		  _category.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

		  _type = CreateTypeComboBox();

		  this._templateList.Columns.Add(new EXEditableColumnHeader("Name", _pattern));
		  this._templateList.Columns.Add(new EXEditableColumnHeader("Type", _type));
		  this._templateList.Columns.Add(new EXEditableColumnHeader("Category", _category));
	   }

	   

	   void cmbType_TextUpdate(object sender, EventArgs e) {
		  ComboBox sndr = sender as ComboBox;
		  if (!sndr.Items.Contains(sndr.Text)) {
			 sndr.Text = "String";
		  }
	   }

	   private void AddToListView() {
		  AddToListView(this.categories);
	   }

	   private void AddToListView(IEnumerable<Category> categories) {
		  _templateList.BeginUpdate();
		  _templateList.Items.Clear();
		  foreach (Category category in categories) {
			 ListViewGroup group = FindGroup(category);
			 foreach (CategoryItem item in category.Items) {
				AddToListView(item, group);
			 }
		  }
		  _templateList.EndUpdate();
	   }

	   private void AddToListView(CategoryItem item, ListViewGroup group) {
		  EXListViewItem newItem = new EXListViewItem(item.Value);
		  newItem.SubItems.Add(new EXListViewSubItem(item.Type.ToString()));
		  newItem.SubItems.Add(new EXListViewSubItem(group.Name));
		  newItem.Name = item.Value;
		  newItem.Group = group;
		  _templateList.Items.Add(newItem);
	   }

	   public ListViewGroup FindGroup(Category category) {
		  foreach (ListViewGroup group in _templateList.Groups) {
			 if (group.Name == category.ToFormat()) {
				return group;
			 }
		  }
		  string name = category.Name;
		  if (!string.IsNullOrEmpty(category.RestrictedPath)) {
			 name += " (" + category.RestrictedPath + ")";
		  }
		  ListViewGroup newGrp = new ListViewGroup(category.ToFormat(), name);
		  _templateList.Groups.Add(newGrp);
		  return newGrp;
	   }

	   public ListViewGroup FindGroup(string name) {
		  foreach (ListViewGroup group in _templateList.Groups) {
			 if (group.Name == name) {
				return group;
			 }
		  }
		  ListViewGroup newGrp  = new ListViewGroup(name, name);
		  _templateList.Groups.Add(newGrp);
		  return newGrp;
	   }

	   private void _add_Click(object sender, EventArgs e) {
//		  if (string.IsNullOrEmpty(_category.Text)) {
//			 MessageBox.Show("A category is required. Choose one from the list or create a new one.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
//			 return;
//		  }

		  //if (provider.table.ContainsKey(_pattern.Text)) {
		  //    provider.table.Remove(_pattern.Text);
		  //    _templateList.Items.RemoveByKey(_pattern.Text);
		  //} else {
		  //    _pattern.Items.Remove(_pattern.Text);
		  //}

		  //provider.table.Add(_pattern.Text, _category.Text);
		  //ListViewItem newItem = new ListViewItem(new string[] { _pattern.Text, DeduceType(_pattern.Text) });
		  //newItem.Group = FindGroup(_category.Text);
		  //_templateList.Items.Add(newItem);

		  //if (!string.IsNullOrEmpty(_searchQuery.Text)) {
		  //    // Update Search Results
		  //    DoSearch();
		  //}
	   }

	   private void _templateList_DoubleClick(object sender, EventArgs e) {
		  /*
		  if ((_templateList.SelectedItems != null) && (_templateList.SelectedItems.Count > 0)) {
			 _pattern.Text = _templateList.SelectedItems[0].SubItems[0].Text;
			 _category.Text = _templateList.SelectedItems[0].Group.Name;
		  }
		   */
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

	   private void DoSearch() {
		  _templateList.BeginUpdate();

		  if (string.IsNullOrEmpty(_searchQuery.Text)) {
			 this.Text = "Template Editor";
			 AddToListView();
		  } else {
			 this.Text = "Template Editor - Search";
			 string query = _searchQuery.Text.ToLower();
			 _templateList.Items.Clear();
			 foreach (Category category in this.categories) {
				foreach (CategoryItem item in category.Items) {
				    string item_format = item.ToFormat();
				    if (item_format.ToLower().Contains(query)) {
					   AddToListView(item, FindGroup(category));
				    }
				}
			 }
		  }
		  _templateList.EndUpdate();
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
	   private void _showPreview_Click(object sender, EventArgs e) {
		  	  
	   }

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
	   }

	   /*
	   private void _pattern_TextChanged(object sender, EventArgs e) {
		  if (preview != null) {
			 preview.PreviewList.BeginUpdate();
			 preview.PreviewList.Items.Clear();
			 string star = "";
			 Regex regex = null;

			 if (_pattern.Text.StartsWith("@")) {
				try {
				    regex = new Regex(_pattern.Text.Substring(1), RegexOptions.IgnoreCase);
				} catch (Exception ex) {
				    preview.PreviewList.Items.Clear();
				    preview.PreviewList.Items.Add(ex.Message);
				    preview.PreviewList.EndUpdate();
				    return;
				}
			 }

			 foreach (StartItem item in manager.StartItems) {
				star = item.Name.Contains("\\") ? "*" : "";
				if (_pattern.Text.StartsWith("*")) {
				    // Secondly, Wildcard match
				    if (item.Name.ToLower().Contains(_pattern.Text.ToLower().Substring(1))) {
					   preview.PreviewList.Items.Add(star + item.Name);
				    }
				} else if (_pattern.Text.StartsWith("@")) {
				    // Thirdly, regex comparison
				    if (regex.Match(item.Name).Success) {
					   preview.PreviewList.Items.Add(star + item.Name);
				    }
				} else {
				    // Finally, case insensitive comparison
				    if (string.Compare(item.Name, _pattern.Text, true) == 0) {
					   preview.PreviewList.Items.Add(star + item.Name);
				    }
				}
			 }
			 preview.PreviewList.EndUpdate();
		  }
	    

//		  if (provider.table.ContainsKey(_pattern.Text)) {
//			 _add.Text = "Modify";
//		  } else {
//			 _add.Text = "Add";
//		  }
	   }
	   */


    }
}