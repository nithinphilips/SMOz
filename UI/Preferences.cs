using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EXControls;
using SMOz.Template;
using SMOz.Utilities;

namespace SMOz.UI
{
    public partial class Preferences : Form
    {
	   public Preferences() {
		  InitializeComponent();

		  _ignoreList.Columns.Add(new EXEditableColumnHeader("Name", new TextBox()));
		  _ignoreList.Columns.Add(new EXEditableColumnHeader("Type", TemplateEditor.CreateTypeComboBox()));

		  if (_tabContainer.SelectedTab == _tabIgnoreList) {
			 if (_ignoreList.Items.Count == 0) {
				foreach (CategoryItem item in IgnoreList.Instance.Items) {
				    _ignoreList.Items.Add(TemplateEditor.CategoryItemToListItem(item, null, null));
				}

				_ignoreList.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
				_ignoreList.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

				_ignoreList.Columns[0].Width += 30;
				_ignoreList.Columns[1].Width += 30;
			 }
		  }
	   }

	   private void _ok_Click(object sender, EventArgs e) {
		  if (_ignoreList.Items.Count > 0) {
			 EXListViewItem[] items = new EXListViewItem[_ignoreList.Items.Count];
			 for (int i = 0; i < _ignoreList.Items.Count; i++) {
				items[i] = _ignoreList.Items[i] as EXListViewItem;
			 }
			 TemplateEditor.PersistItemChanges(items);
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


	   EXListViewItem clickedItem;

	   private void _ignoreList_SelectedIndexChanged(object sender, EventArgs e) {
		  if ((_ignoreList.SelectedItems != null) && (_ignoreList.SelectedItems.Count >= 1)) {
			 clickedItem = (EXListViewItem)_ignoreList.SelectedItems[0];
		  }
	   }

	   private void _ignoreList_MouseUp(object sender, MouseEventArgs e) {
		  if (e.Button == MouseButtons.Right) {
			 clickedItem = (EXListViewItem)_ignoreList.GetItemAt(e.X, e.Y);
		  }
	   }

	   private void newToolStripMenuItem_Click(object sender, EventArgs e) {
		  CategoryItem item = new CategoryItem();
		  IgnoreList.Instance.Add(item);
		  _ignoreList.Items.Add(TemplateEditor.CategoryItemToListItem(item, null, null));
	   }

	   private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
		  if(clickedItem != null){
			 IgnoreList.Instance.Remove(clickedItem.TagData as CategoryItem);
			 clickedItem.Remove();
		  }
	   }

    }
}