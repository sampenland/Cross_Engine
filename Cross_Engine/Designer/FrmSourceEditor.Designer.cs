namespace Cross_Engine.Designer
{
    partial class FrmSourceEditor
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
            lstFunctions = new ListBox();
            codeEditor = new ScintillaNET.Scintilla();
            msMenu = new MenuStrip();
            tsFile = new ToolStripMenuItem();
            tsSave = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            msMenu.SuspendLayout();
            SuspendLayout();
            // 
            // lstFunctions
            // 
            lstFunctions.Dock = DockStyle.Left;
            lstFunctions.FormattingEnabled = true;
            lstFunctions.Location = new Point(0, 28);
            lstFunctions.Name = "lstFunctions";
            lstFunctions.Size = new Size(187, 576);
            lstFunctions.TabIndex = 0;
            lstFunctions.SelectedValueChanged += lstFunctions_SelectedValueChanged;
            // 
            // codeEditor
            // 
            codeEditor.AutocompleteListSelectedBackColor = Color.FromArgb(0, 120, 215);
            codeEditor.Dock = DockStyle.Fill;
            codeEditor.LexerName = "";
            codeEditor.Location = new Point(187, 28);
            codeEditor.Name = "codeEditor";
            codeEditor.ScrollWidth = 57;
            codeEditor.Size = new Size(820, 576);
            codeEditor.TabIndex = 1;
            // 
            // msMenu
            // 
            msMenu.ImageScalingSize = new Size(20, 20);
            msMenu.Items.AddRange(new ToolStripItem[] { tsFile });
            msMenu.Location = new Point(0, 0);
            msMenu.Name = "msMenu";
            msMenu.Size = new Size(1007, 28);
            msMenu.TabIndex = 2;
            msMenu.Text = "menuStrip1";
            // 
            // tsFile
            // 
            tsFile.DropDownItems.AddRange(new ToolStripItem[] { tsSave, closeToolStripMenuItem });
            tsFile.Name = "tsFile";
            tsFile.Size = new Size(46, 24);
            tsFile.Text = "File";
            // 
            // tsSave
            // 
            tsSave.Name = "tsSave";
            tsSave.Size = new Size(224, 26);
            tsSave.Text = "Save";
            tsSave.Click += tsSave_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(224, 26);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // FrmSourceEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1007, 604);
            Controls.Add(codeEditor);
            Controls.Add(lstFunctions);
            Controls.Add(msMenu);
            MainMenuStrip = msMenu;
            Name = "FrmSourceEditor";
            Text = "Code Editor";
            msMenu.ResumeLayout(false);
            msMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstFunctions;
        private ScintillaNET.Scintilla codeEditor;
        private MenuStrip msMenu;
        private ToolStripMenuItem tsFile;
        private ToolStripMenuItem tsSave;
        private ToolStripMenuItem closeToolStripMenuItem;
    }
}