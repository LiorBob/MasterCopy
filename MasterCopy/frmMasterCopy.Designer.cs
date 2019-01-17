namespace MasterCopy
{
    partial class frmMasterCopy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMasterCopy));
            this.tmrCheckNewClipboardContent = new System.Windows.Forms.Timer(this.components);
            this.lstLatestClipboardContent = new System.Windows.Forms.ListBox();
            this.lblSelectTextToCopy = new System.Windows.Forms.Label();
            this.lblSelectPictureToCopy = new System.Windows.Forms.Label();
            this.lstLatestClipboardFiles = new System.Windows.Forms.ListBox();
            this.lblSelectFileToCopy = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tmrCheckNewClipboardContent
            // 
            this.tmrCheckNewClipboardContent.Enabled = true;
            this.tmrCheckNewClipboardContent.Tick += new System.EventHandler(this.tmrCheckNewClipboardContent_Tick);
            // 
            // lstLatestClipboardContent
            // 
            this.lstLatestClipboardContent.FormattingEnabled = true;
            this.lstLatestClipboardContent.HorizontalScrollbar = true;
            this.lstLatestClipboardContent.Location = new System.Drawing.Point(32, 55);
            this.lstLatestClipboardContent.Name = "lstLatestClipboardContent";
            this.lstLatestClipboardContent.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstLatestClipboardContent.Size = new System.Drawing.Size(219, 394);
            this.lstLatestClipboardContent.TabIndex = 0;
            this.lstLatestClipboardContent.DoubleClick += new System.EventHandler(this.lstLatestClipboardContent_DoubleClick);
            this.lstLatestClipboardContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstLatestClipboardContent_KeyDown);
            this.lstLatestClipboardContent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstLatestClipboardContent_KeyPress);
            // 
            // lblSelectTextToCopy
            // 
            this.lblSelectTextToCopy.Location = new System.Drawing.Point(29, 20);
            this.lblSelectTextToCopy.Name = "lblSelectTextToCopy";
            this.lblSelectTextToCopy.Size = new System.Drawing.Size(228, 32);
            this.lblSelectTextToCopy.TabIndex = 1;
            this.lblSelectTextToCopy.Text = "Double-Click on text item, to copy to clipboard. Press \"Delete\" to remove selecte" +
    "d item";
            // 
            // lblSelectPictureToCopy
            // 
            this.lblSelectPictureToCopy.Location = new System.Drawing.Point(352, 20);
            this.lblSelectPictureToCopy.Name = "lblSelectPictureToCopy";
            this.lblSelectPictureToCopy.Size = new System.Drawing.Size(445, 32);
            this.lblSelectPictureToCopy.TabIndex = 2;
            this.lblSelectPictureToCopy.Text = "Double-Click on a picture, to copy to clipboard.  Ctrl+A, then Delete, removes al" +
    "l pictures. Right-Click on a picture, to view its original size. Click again for" +
    " main screen";
            // 
            // lstLatestClipboardFiles
            // 
            this.lstLatestClipboardFiles.FormattingEnabled = true;
            this.lstLatestClipboardFiles.HorizontalScrollbar = true;
            this.lstLatestClipboardFiles.Location = new System.Drawing.Point(32, 533);
            this.lstLatestClipboardFiles.Name = "lstLatestClipboardFiles";
            this.lstLatestClipboardFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstLatestClipboardFiles.Size = new System.Drawing.Size(219, 147);
            this.lstLatestClipboardFiles.TabIndex = 3;
            this.lstLatestClipboardFiles.DoubleClick += new System.EventHandler(this.lstLatestClipboardFiles_DoubleClick);
            this.lstLatestClipboardFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstLatestClipboardFiles_KeyDown);
            // 
            // lblSelectFileToCopy
            // 
            this.lblSelectFileToCopy.Location = new System.Drawing.Point(29, 498);
            this.lblSelectFileToCopy.Name = "lblSelectFileToCopy";
            this.lblSelectFileToCopy.Size = new System.Drawing.Size(222, 32);
            this.lblSelectFileToCopy.TabIndex = 4;
            this.lblSelectFileToCopy.Text = "Double-Click on file item, to copy to clipboard. Press \"Delete\" to remove selecte" +
    "d item";
            // 
            // frmMasterCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 694);
            this.Controls.Add(this.lblSelectFileToCopy);
            this.Controls.Add(this.lstLatestClipboardFiles);
            this.Controls.Add(this.lblSelectPictureToCopy);
            this.Controls.Add(this.lblSelectTextToCopy);
            this.Controls.Add(this.lstLatestClipboardContent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMasterCopy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Master Copy";
            this.Click += new System.EventHandler(this.frmMasterCopy_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMasterCopy_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrCheckNewClipboardContent;
        private System.Windows.Forms.ListBox lstLatestClipboardContent;
        private System.Windows.Forms.Label lblSelectTextToCopy;
        private System.Windows.Forms.Label lblSelectPictureToCopy;
        private System.Windows.Forms.ListBox lstLatestClipboardFiles;
        private System.Windows.Forms.Label lblSelectFileToCopy;
    }
}

