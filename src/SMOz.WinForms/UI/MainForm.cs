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
 *  Original FileName :  MainForm.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SMOz.Utilities;
using vbAccelerator.Components.ImageList;
using System.IO;
using SMOz.Template;
using SMOz.StartMenu;
using System.Diagnostics;
using SMOz.Commands;
using SMOz.Commands.UI;
using System.Text.RegularExpressions;
using SMOz.Commands.IO;
using Nithin.Philips.Utilities.AboutBox;
using SMOz.Resources.Localization;
using SMOz.User;
using System.Threading;
using System.Xml;

namespace SMOz.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Icon = SMOz.Properties.Resources.Application;
            SetView(View.Tile);
            openToolStripMenuItem.Font = new Font(openToolStripMenuItem.Font, FontStyle.Bold);
            SysImageListHelper.SetTreeViewImageList(_categoryTree, treeIconList, false);
            
            //		  SetupFileSystemWatchers();
        }

        private void Reload()
        {
            this.startManager = new StartManager();
            this.undoQueue.Clear();
            this.redoQueue.Clear();
            this._categoryTree.Nodes.Clear();
            this._itemList.Items.Clear();

            if (File.Exists("Template.ini"))
            {
                OpenTemplate("Template.ini");
            }
            else
            {
                this.template = new TemplateProvider();
                OpenTemplate(this.template);
            }

            foreach (string knownCategory in KnownCategories.Instance)
            {
                foreach (string str in AddCategoryToTree(knownCategory))
                {
                    // This ensures that nodes that are not explicitly listed as categories are scanned
                    AddToManager(str);
                }
            }

            _categoryTree.Sort();
            if (_categoryTree.Nodes.Count > 0)
            {
                _categoryTree.SelectedNode = _categoryTree.Nodes[0];
            }


            startManager.LoadAssociationList(Utility.ASSOCIATION_LIST_FILE_PATH);

            UpdateUndoRedo();
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            startManager.SaveAssociationList(Utility.ASSOCIATION_LIST_FILE_PATH);
            Program.PersistRuntimeData();


            if ((undoQueue.Count > 0) && (undoQueue.Peek().Name != Language.ApplyChanges))
            {
                if (MessageBox.Show(Language.UnsavedChangesMessage, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }



        #region File System Watcher

        // TODO: Only warn user if the changed file system object has been modified by the user,
        //       Otherwise, simply reflect the changes in the UI.
        //
        private void SetupFileSystemWatchers()
        {
            // TODO: This method needs to be changed to support the new
            //		 'additional paths' feature. 
            //    also edit applyChangesToolStripMenuItem_Click()

            localWatcher = new FileSystemWatcher(Utility.LOCAL_START_ROOT);
            userWatcher = new FileSystemWatcher(Utility.USER_START_ROOT);

            localWatcher.IncludeSubdirectories = true;
            userWatcher.IncludeSubdirectories = true;

            localWatcher.Renamed += new RenamedEventHandler(localWatcher_Renamed);
            userWatcher.Renamed += new RenamedEventHandler(localWatcher_Renamed);

            localWatcher.Deleted += new FileSystemEventHandler(localWatcher_Deleted);
            userWatcher.Deleted += new FileSystemEventHandler(localWatcher_Deleted);

            localWatcher.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName;
            userWatcher.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName;

            localWatcher.EnableRaisingEvents = true;
            userWatcher.EnableRaisingEvents = true;
        }

        ReviewChanges review;
        void localWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (review == null)
                {
                    review = new ReviewChanges(true);
                    review.Message = Language.StartMenuModifiedMessage;
                    review.FormClosing += new FormClosingEventHandler(review_FormClosing);
                    review._ok.Text = Language.Close;
                }
                review.Add(Language.Deleted, e.FullPath);
                review.Show(this);
            });
        }

        void review_FormClosing(object sender, FormClosingEventArgs e)
        {
            review = null;
        }

        void localWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (review == null)
                {
                    review = new ReviewChanges(true);
                    review.Message = Language.StartMenuModifiedMessage;
                    review.FormClosing += new FormClosingEventHandler(review_FormClosing);
                    review._ok.Text = Language.Close;
                }
                review.Add(Language.Renamed, e.FullPath);
                review.Show(this);
            });
        }

        #endregion

        public class RadioCheckRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
            {
                RadioButtonRenderer.DrawRadioButton(e.Graphics, e.ImageRectangle.Location, System.Windows.Forms.VisualStyles.RadioButtonState.CheckedNormal);
            }
        }

        FileSystemWatcher localWatcher;
        FileSystemWatcher userWatcher;

        SysImageList treeIconList = new SysImageList(SysImageListSize.smallIcons);
        SysImageList largeListIconList = new SysImageList(SysImageListSize.extraLargeIcons);
        SysImageList smallListIconList = new SysImageList(SysImageListSize.smallIcons);

        TemplateProvider template;
        StartManager startManager;

        private string[] AddCategoryToTree(string name)
        {
            int iconIndex = treeIconList.IconIndex(Utility.USER_START_ROOT, true, ShellIconStateConstants.ShellIconStateNormal);

            string[] tree;
            if (name == "")
            {
                tree = new string[] { name };
            }
            else
            {
                tree = Utility.PathToTree(name);
            }

            TreeNode[] nodes;
            TreeNode node, lastNode = null;
            // look from 
            bool orphan = true;
            for (int j = tree.Length - 1; j >= 0; j--)
            {
                nodes = _categoryTree.Nodes.Find(tree[j], true);
                if (nodes == null || nodes.Length <= 0)
                {
                    // create it
                    node = CreateTreeNode(tree[j], iconIndex);
                    if (lastNode != null)
                    {
                        node.Nodes.Add(lastNode);
                    }
                    lastNode = node;
                }
                else
                {
                    // tree exists
                    orphan = false;
                    if (lastNode != null) { nodes[0].Nodes.Add(lastNode); }
                    break;
                }
            }
            if (orphan)
            {
                _categoryTree.Nodes.Add(lastNode);
            }

            return tree;
        }

        private TreeNode CreateTreeNode(string name, int iconIndex)
        {
            TreeNode node = new TreeNode(name.Substring(name.LastIndexOf(Path.DirectorySeparatorChar) + 1));
            if (name == "") { node.Text = Language.UnCategorizedNodeName; }
            node.ImageIndex = iconIndex;
            node.SelectedImageIndex = iconIndex;
            node.ContextMenuStrip = _categoryTreeContext;

            node.Name = name;
            return node;
        }

        private void AddToManager(string category)
        {

            string localFull = Path.Combine(Utility.LOCAL_START_ROOT, category);
            string userFull = Path.Combine(Utility.USER_START_ROOT, category);

            if (Settings.Instance.ScanLocalPath && Directory.Exists(localFull)) { AddToManager(localFull, category, Utility.LOCAL_START_ROOT.Length); }
            if (Settings.Instance.ScanUserPath && Directory.Exists(userFull)) { AddToManager(userFull, category, Utility.USER_START_ROOT.Length); }

            for (int i = 0; i < Settings.Instance.AdditionalPaths.Length; i++)
            {
                string addt = Settings.Instance.AdditionalPaths[i];
                if (!addt.EndsWith(Path.DirectorySeparatorChar.ToString())) { addt += Path.DirectorySeparatorChar; }

                string addtFull = Path.Combine(addt, category);
                if (Directory.Exists(addtFull))
                {
                    AddToManager(addtFull, category, addt.Length);
                }

            }
        }

        /// <summary>
        /// NOTE: root MUST end with a '\', otherwise bad things will happen. ye be warned!
        /// </summary>
        /// <param name="root"></param>
        /// <param name="category"></param>
        /// <param name="trimCount"></param>
        private void AddToManager(string root, string category, int trimCount)
        {
            Debug.WriteLine("Adding directory: " + "root = " + root + ", category = " + category);
            string[] dirs = Directory.GetDirectories(root);
            for (int i = 0; i < dirs.Length; i++)
            {
                string dir = dirs[i].Remove(0, trimCount);
                if (!KnownCategories.Instance.IsCategory(dir) && !IgnoreList.Instance.Match(dir))
                {
                    startManager.AddItem(dir, StartItemType.Directory, category);
                }
                else
                {
                    Debug.WriteLine("Ignoring: " + dir);
                }
            }

            string[] files = Directory.GetFiles(root);
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i].Remove(0, trimCount);
                if (!IgnoreList.Instance.Match(file))
                {
                    startManager.AddItem(file, StartItemType.File, category);
                }
                else
                {
                    Debug.WriteLine("Ignoring: " + file);
                }
            }
        }

        #region Undo/Redo

        Stack<Command> undoQueue = new Stack<Command>();
        Stack<Command> redoQueue = new Stack<Command>();

        private void AddUndoCommand(Command command)
        {
            AddUndoCommand(command, false);
        }

        private void AddUndoCommand(Command command, bool execute)
        {
            this.redoQueue.Clear(); // cannot redo the same actions because the state is lost.
            if (execute)
            {
                command.Execute();
            }
            this.undoQueue.Push(command);
            UpdateUndoRedo();
        }

        private void UpdateUndoRedo()
        {
            if (undoQueue.Count != 0)
            {
                this.undoToolStripMenuItem.Text = "Undo " + undoQueue.Peek().Name;
                this._undoButton.Enabled = this.undoToolStripMenuItem.Enabled = true;
            }
            else
            {
                this.undoToolStripMenuItem.Text = "Nothing To Undo";
                this._undoButton.Enabled = this.undoToolStripMenuItem.Enabled = false;
            }

            if (redoQueue.Count != 0)
            {
                this.redoToolStripMenuItem.Text = "Redo " + redoQueue.Peek().Name;
                this._redoButton.Enabled = this.redoToolStripMenuItem.Enabled = true;
            }
            else
            {
                this.redoToolStripMenuItem.Text = "Nothing To Redo";
                this._redoButton.Enabled = this.redoToolStripMenuItem.Enabled = false;
            }
        }

        private void Undo()
        {
            Debug.Assert(undoQueue.Count != 0);
            Command cmd = undoQueue.Pop();
            cmd.UnExecute();
            redoQueue.Push(cmd);

            UpdateItemListAfterUndoRedo(cmd, true);
        }

        private void Redo()
        {
            Debug.Assert(redoQueue.Count != 0);
            Command cmd = redoQueue.Pop();
            cmd.Execute();
            undoQueue.Push(cmd);

            UpdateItemListAfterUndoRedo(cmd, false);
        }

        private void UpdateItemListAfterUndoRedo(Command cmd, bool isUndo)
        {
            UpdateItemList(_categoryTree.SelectedNode.Name);
            DeleteStartItemCommand cmdDelete = cmd as DeleteStartItemCommand;
            MoveStartItemCommand cmdMove = cmd as MoveStartItemCommand;
            RenameStartItemCommand cmdRename = cmd as RenameStartItemCommand;
            ListViewItem item = null;
            if (cmdDelete != null)
            {
                item = FindItem(cmdDelete.StartItem);
            }
            else if (cmdMove != null)
            {
                item = FindItem(cmdMove.StartItem);
            }
            else if (cmdRename != null)
            {
                item = FindItem(cmdRename.StartItem);
            }

            if (item != null)
            {
                item.Selected = true;
            }
        }

        /*	   private void UpdateItemListAfterUndoRedo(Command cmd, bool isUndo) {
                  RenameStartItemCommand renameCmd = cmd as RenameStartItemCommand;
                  MoveStartItemCommand moveCmd = cmd as MoveStartItemCommand;
                  DeleteStartItemCommand deleteCmd = cmd as DeleteStartItemCommand;
                  CommandGroup groupCmd = cmd as CommandGroup;
		  
                  if (renameCmd != null) {

                     if (renameCmd.OldCategory == _categoryTree.SelectedNode.Name) {
                        // If the item belongs to selected tree node, restore UI changes
                        foreach (ListViewItem item in _itemList.Items) {
                            Console.WriteLine(item.Name);
                        }
        //				ListViewItem[] items = _itemList.Items.Find(renameCmd.OldName, false);
                        ListViewItem[] items = _itemList.Items.Find(renameCmd.StartItem.RealName, false);
                        Debug.Assert(items.Length == 1);
                        if (renameCmd.StartItem.Type == StartItemType.File) {
                            items[0].SubItems[0].Text = Path.GetFileNameWithoutExtension(renameCmd.StartItem.Name);
                        } else {
                            items[0].SubItems[0].Text = Path.GetFileName(renameCmd.StartItem.Name);
                        }
                     } else {
                        categoryCache.Invalidate(renameCmd.OldCategory);
                     }

                  } else if (moveCmd != null) {

                     categoryCache.Invalidate(moveCmd.OldCategory);
                     categoryCache.Invalidate(moveCmd.NewCategory);

                     if (((moveCmd.OldCategory == _categoryTree.SelectedNode.Name) && isUndo) 
                      || ((moveCmd.NewCategory == _categoryTree.SelectedNode.Name) && !isUndo)) {
                        // item is now in this category
                        _itemList.Items.Add(StartItemToListItem(moveCmd.StartItem));
                     }else if (((moveCmd.OldCategory == _categoryTree.SelectedNode.Name) && !isUndo) 
                      || ((moveCmd.NewCategory == _categoryTree.SelectedNode.Name) && isUndo)) {
                        // item is now removed from this category
                        ListViewItem[] items = _itemList.Items.Find(moveCmd.NewName, false);
                        _itemList.Items.Remove(items[0]);
                      }
	
                  } else if (deleteCmd != null) {
                     categoryCache.Invalidate(deleteCmd.StartItem.Category);

                     if (deleteCmd.StartItem.Category == _categoryTree.SelectedNode.Name) {
                        if (isUndo) {
                            _itemList.Items.Add(StartItemToListItem(deleteCmd.StartItem));
                        } else {
                            ListViewItem[] items = _itemList.Items.Find(deleteCmd.StartItem.Name, false);
                            _itemList.Items.Remove(items[0]);
                        }
                     }

                  } else if (groupCmd != null) {

                     foreach (Command subCmd in groupCmd.Commands) {
                        UpdateItemListAfterUndoRedo(subCmd, isUndo);
                     }

                  } else {
                     categoryCache.Invalidate();
                  }
               }
         */

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
            UpdateUndoRedo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Redo();
            UpdateUndoRedo();
        }

        #endregion

        private ListViewItem[] GetListItems(string category)
        {
            return StartItemsToListItems(startManager.GetByCategory(category));
        }

        private ListViewItem[] StartItemsToListItems(IEnumerable<StartItem> startItems)
        {
            var transformer = from s in startItems
                              select StartItemToListItem(s, false);

            var listItems = new List<ListViewItem>(transformer);


            BeginSetIcons();

            return listItems.ToArray();
        }

        Dictionary<ListViewItem, string> iconInfo = new Dictionary<ListViewItem, string>();
        object iconInfoLock = new object();

        private void BeginSetIcons()
        {
            MethodInvoker setIcons = SetIcons;
            setIcons.BeginInvoke(null, null);
        }

        private void SetIcons()
        {
            lock (iconInfoLock)
            {
                SetIconDelegate setIconDlg = SetIcon;
                foreach (KeyValuePair<ListViewItem, string> pair in iconInfo)
                {
                    if (pair.Key != null)
                    {
                        int imageIndex = largeListIconList.IconIndex(pair.Value, true, ShellIconStateConstants.ShellIconStateNormal);
                        this.BeginInvoke(setIconDlg, new object[] { pair.Key, imageIndex });
                    }
                    else
                    {
                        Console.WriteLine("Empty: " + pair.Value);
                    }
                }
                iconInfo.Clear();
            }
        }

        private delegate void SetIconDelegate(ListViewItem item, int index);
        private void SetIcon(ListViewItem item, int index)
        {
            item.ImageIndex = index;
        }

        private ListViewItem StartItemToListItem(StartItem startItem, bool setIcon)
        {
            ListViewItem listItem;

            string[] validPaths = startItem.GetValidPaths();
            string path = (validPaths.Length >= 1) ? validPaths[0] : "";

            if (startItem.Type == StartItemType.File)
            {
                listItem = new ListViewItem(new string[] { Path.GetFileNameWithoutExtension(startItem.Name), startItem.Location.ToString(), startItem.Application });
            }
            else
            {
                listItem = new ListViewItem(new string[] { Path.GetFileNameWithoutExtension(startItem.Name), startItem.Location.ToString(), startItem.Application });
            }

            listItem.SubItems[1].ForeColor = SystemColors.GrayText;
            listItem.SubItems[2].ForeColor = SystemColors.GrayText;

            listItem.Tag = startItem;
            listItem.Name = startItem.Name;

            if (setIcon)
            {
                listItem.ImageIndex = largeListIconList.IconIndex(path, true, ShellIconStateConstants.ShellIconStateNormal);
            }
            else
            {
                lock (iconInfoLock)
                {
                    iconInfo.Add(listItem, path);
                }
            }

            return listItem;
        }

        private ListViewItem FindItem(StartItem tag)
        {
            for (int i = 0; i < _itemList.Items.Count; i++)
            {
                if (_itemList.Items[i].Tag == tag)
                {
                    return _itemList.Items[i];
                }
            }
            return null;
        }

        private void UpdateItemList()
        {
            UpdateItemList(_categoryTree.SelectedNode.Name);
        }

        private void UpdateItemList(string category)
        {
            _itemList.BeginUpdate();
            _itemList.Items.Clear();
            _itemList.Items.AddRange(GetListItems(category));
            ResizeListHeaders();
            _itemList.EndUpdate();
        }

        private void ResizeListHeaders()
        {
            if ((_itemList.Items.Count > 0) && (_itemList.View == View.Details))
            {
                foreach (ColumnHeader header in _itemList.Columns)
                {
                    if (header.Index == 1)
                    {
                        header.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                    }
                    else
                    {
                        header.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    }
                }
            }
        }

        private void _categoryTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if ((e.Node.Name == Language.SearchResultsNodeName) || (allToolStripMenuItem.Checked))
            {
                UpdateItemList(e.Node.Name);
                this._statusLabel.Text = "Search <" + _searchText.Text + ">. Found " + _itemList.Items.Count.ToString() + " Items";
            }
            else if (!string.IsNullOrEmpty(_searchText.Text))
            {
                DoSearch();
                this._statusLabel.Text = "Search <" + _searchText.Text + ">. Found " + _itemList.Items.Count.ToString() + " Items";
            }
            else
            {
                UpdateItemList(e.Node.Name);
                if (e.Node.Name != "")
                {
                    this._statusLabel.Text = e.Node.Name + ". " + _itemList.Items.Count.ToString() + " Items";
                }
                else
                {
                    this._statusLabel.Text = Language.UnCategorizedNodeName + ". " + _itemList.Items.Count.ToString() + " Items";
                }
            }
        }

        #region Drag & Drop

        string dragDataType = "System.Windows.Forms.ListView+SelectedListViewItemCollection";

        private void _itemList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            _itemList.DoDragDrop(_itemList.SelectedItems, DragDropEffects.Move);
        }

        private void _categoryTree_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(dragDataType))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        TreeNode lastTarget;
        private void _categoryTree_DragOver(object sender, DragEventArgs e)
        {
            _categoryTree.BeginUpdate();
            if (e.Data.GetDataPresent(dragDataType))
            {
                Point point = _categoryTree.PointToClient(new Point(e.X, e.Y));

                TreeNode target = _categoryTree.GetNodeAt(point);
                if (lastTarget != null)
                {
                    lastTarget.BackColor = SystemColors.Window;
                }
                if (target != null)
                {
                    target.BackColor = SystemColors.Highlight;
                    if (!target.IsExpanded)
                    {
                        target.Expand();
                    }
                    e.Effect = DragDropEffects.Move;
                }
                lastTarget = target;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
            _categoryTree.EndUpdate();
        }

        private void _categoryTree_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(dragDataType))
            {
                ListView.SelectedListViewItemCollection draggedItems = e.Data.GetData(dragDataType) as ListView.SelectedListViewItemCollection;

                if (draggedItems != null)
                {
                    Point pt = _categoryTree.PointToClient(new Point(e.X, e.Y));
                    TreeNode target = _categoryTree.GetNodeAt(pt);

                    if (target != null)
                    {
                        target.BackColor = SystemColors.Window;
                        CommandGroup group = new CommandGroup(string.Format(Language.MoveItemsFormat, draggedItems.Count));

                        foreach (ListViewItem listItem in draggedItems)
                        {
                            StartItem item = (StartItem)listItem.Tag;
                            // TODO: Check if target exists. 
                            //   Give options: 'Overwrite' or 'Don't Move'
                            // Overwrite:
                            //	 Delete Target, Move File
                            var existingItems = startManager.GetByCategory(target.Name);
                            if (target.Name != item.Category)
                            {
                                MoveStartItemCommand cmd = new MoveStartItemCommand(item, target.Name);
                                DeleteStartItemCommand cmdDelete = null;
                                bool cancel = false;

                                foreach (StartItem existing in existingItems)
                                {
                                    if ((existing.Name == cmd.NewName) && (existing.Type == item.Type))
                                    {
                                        if (MessageBox.Show("An item named '" + existing.Name + "' already exists. Do still want to move?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                                        {
                                            cancel = true;
                                        }
                                        else
                                        {
                                            cmdDelete = new DeleteStartItemCommand(existing, startManager);
                                        }
                                        break;
                                    }
                                }
                                if (cancel) { continue; } // go to next one
                                if (cmdDelete != null) group.Commands.Add(cmdDelete);
                                group.Commands.Add(cmd);
                                _itemList.Items.Remove(listItem);
                            }
                        }

                        group.Execute();


                        if (group.Commands.Count == 1)
                        {
                            this.AddUndoCommand(group.Commands[0]);
                        }
                        else if (group.Commands.Count > 1)
                        {
                            this.AddUndoCommand(group);
                        }
                    }
                }
            }
        }

        private void _categoryTree_DragLeave(object sender, EventArgs e)
        {
            foreach (TreeNode node in _categoryTree.Nodes)
            {
                node.BackColor = SystemColors.Window;
            }
        }

        #endregion

        private void _itemList_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            StartItem startItem = (StartItem)_itemList.Items[e.Item].Tag;

            if (!string.IsNullOrEmpty(e.Label))
            {
                if (Utility.IsValidFileName(e.Label))
                {
                    string newName = e.Label;
                    if (startItem.Type == StartItemType.File) newName += Path.GetExtension(startItem.Name);

                    RenameStartItemCommand renameCmd = new RenameStartItemCommand(startItem, newName);
                    if (renameCmd.OldName != renameCmd.NewName)
                    {
                        StartItem[] matches = startManager.GetByName(renameCmd.NewName, startItem.Category);
                        if ((matches.Length > 0) && (matches[0].Type == startItem.Type))
                        {
                            e.CancelEdit = true;
                            _statusLabel.Text = "A folder by this name already exists";
                        }
                        else
                        {
                            renameCmd.Execute();
                            AddUndoCommand(renameCmd);
                        }
                    }
                }
                else
                {
                    e.CancelEdit = true;
                    _statusLabel.Text = Language.EnteredNameIsNotValid;
                }
            }
            else
            {
                e.CancelEdit = true;
                _statusLabel.Text = Language.EnteredNameIsNotValid;
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listItem in _itemList.Items)
            {
                listItem.Selected = true;
            }
            _itemList.Select();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((_itemList.SelectedItems != null) && (_itemList.SelectedItems.Count > 0))
            {
                CommandGroup group = new CommandGroup(string.Format(Language.DeleteItemsFormat, _itemList.SelectedItems.Count));
                foreach (ListViewItem listItem in _itemList.SelectedItems)
                {
                    StartItem item = (StartItem)listItem.Tag;
                    DeleteStartItemCommand cmd = new DeleteStartItemCommand(item, this.startManager);
                    group.Commands.Add(cmd);
                    _itemList.Items.Remove(listItem);
                }

                group.Execute();


                if (group.Commands.Count == 1)
                {
                    this.AddUndoCommand(group.Commands[0]);
                }
                else if (group.Commands.Count > 1)
                {
                    this.AddUndoCommand(group);
                }
            }
        }

        #region Cut/Copy/Paste

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }



        System.Windows.Forms.DataFormats.Format clipboardDataType = DataFormats.GetFormat(typeof(StartItem[]).FullName);

        private void Cut()
        {
            List<StartItem> data = new List<StartItem>(_itemList.SelectedItems.Count);

            foreach (ListViewItem item in _itemList.SelectedItems)
            {
                StartItem startItem = (StartItem)item.Tag;
                data.Add(startItem);
                startManager.RemoveItem(startItem);
                _itemList.Items.Remove(item);
            }
            IDataObject dataObj = new DataObject();
            dataObj.SetData(clipboardDataType.Name, false, data.ToArray());
            Clipboard.SetDataObject(dataObj, true);
        }

        private void Paste()
        {
            if (Clipboard.ContainsData(clipboardDataType.Name))
            {
                StartItem[] data;
                IDataObject dataObj = Clipboard.GetDataObject();

                if (dataObj.GetDataPresent(clipboardDataType.Name))
                {
                    data = dataObj.GetData(clipboardDataType.Name) as StartItem[];

                    List<Command> commands = new List<Command>();
                    foreach (StartItem item in data)
                    {
                        if (item.Category != _categoryTree.SelectedNode.Name)
                        {
                            MoveStartItemCommand cmd = new MoveStartItemCommand(item, _categoryTree.SelectedNode.Name);
                            commands.Add(cmd);
                        }
                        _itemList.Items.Add(StartItemToListItem(item, true));
                        startManager.AddItem(item);
                    }

                    if (commands.Count > 0)
                    {
                        CommandGroup group = new CommandGroup(string.Format(Language.MoveItemsFormat, commands.Count), commands);

                        group.Execute();

                        if (group.Commands.Count == 1)
                        {
                            this.AddUndoCommand(group.Commands[0]);
                        }
                        else if (group.Commands.Count > 1)
                        {
                            this.AddUndoCommand(group);
                        }
                    }

                }
            }
        }

        #endregion

        private void _itemList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if ((_itemList.SelectedItems != null) && (_itemList.SelectedItems.Count > 0))
                {
                    _itemList.SelectedItems[0].BeginEdit();
                }
            }
        }

        private void templateEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TemplateEditor editor = new TemplateEditor(startManager, template))
            {
                editor.ShowDialog(this);
            }
        }

        private void applyChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ((redoQueue.Count > 0) && (redoQueue.Peek().Name == Language.ApplyChanges))
            {
                // ...
                Redo();
            }

            List<Command> result = new List<Command>();
            foreach (Command command in undoQueue)
            {
                if (command.Type == SMOz.Commands.CommandType.Group)
                {
                    CommandGroup group = command as CommandGroup;
                    if (group.Name == Language.ApplyChanges)
                    {
                        // Stop when the last Apply Changes is found
                        break;
                    }
                }
                ConvertCommands(result, command);
            }
            if (result.Count <= 0)
            {
                MessageBox.Show(Language.NothingToDoMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            result.Reverse();
            using (ReviewChanges review = new ReviewChanges(false))
            {
                foreach (Command cmd in result)
                {
                    MoveFileCommand cmdMove = cmd as MoveFileCommand;
                    if (cmd.Type == SMOz.Commands.CommandType.IODelete)
                    {
                        review.Add(Command.TypeToString(cmdMove.Type),
                          cmdMove.Source.Replace("Documents and Settings", "...")
                          .Replace("Start Menu\\Programs", "..."), "Trash", cmdMove.MayFail);
                    }
                    else
                    {
                        review.Add(Command.TypeToString(cmdMove.Type),
                          cmdMove.Source.Replace("Documents and Settings", "...")
                          .Replace("Start Menu\\Programs", "..."),
                          "to " + cmdMove.Target.Replace("Documents and Settings", "...")
                          .Replace("Start Menu\\Programs", "..."), cmdMove.MayFail);
                    }
                }
                if (review.ShowDialog(this) == DialogResult.OK)
                {
                    //				localWatcher.EnableRaisingEvents = false;
                    //				userWatcher.EnableRaisingEvents = false;
                    if (ApplyRealChanges(result))
                    {
                        CommandGroup group = new CommandGroup(Language.ApplyChanges, result);
                        AddUndoCommand(group);
                        _statusLabel.Text = "Changes successfully applied";
                    }
                    //				localWatcher.EnableRaisingEvents = true;
                    //				userWatcher.EnableRaisingEvents = true;
                }
            }
        }

        private bool ApplyRealChanges(List<Command> commands)
        {
            bool revert = false;
            int revertBegin = 0;
            Exception ex = null;
            for (int i = 0; i < commands.Count; i++)
            {
                if (!ApplyRealChange(commands[i], out ex))
                {
                    revertBegin = i;
                    revert = true;
                    break;
                }
            }

            if (revert)
            {
                MessageBox.Show(string.Format(Language.OperationFailedMessageFormat, commands[revertBegin].Name, ex.Message), Application.ProductName, MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                for (int i = revertBegin - 1; i >= 0; i--)
                {
                    commands[i].UnExecute();
                }
            }


            return !revert;
        }

        private bool ApplyRealChange(Command command, out Exception exception)
        {
            try
            {
                command.Execute();
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
            exception = null;
            return true;
        }

        private void ConvertCommands(List<Command> result, Command command)
        {
            if (command.Type == SMOz.Commands.CommandType.UIRename)
            {
                result.AddRange(CommandConverter.ConvertRename(command as RenameStartItemCommand));
            }
            else if (command.Type == SMOz.Commands.CommandType.UIMove)
            {
                result.AddRange(CommandConverter.ConvertMove(command as MoveStartItemCommand));
            }
            else if (command.Type == SMOz.Commands.CommandType.UIDelete)
            {
                result.AddRange(CommandConverter.ConvertDelete(command as DeleteStartItemCommand));
            }
            else if (command.Type == SMOz.Commands.CommandType.Group)
            {
                CommandGroup groupCmd = command as CommandGroup;
                for (int i = groupCmd.Commands.Count - 1; i >= 0; i--)
                {
                    ConvertCommands(result, groupCmd.Commands[i]);
                }
            }
        }

        #region Search

        System.Threading.Timer searchTimer;
        private void _searchText_TextChanged(object sender, EventArgs e)
        {
            if (_searchText.BackColor != SystemColors.Window) { _searchText.BackColor = SystemColors.Window; }
            if (string.IsNullOrEmpty(_searchText.Text))
            {
                _searchText.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                _searchText.BorderStyle = BorderStyle.FixedSingle;
            }
            if (searchTimer != null)
            {
                searchTimer.Dispose();
                searchTimer = null;
            }
            searchTimer = new System.Threading.Timer(this.DoSearch, null, 500, System.Threading.Timeout.Infinite);
        }

        private void DoSearch(object state)
        {
            this.Invoke(new MethodInvoker(this.DoSearch));
        }

        private void ResetSearch()
        {
            if (_categoryTree.SelectedNode.Name == Language.SearchResultsNodeName)
            {
                if (_categoryTree.SelectedNode.NextNode != null)
                {
                    UpdateItemList(_categoryTree.SelectedNode.NextNode.Name);
                }
                else if (_categoryTree.SelectedNode.PrevNode != null)
                {
                    UpdateItemList(_categoryTree.SelectedNode.PrevNode.Name);
                }
                else
                {
                    UpdateItemList("");
                }

            }
            else
            {
                UpdateItemList(_categoryTree.SelectedNode.Name);
            }
            _categoryTree.Nodes.RemoveByKey(Language.SearchResultsNodeName);
        }

        private void CreateSearchResultsNode()
        {
            if (!_categoryTree.Nodes.ContainsKey(Language.SearchResultsNodeName))
            {
                TreeNode node = new TreeNode(Language.SearchResultsNodeName);
                node.ImageIndex = 4;
                node.SelectedImageIndex = 4;
                node.Name = Language.SearchResultsNodeName;
                _categoryTree.Nodes.Insert(-1, node);
            }
        }

        private void DoSearch()
        {
            _itemList.BeginUpdate();

            if (string.IsNullOrEmpty(_searchText.Text))
            {
                ResetSearch();
            }
            else
            {
                string pattern = _searchText.Text;
                if (!regexToolStripMenuItem.Checked)
                {
                    pattern = ".*" + Regex.Escape(pattern) + ".*";
                }

                StartItem[] result;
                try
                {
                    if (allToolStripMenuItem.Checked)
                    {
                        CreateSearchResultsNode();
                        result = startManager.GetByPattern(pattern);
                    }
                    else
                    {
                        _categoryTree.Nodes.RemoveByKey(Language.SearchResultsNodeName);
                        result = startManager.GetByPattern(pattern, _categoryTree.SelectedNode.Name);
                    }
                }
                catch (Exception)
                {
                    _searchText.BackColor = Color.IndianRed;
                    UpdateItemList(_categoryTree.SelectedNode.Name);
                    _itemList.EndUpdate();
                    return;
                }

                _itemList.Items.Clear();
                ListViewItem[] listItems = StartItemsToListItems(result);
                _itemList.Items.AddRange(listItems);
                if (allToolStripMenuItem.Checked)
                {
                    _categoryTree.SelectedNode = _categoryTree.Nodes.Find(Language.SearchResultsNodeName, false)[0];
                }
            }
            _itemList.EndUpdate();
        }


        private void selectedCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allToolStripMenuItem.Checked = false;
            selectedCategoryToolStripMenuItem.Checked = true;
            if (!string.IsNullOrEmpty(_searchText.Text))
            {
                DoSearch();
            }
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allToolStripMenuItem.Checked = true;
            selectedCategoryToolStripMenuItem.Checked = false;
            if (!string.IsNullOrEmpty(_searchText.Text))
            {
                DoSearch();
            }
        }


        private void regexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        #endregion

        private void applyTemplateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ApplyTemplate(StartCategorizer.Categorize(template, startManager), "Start Menu");
        }

        TreeNode clickedNode;
        private void applyTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = clickedNode;
            if (node != null)
            {

                ApplyTemplate(StartCategorizer.Categorize(template, startManager, node.Name, false), node.Name);


            }
        }

        void ApplyTemplate(MoveStartItemCommand[] commands, string name)
        {
            if ((commands == null) || (commands.Length <= 0))
            {
                MessageBox.Show(Language.NothingToDoMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (ReviewChanges review = new ReviewChanges(true))
            {
                foreach (MoveStartItemCommand cmd in commands)
                {
                    foreach (string str in AddCategoryToTree(cmd.NewCategory))
                    {
                        // Ensure that the target category is visible to user
                        AddToManager(str);
                    }
                    review.Add("Move", "'" + cmd.OldName + "' to '" + cmd.NewName + "'");
                }

                if (review.ShowDialog(this) == DialogResult.OK)
                {
                    CommandGroup group = new CommandGroup(Language.ApplyTemplate, commands);
                    AddUndoCommand(group, true);
                    UpdateItemList();
                }
            }

        }


        private void _categoryTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                clickedNode = _categoryTree.GetNodeAt(e.Location);
            }
        }

        private void _categoryTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Apps)
            {
                clickedNode = _categoryTree.SelectedNode;
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clickedItem != null)
            {
                clickedItem.BeginEdit();
            }
        }

        private void _itemListContext_Opening(object sender, CancelEventArgs e)
        {
            if (clickedItem != null)
            {
                StartItem item = clickedItem.Tag as StartItem;
                if (item.Type == StartItemType.File)
                {
                    convertToCategoryToolStripMenuItem.Enabled = false;
                }
                else
                {
                    convertToCategoryToolStripMenuItem.Enabled = true;
                }
                moveToToolStripMenuItem.DropDownItems.Clear();
                foreach (string category in KnownCategories.Instance)
                {
                    moveToToolStripMenuItem.DropDownItems.Add(category);
                }
            }
        }

        private void convertToCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((_itemList.SelectedItems != null) && (_itemList.SelectedItems.Count > 1))
            {
                List<StartItem> selectedItems = new List<StartItem>(_itemList.SelectedItems.Count);
                foreach (ListViewItem item in _itemList.SelectedItems)
                {
                    StartItem startItem = item.Tag as StartItem;
                    if (startItem.Type == StartItemType.Directory)
                    {
                        selectedItems.Add(startItem);
                    }
                }
                if (MessageBox.Show("Convert " + selectedItems.Count + " items to category?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    foreach (StartItem startItem in selectedItems)
                    {
                        ConvertToCategory(startItem);
                    }
                }
            }
            else if (clickedItem != null)
            {
                if (((StartItem)clickedItem.Tag).Type == StartItemType.File)
                {
                    MessageBox.Show("Sorry, but '" + clickedItem.Name + "' is a file. Only folders can be made into categories", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                else if (MessageBox.Show("Make '" + clickedItem.Name + "' a category?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    ConvertToCategory(clickedItem.Tag as StartItem);
                }
            }
        }

        private void cleanupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CleanUp();
            // 1. Delete Empty Category Folders

            // 2. Delete Empty Folders

            // 3. Delete Folders with links that point to nowhere
        }

        private void CleanUp()
        {
            List<string> installedPrograms = new List<string>(SMOz.Cleanup.InstalledProgramList.RetrieveProgramList());
            string result = "";
            foreach (StartItem startItem in startManager.StartItems)
            {
                if (!string.IsNullOrEmpty(startItem.Application))
                {
                    if (!installedPrograms.Contains(startItem.Application))
                    {
                        result += string.Format("{0} => {1}\n", startItem.Name, startItem.Application);
                        Console.WriteLine("{0} => {1}\n", startItem.Name, startItem.Application);
                    }
                }
            }
            if (result != "")
            {
                MessageBox.Show("The following start menu items belong to uninstalled programs:\n\n" + result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Everything is OK.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void newCategoryHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clickedNode.Name != "")
            {
                AddCategoryAsk(clickedNode.Name + Path.DirectorySeparatorChar);
                //			 TreeNode node = CreateTreeNode("New Category", 4);
                //			 clickedNode.Nodes.Add(node);
                //			 _categoryTree.SelectedNode = node;
                //			 node.BeginEdit();
            }
            else
            {
                //			 TreeNode node = CreateTreeNode("New Category", 4);
                //			 _categoryTree.Nodes.Add(node);
                //			 _categoryTree.SelectedNode = node;
                //			 node.BeginEdit();
                AddCategoryAsk(clickedNode.Name);
            }
        }

        private void _categoryTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Label))
            {
                if (Utility.IsValidFileName(e.Label))
                {

                }
                else
                {
                    e.CancelEdit = true;
                    _statusLabel.Text = Language.EnteredNameIsNotValid;
                }
            }
            else
            {
                e.CancelEdit = true;
                _statusLabel.Text = Language.EnteredNameIsNotValid;
            }
        }

        private void _categoryTree_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = true;
            _statusLabel.Text = "Currently categories cannot be renamed.";
        }

        private void addCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCategoryAsk();
        }

        private void ConvertToCategory(StartItem startItem)
        {
            KnownCategories.Instance.Add(startItem.Name);
            startManager.RemoveItem(startItem);
            foreach (string str in AddCategoryToTree(startItem.Name))
            {
                // This ensures that nodes that are not explicitly listed as categories are scanned
                AddToManager(str);
            }
            startManager.LoadAssociationList(Utility.ASSOCIATION_LIST_FILE_PATH);
            _categoryTree.SelectedNode = _categoryTree.Nodes.Find(startItem.Name, true)[0];
        }

        private void AddCategory(string name)
        {
            KnownCategories.Instance.Add(name);
            startManager.RemoveAllItems(name);
            foreach (string str in AddCategoryToTree(name))
            {
                // This ensures that nodes that are not explicitly listed as categories are scanned
                AddToManager(str);
            }
            startManager.LoadAssociationList(Utility.ASSOCIATION_LIST_FILE_PATH); // Update
            _categoryTree.SelectedNode = _categoryTree.Nodes.Find(name, true)[0];
        }

        private void AddCategoryAsk()
        {
            AddCategoryAsk("");
        }
        private void AddCategoryAsk(string suggested)
        {
            using (NewCategory editor = new NewCategory(suggested))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    AddCategory(editor.CategoryName);
                }
            }
        }

        ListViewItem clickedItem;
        private void _itemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Apps)
            {
                if ((_itemList.SelectedItems != null) && (_itemList.SelectedItems.Count > 0))
                {
                    clickedItem = _itemList.SelectedItems[0];
                }
            }
        }

        private void _itemList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                clickedItem = _itemList.GetItemAt(e.X, e.Y);
            }
        }

        private void _itemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_itemList.SelectedItems != null && _itemList.SelectedItems.Count != 0)
            {
                if (_itemList.SelectedItems.Count == 1)
                {
                    StartItem item = _itemList.SelectedItems[0].Tag as StartItem;
                    _statusLabel.Text = string.Format("{0}. {1} ({2})", item.Name, item.Type, item.Location);
                }
                else
                {
                    _statusLabel.Text = _itemList.SelectedItems.Count + " Items Selected";
                }
            }
            else
            {
                _statusLabel.Text = Language.NothingSelected;
            }
        }

        private void _categoryTreeContext_Opening(object sender, CancelEventArgs e)
        {
            if (clickedNode != null)
            {
                if (clickedNode.Name == string.Empty)
                {
                    hideToolStripMenuItem.Enabled = false;
                    removeToolStripMenuItem.Enabled = false;
                }
                else
                {
                    hideToolStripMenuItem.Enabled = true;
                    removeToolStripMenuItem.Enabled = true;
                }
            }
        }

        #region Remove Category

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clickedNode != null)
            {
                RemoveCategory(clickedNode);
            }
        }

        private void RemoveCategory(TreeNode parent)
        {
            // remove the item and all children
            RecursiveRemoveCategory(parent);
            // add it back to parent category
            string grandParent = Path.GetDirectoryName(parent.Name);

            StartItem item = new StartItem(parent.Name, StartItemType.Directory, grandParent);
            if (item.HasLocal || item.HasUser)
            {
                startManager.AddItem(item);
                UpdateItemList(grandParent);
                _itemList.Items[parent.Name].Focused = true;
                _itemList.Focus();
            }
            _categoryTree.SelectedNode = _categoryTree.Nodes.Find(grandParent, true)[0];
        }

        private void RecursiveRemoveCategory(TreeNode parent)
        {
            foreach (TreeNode child in parent.Nodes)
            {
                RecursiveRemoveCategory(child);
            }
            _categoryTree.Nodes.Remove(parent);
            KnownCategories.Instance.RemoveCategory(parent.Name);
            foreach (StartItem item in startManager.GetByCategory(parent.Name))
            {
                startManager.RemoveItem(item);
            }
        }

        #endregion

        #region ListView View

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetView(View.Details);
        }

        private void tileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetView(View.Tile);
        }

        private void iconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetView(View.LargeIcon);
        }

        private void smallIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetView(View.SmallIcon);
        }

        private void _toggleView_ButtonClick(object sender, EventArgs e)
        {
            if (_itemList.View == View.Tile)
            {
                SetView(View.LargeIcon);
            }
            else if (_itemList.View == View.LargeIcon)
            {
                SetView(View.SmallIcon);
            }
            else if (_itemList.View == View.SmallIcon)
            {
                SetView(View.Details);
            }
            else if (_itemList.View == View.Details)
            {
                SetView(View.Tile);
            }
        }

        private void SetView(View view)
        {
            _itemList.View = view;
            _itemList.Alignment = ListViewAlignment.Top;

            detailsToolStripMenuItem.Checked = false;
            tileToolStripMenuItem.Checked = false;
            iconsToolStripMenuItem.Checked = false;
            smallIconsToolStripMenuItem.Checked = false;

            if (view == View.Tile)
            {
                tileToolStripMenuItem.Checked = true;

            }
            else if (view == View.Details)
            {
                detailsToolStripMenuItem.Checked = true;
                ResizeListHeaders();
            }
            else if (view == View.SmallIcon)
            {
                _itemList.Alignment = ListViewAlignment.Left;
                smallIconsToolStripMenuItem.Checked = true;
            }
            else if (view == View.LargeIcon)
            {
                iconsToolStripMenuItem.Checked = true;
            }
            SysImageListHelper.SetListViewImageList(_itemList, largeListIconList, false);
            SysImageListHelper.SetListViewImageList(_itemList, smallListIconList, false);
        }

        #endregion

        #region Save Current Configuration

        private void saveCurrentConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
        }

        private void SaveConfiguration()
        {
            using (SaveFileDialog dlgSave = new SaveFileDialog())
            {
                dlgSave.FileName = "Start Menu";
                dlgSave.Filter = "Configuration File (*.ini)|*.ini|XML File (*.xml)|*.xml";
                if (dlgSave.ShowDialog(this) == DialogResult.OK)
                {
                    if (dlgSave.FilterIndex == 1)
                    {
                        SaveIniConfiguration(dlgSave.FileName);
                    }
                    else if (dlgSave.FilterIndex == 2)
                    {
                        SaveXmlConfiguration(dlgSave.FileName);
                    }
                }
            }
        }

        private void SaveIniConfiguration(string fileName)
        {
            Ini.IniWriter writer = new SMOz.Ini.IniWriter();
            foreach (StartItem item in startManager.StartItems)
            {
                if (!string.IsNullOrEmpty(item.Category))
                {
                    writer.AddValue(Path.GetFileName(item.Name), item.Category);
                }
            }
            writer.Save(fileName);
        }
        private void SaveXmlConfiguration(string fileName)
        {
            Stream stream = File.OpenWrite(fileName);
            XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);

            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument(true);

            writer.WriteStartElement("Smoz");
            Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>();
            foreach (StartItem item in startManager.StartItems)
            {
                if (!groups.ContainsKey(item.Category))
                {
                    groups.Add(item.Category, new List<string>());
                }
                groups[item.Category].Add(Path.GetFileName(item.Name));
            }

            foreach (KeyValuePair<string, List<string>> pair in groups)
            {
                writer.WriteStartElement("Category");
                writer.WriteAttributeString("name", pair.Key);
                foreach (string item in pair.Value)
                {
                    writer.WriteElementString("Item", item);
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        #endregion

        #region Locate On Disk

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clickedItem != null)
            {
                LocateOnDisk(clickedItem.Tag as StartItem);
            }
        }

        private void _itemList_DoubleClick(object sender, EventArgs e)
        {
            if ((_itemList.SelectedItems != null) && (_itemList.SelectedItems.Count > 0))
            {
                LocateOnDisk(_itemList.SelectedItems[0].Tag as StartItem);
            }
        }

        private void LocateOnDisk(StartItem item)
        {
            string[] validPaths = item.GetValidPaths();
            for (int i = 0; i < validPaths.Length; i++)
            {
                LocateOnDisk(validPaths[i]);
            }
        }

        public void LocateOnDisk(string path)
        {
            Process.Start("explorer.exe", "/select," + path);
        }

        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clickedNode != null)
            {
                HideCategory(clickedNode);
            }
        }

        private void HideCategory(TreeNode node)
        {
            IgnoreList.Instance.Add(new CategoryItem(node.Name, CategoryItemType.String));
            RecursiveRemoveCategory(node);
        }

        private void associateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AssociationBuilder asBuilder = new AssociationBuilder(this.startManager.StartItems))
            {
                if (asBuilder.ShowDialog(this) == DialogResult.OK)
                {
                    this.startManager.SaveAssociationList(Utility.ASSOCIATION_LIST_FILE_PATH);
                }
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Preferences prefs = new Preferences())
            {
                prefs.ShowDialog(this);
                if (prefs.NeedReScanning)
                {
                    Reload();
                }
            }
        }

        private void saveTemplateAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveTemplate();
        }

        private void openTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTemplate();
        }

        private void mergeTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MergeTemplate();
        }

        private void SaveTemplate()
        {
            using (SaveFileDialog dlgSave = new SaveFileDialog())
            {
                dlgSave.Filter = "Configuration Files (*.ini)|*.ini|All Files (*.*)|*.*";
                dlgSave.FileName = "Template.ini";
                if (dlgSave.ShowDialog(this) == DialogResult.OK)
                {
                    SaveTemplate(dlgSave.FileName);
                }
            }
        }

        private void SaveTemplate(string fileName)
        {
            Template.TemplateHelper.Save(template, fileName);
        }

        private void OpenTemplate()
        {
            using (OpenFileDialog dlgOpen = new OpenFileDialog())
            {
                dlgOpen.Filter = "Configuration Files (*.ini)|*.ini|All Files (*.*)|*.*";
                if (dlgOpen.ShowDialog(this) == DialogResult.OK)
                {
                    OpenTemplate(dlgOpen.FileName);
                }
            }
        }

        private void OpenTemplate(string path)
        {
            TemplateProvider template = Template.TemplateHelper.Build(path);
            OpenTemplate(template);
            this.template = template;
        }

        private void OpenTemplate(TemplateProvider template)
        {
            for (int i = -1; i < template.Count; i++)
            {
                string current;
                current = i == -1 ? "" : template[i].Name;
                foreach (string str in AddCategoryToTree(current))
                {
                    // This ensures that nodes that are not explicitly listed as categories are scanned
                    KnownCategories.Instance.Add(str);
                    startManager.RemoveAllItems(str);
                    AddToManager(str);
                }
            }
            startManager.LoadAssociationList(Utility.ASSOCIATION_LIST_FILE_PATH);
        }

        private void MergeTemplate()
        {
            using (OpenFileDialog dlgOpen = new OpenFileDialog())
            {
                dlgOpen.Title = "Choose template to merge";
                dlgOpen.Filter = "Configuration Files (*.ini)|*.ini|All Files (*.*)|*.*";
                if (dlgOpen.ShowDialog(this) == DialogResult.OK)
                {
                    MergeTemplate(dlgOpen.FileName);
                }
            }
        }

        private void MergeTemplate(string path)
        {
            TemplateProvider template = Template.TemplateHelper.Build(path);
            // Add to currently loaded template
            this.template.Merge(template);
            OpenTemplate(this.template);
        }

        private void _newTemplate_Click(object sender, EventArgs e)
        {
            this.template = new TemplateProvider();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("http://smoz.sourceforge.net/");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox about = new AboutBox())
            {
                List<InformationPage> ipages = new List<InformationPage>(3);
                ipages.Add(new InformationPage(Language.Version, Program.GetAboutVersion()));
                ipages.Add(new InformationPage(Language.License, Language.AboutLicense));
                ipages.Add(new InformationPage(Language.Contributors, Program.GetAboutContributors()));
                about.PageCollection = ipages;

                about.ProductName = "Start Menu Organizer";
                about.ProductVersion = Application.ProductVersion;
                about.ProductCopyright = "(C) 2004-2006 Nithin Philips";
                about.ProductUrl = "http://smoz.sourceforge.net/";

                about.ProductLargeIcon = Properties.Resources.App;
                about.Icon = Properties.Resources.Application;

                about.ShowDialog(this);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Reload();
#if RELEASE
            associateToolStripMenuItem.Visible = false;
            cleanupToolStripMenuItem.Visible = false;
#endif
        }


    }

}