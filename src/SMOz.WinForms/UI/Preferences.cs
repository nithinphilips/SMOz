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
using System.IO;

namespace SMOz.UI
{
    public partial class Preferences : Form
    {
	   bool needReScanning = false;

	   public bool NeedReScanning {
		  get { return needReScanning; }
		  set { needReScanning = value; }
	   }

	   public Preferences() {
		  InitializeComponent();

		  lbLocalDir.Text = string.Format("Your Local Path is: \"{0}\"", Utility.LOCAL_START_ROOT);
		  lbUserDir.Text = string.Format("Your User Path is: \"{0}\"", Utility.USER_START_ROOT);

		  lbLocalDir.Checked = User.Settings.Instance.ScanLocalPath;
		  lbUserDir.Checked = User.Settings.Instance.ScanUserPath;

		  foreach (string s in User.Settings.Instance.AdditionalPaths) {
			 lstAddtPaths.Items.Add(s);
		  }
		  

		  _ignoreList.Columns.Add(new EXEditableColumnHeader("Name", new TextBox()));
		  _ignoreList.Columns.Add(new EXEditableColumnHeader("Type", new ComboBox()));

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
		  if (User.Settings.Instance.ScanLocalPath != lbLocalDir.Checked) { this.needReScanning = true; }
		  if (User.Settings.Instance.ScanUserPath != lbUserDir.Checked) { this.needReScanning = true; }

		  if (this.needReScanning) {
			 if (MessageBox.Show("Due to the changes you made, it is necessary to reload the Start Menu.\nUnsaved work will be lost. Continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) {
				this.DialogResult = DialogResult.None;
				return;
			 }
		  }

		  if (_ignoreList.Items.Count > 0) {
			 EXListViewItem[] items = new EXListViewItem[_ignoreList.Items.Count];
			 for (int i = 0; i < _ignoreList.Items.Count; i++) {
				items[i] = _ignoreList.Items[i] as EXListViewItem;
			 }
			 TemplateEditor.PersistItemChanges(items);
		  }

		  string[] addtPaths = new string[lstAddtPaths.Items.Count];
		  for (int i = 0; i < lstAddtPaths.Items.Count; i++) {
			 addtPaths[i] = lstAddtPaths.Items[i].Text;
			 if (!addtPaths[i].EndsWith(Path.DirectorySeparatorChar.ToString())) { addtPaths[i] += Path.DirectorySeparatorChar; }
		  }
		  
		  User.Settings.Instance.AdditionalPaths = addtPaths;
		  User.Settings.Instance.ScanLocalPath = lbLocalDir.Checked;
		  User.Settings.Instance.ScanUserPath = lbUserDir.Checked;
		  User.Settings.Save();
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

	   private void btAddPath_Click(object sender, EventArgs e) {
		  using (FolderBrowserDialog dlgBrowse = new FolderBrowserDialog()) {
			 if (dlgBrowse.ShowDialog(this) == DialogResult.OK) {
				this.lstAddtPaths.Items.Add(dlgBrowse.SelectedPath);
				this.needReScanning = true;
			 }
		  }
	   }

	   private void btRemovePath_Click(object sender, EventArgs e) {
		  if (lstAddtPaths.SelectedItems.Count > 0) {
			 foreach (ListViewItem var in lstAddtPaths.SelectedItems) {
				lstAddtPaths.Items.Remove(var);
				this.needReScanning = true;
			 }
		  }
	   }

    }
}