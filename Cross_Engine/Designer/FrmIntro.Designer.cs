namespace Cross_Engine
{
    partial class FrmIntro
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIntro));
            btnNewProject = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // btnNewProject
            // 
            btnNewProject.Location = new Point(574, 255);
            btnNewProject.Name = "btnNewProject";
            btnNewProject.Size = new Size(134, 29);
            btnNewProject.TabIndex = 0;
            btnNewProject.Text = "New Project";
            btnNewProject.UseVisualStyleBackColor = true;
            btnNewProject.Click += btnNewProject_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(574, 528);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(134, 29);
            btnExit.TabIndex = 1;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // FrmIntro
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 569);
            Controls.Add(btnExit);
            Controls.Add(btnNewProject);
            Name = "FrmIntro";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cross Engine";
            FormClosing += FrmIntro_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private Button btnNewProject;
        private Button btnExit;
    }
}
