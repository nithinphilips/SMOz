using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SMOz.StartMenu;
using EXControls;
using System.IO;
using SMOz.Cleanup;

namespace SMOz.UI
{
    public partial class AssociationBuilder : Form
    {
	   Dictionary<EXListViewItem, StartItem> itemTags;
	   string[] installedPrograms;

	   public AssociationBuilder(StartItem[] startItems) {
		  InitializeComponent();

		  this.Icon = SMOz.Properties.Resources.Application;

		  installedPrograms = SMOz.Cleanup.InstalledProgramList.RetrieveProgramList();
		  itemTags = new Dictionary<EXListViewItem, StartItem>(startItems.Length);

		  ComboBox comboBox = new ComboBox();
		  comboBox.Items.Add("");
		  foreach (string str in installedPrograms) {
			 comboBox.Items.Add(str);
		  }

		  comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		  comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

		  _associationList.Columns.Add(new ColumnHeader("Start Item"));
		  _associationList.Columns.Add(new EXEditableColumnHeader("Program", comboBox));

		  foreach (StartItem startItem in startItems) {
			 EXListViewItem listItem = new EXListViewItem(startItem.Name);
			 listItem.SubItems.Add(new EXListViewSubItem(startItem.Application));
			 _associationList.Items.Add(listItem);
			 itemTags.Add(listItem, startItem);
		  }

		  _associationList.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
		  _associationList.Columns[0].Width += 30;
		  _associationList.Columns[1].Width = _associationList.Width - _associationList.Columns[0].Width - 30;
	   }

	   private void _ok_Click(object sender, EventArgs e) {
		  foreach (EXListViewItem listItem in _associationList.Items) {
			 if (!string.IsNullOrEmpty(listItem.SubItems[1].Text)) {
				itemTags[listItem].Application = listItem.SubItems[1].Text;
			 }
		  }
	   }

	   private void _autoAssociate_Click(object sender, EventArgs e) {
		  float bestResult;
		  if (!float.TryParse(_autoTreshold.Text, out bestResult)) {
			 bestResult = 0.5f;
		  } else {
			 if ((bestResult < 0) || (bestResult > 1)) {
				MessageBox.Show("Tolerence value out of range. Allowed range is [0-1].", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			 }
		  }

		  foreach ( EXListViewItem listItem in _associationList.Items ) {
			 if ( string.IsNullOrEmpty(listItem.SubItems[1].Text) ) {
				listItem.SubItems[1].Text = FindAssociatedProgram(Path.GetFileName(listItem.SubItems[0].Text), bestResult);
			 }
		  }
	   }

	   private string FindAssociatedProgram(string startEntryName, float bestResult) {
		  string bestMatch = "";
//		  float bestResult = 0.5f; // This is the minimum treshold
		  
		  foreach (string installedProgram in installedPrograms) {
			 float similarity = GetSimilarity(startEntryName, installedProgram);
			 if (similarity > bestResult) {
				bestResult = similarity;
				bestMatch = installedProgram;
			 }
		  }
		  return bestMatch;
	   }

	   public float GetSimilarity(string string1, string string2) {
		  float dis = ComputeDistance(string1, string2);
		  float maxLen = string1.Length;
		  if (maxLen < string2.Length)
			 maxLen = string2.Length;
		  if (maxLen == 0.0F)
			 return 1.0F;
		  else
			 return 1.0F - dis / maxLen;
	   }

	   private int ComputeDistance(string s, string t) {
		  int n = s.Length;
		  int m = t.Length;
		  int[,] distance = new int[n + 1, m + 1]; // matrix
		  int cost = 0;
		  if (n == 0) return m;
		  if (m == 0) return n;
		  //init1
		  for (int i = 0; i <= n; distance[i, 0] = i++) ;
		  for (int j = 0; j <= m; distance[0, j] = j++) ;
		  //find min distance
		  for (int i = 1; i <= n; i++) {
			 for (int j = 1; j <= m; j++) {
				cost = (t.Substring(j - 1, 1) ==
				    s.Substring(i - 1, 1) ? 0 : 1);
				distance[i, j] = Min3(distance[i - 1, j] + 1,
				distance[i, j - 1] + 1,
				distance[i - 1, j - 1] + cost);
			 }
		  }
		  return distance[n, m];
	   }

	   public static int Min3(int a, int b, int c) {
		  return a < b ? (a < c ? a : c) : (b < c ? b : c);
	   }

	   private string FindAssociatedProgramA(string startEntryName) {
		  int bestMatch = 0;
		  string bestResult = "";
		  int result = 0;
		  string[] entries = startEntryName.Split(new char[] { ' ' });
		  foreach (string installedProgram in installedPrograms) {

			 if (string.Compare(startEntryName, installedProgram, true) == 0) {
				return installedProgram;
			 }

			 string[] parts = installedProgram.Split(new char[] { ' ' });
			 result = 0;
			 foreach (string entry in entries) {
				foreach (string part in parts) {
				    if (string.Compare(entry, part, true) == 0) {
					   result++;
				    }
				}
			 }
			 if (result > bestMatch) { 
				bestMatch = result;
				bestResult = installedProgram;
			 }
		  }
		  return bestResult;
	   }

	   private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {
		  _autoTreshold.Focus();
	   }

	   private void AssociationBuilder_SizeChanged(object sender, EventArgs e) {
		  _associationList.Columns[1].Width = _associationList.Width - _associationList.Columns[0].Width - 30;
	   }
    }
}