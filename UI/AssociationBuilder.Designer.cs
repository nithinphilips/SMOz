namespace SMOz.UI
{
    partial class AssociationBuilder
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
		  this._ok = new System.Windows.Forms.Button();
		  this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		  this.tresholdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		  this._autoTreshold = new System.Windows.Forms.ToolStripTextBox();
		  this._associationList = new EXControls.EXListView();
		  this._autoAssociate = new Microsoft.Samples.SplitButton();
		  this.contextMenuStrip1.SuspendLayout();
		  this.SuspendLayout();
		  // 
		  // _ok
		  // 
		  this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		  this._ok.DialogResult = System.Windows.Forms.DialogResult.OK;
		  this._ok.Location = new System.Drawing.Point(510, 348);
		  this._ok.Name = "_ok";
		  this._ok.Size = new System.Drawing.Size(75, 23);
		  this._ok.TabIndex = 2;
		  this._ok.Text = "OK";
		  this._ok.UseVisualStyleBackColor = true;
		  this._ok.Click += new System.EventHandler(this._ok_Click);
		  // 
		  // contextMenuStrip1
		  // 
		  this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tresholdToolStripMenuItem,
            this._autoTreshold});
		  this.contextMenuStrip1.Name = "contextMenuStrip1";
		  this.contextMenuStrip1.Size = new System.Drawing.Size(213, 49);
		  this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
		  // 
		  // tresholdToolStripMenuItem
		  // 
		  this.tresholdToolStripMenuItem.Enabled = false;
		  this.tresholdToolStripMenuItem.Name = "tresholdToolStripMenuItem";
		  this.tresholdToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
		  this.tresholdToolStripMenuItem.Text = "Treshold:";
		  // 
		  // _autoTreshold
		  // 
		  this._autoTreshold.Margin = new System.Windows.Forms.Padding(10, 1, 1, 1);
		  this._autoTreshold.Name = "_autoTreshold";
		  this._autoTreshold.Size = new System.Drawing.Size(152, 21);
		  this._autoTreshold.Text = "0.5";
		  // 
		  // _associationList
		  // 
		  this._associationList.AllowColumnSort = false;
		  this._associationList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
				    | System.Windows.Forms.AnchorStyles.Left)
				    | System.Windows.Forms.AnchorStyles.Right)));
		  this._associationList.FullRowSelect = true;
		  this._associationList.Location = new System.Drawing.Point(12, 12);
		  this._associationList.Name = "_associationList";
		  this._associationList.OwnerDraw = true;
		  this._associationList.Size = new System.Drawing.Size(573, 330);
		  this._associationList.TabIndex = 0;
		  this._associationList.UseCompatibleStateImageBehavior = false;
		  this._associationList.View = System.Windows.Forms.View.Details;
		  // 
		  // _autoAssociate
		  // 
		  this._autoAssociate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		  this._autoAssociate.AutoSize = true;
		  this._autoAssociate.ContextMenuStrip = this.contextMenuStrip1;
		  this._autoAssociate.Location = new System.Drawing.Point(12, 348);
		  this._autoAssociate.Name = "_autoAssociate";
		  this._autoAssociate.Size = new System.Drawing.Size(75, 23);
		  this._autoAssociate.TabIndex = 1;
		  this._autoAssociate.Text = "Auto";
		  this._autoAssociate.UseVisualStyleBackColor = true;
		  this._autoAssociate.Click += new System.EventHandler(this._autoAssociate_Click);
		  // 
		  // AssociationBuilder
		  // 
		  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		  this.ClientSize = new System.Drawing.Size(597, 380);
		  this.Controls.Add(this._ok);
		  this.Controls.Add(this._associationList);
		  this.Controls.Add(this._autoAssociate);
		  this.MinimizeBox = false;
		  this.Name = "AssociationBuilder";
		  this.ShowInTaskbar = false;
		  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		  this.Text = "Associate Applications";
		  this.SizeChanged += new System.EventHandler(this.AssociationBuilder_SizeChanged);
		  this.contextMenuStrip1.ResumeLayout(false);
		  this.contextMenuStrip1.PerformLayout();
		  this.ResumeLayout(false);
		  this.PerformLayout();

	   }

	   #endregion

	   private EXControls.EXListView _associationList;
	   private System.Windows.Forms.Button _ok;
	   private Microsoft.Samples.SplitButton _autoAssociate;
	   private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	   private System.Windows.Forms.ToolStripTextBox _autoTreshold;
	   private System.Windows.Forms.ToolStripMenuItem tresholdToolStripMenuItem;
    }
}