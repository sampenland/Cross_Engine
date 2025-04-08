namespace Cross_Engine.Designer
{
    partial class FrmEditor
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
            label1 = new Label();
            lstScenes = new ListBox();
            btnRun = new Button();
            ndGameHeight = new NumericUpDown();
            ndGameWidth = new NumericUpDown();
            label2 = new Label();
            btnSceneSource = new Button();
            btnAddScene = new Button();
            btnRemoveScene = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            errorLogToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)ndGameHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ndGameWidth).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 50);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 0;
            label1.Text = "Scenes";
            // 
            // lstScenes
            // 
            lstScenes.FormattingEnabled = true;
            lstScenes.Location = new Point(12, 143);
            lstScenes.Name = "lstScenes";
            lstScenes.Size = new Size(150, 224);
            lstScenes.TabIndex = 1;
            // 
            // btnRun
            // 
            btnRun.Location = new Point(1216, 41);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(94, 29);
            btnRun.TabIndex = 2;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // ndGameHeight
            // 
            ndGameHeight.Location = new Point(1113, 43);
            ndGameHeight.Maximum = new decimal(new int[] { 1080, 0, 0, 0 });
            ndGameHeight.Minimum = new decimal(new int[] { 320, 0, 0, 0 });
            ndGameHeight.Name = "ndGameHeight";
            ndGameHeight.Size = new Size(97, 27);
            ndGameHeight.TabIndex = 3;
            ndGameHeight.Value = new decimal(new int[] { 720, 0, 0, 0 });
            // 
            // ndGameWidth
            // 
            ndGameWidth.Location = new Point(999, 43);
            ndGameWidth.Maximum = new decimal(new int[] { 1920, 0, 0, 0 });
            ndGameWidth.Minimum = new decimal(new int[] { 640, 0, 0, 0 });
            ndGameWidth.Name = "ndGameWidth";
            ndGameWidth.Size = new Size(86, 27);
            ndGameWidth.TabIndex = 4;
            ndGameWidth.Value = new decimal(new int[] { 1280, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1091, 45);
            label2.Name = "label2";
            label2.Size = new Size(16, 20);
            label2.TabIndex = 5;
            label2.Text = "x";
            // 
            // btnSceneSource
            // 
            btnSceneSource.Location = new Point(12, 108);
            btnSceneSource.Name = "btnSceneSource";
            btnSceneSource.Size = new Size(150, 29);
            btnSceneSource.TabIndex = 6;
            btnSceneSource.Text = "Edit Scene Source";
            btnSceneSource.UseVisualStyleBackColor = true;
            btnSceneSource.Click += btnSceneSource_Click;
            // 
            // btnAddScene
            // 
            btnAddScene.Location = new Point(12, 73);
            btnAddScene.Name = "btnAddScene";
            btnAddScene.Size = new Size(150, 29);
            btnAddScene.TabIndex = 7;
            btnAddScene.Text = "Add Scene";
            btnAddScene.UseVisualStyleBackColor = true;
            btnAddScene.Click += btnAddScene_Click;
            // 
            // btnRemoveScene
            // 
            btnRemoveScene.Location = new Point(12, 373);
            btnRemoveScene.Name = "btnRemoveScene";
            btnRemoveScene.Size = new Size(150, 29);
            btnRemoveScene.TabIndex = 8;
            btnRemoveScene.Text = "Remove Scene";
            btnRemoveScene.UseVisualStyleBackColor = true;
            btnRemoveScene.Click += btnRemoveScene_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1322, 28);
            menuStrip1.TabIndex = 9;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { errorLogToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // errorLogToolStripMenuItem
            // 
            errorLogToolStripMenuItem.Name = "errorLogToolStripMenuItem";
            errorLogToolStripMenuItem.Size = new Size(224, 26);
            errorLogToolStripMenuItem.Text = "Error Log";
            errorLogToolStripMenuItem.Click += errorLogToolStripMenuItem_Click;
            // 
            // FrmEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1322, 719);
            Controls.Add(btnRemoveScene);
            Controls.Add(btnAddScene);
            Controls.Add(btnSceneSource);
            Controls.Add(label2);
            Controls.Add(ndGameWidth);
            Controls.Add(ndGameHeight);
            Controls.Add(btnRun);
            Controls.Add(lstScenes);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FrmEditor";
            Text = "Cross Engine";
            FormClosing += FrmEditor_FormClosing;
            ((System.ComponentModel.ISupportInitialize)ndGameHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)ndGameWidth).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListBox lstScenes;
        private Button btnRun;
        private NumericUpDown ndGameHeight;
        private NumericUpDown ndGameWidth;
        private Label label2;
        private Button btnSceneSource;
        private Button btnAddScene;
        private Button btnRemoveScene;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem errorLogToolStripMenuItem;
    }
}