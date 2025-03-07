using Cross_Engine.Engine;
using Microsoft.VisualBasic;

namespace Cross_Engine
{
    public partial class FrmIntro : Form
    {
        public FrmIntro()
        {
            InitializeComponent();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            if (fvi != null)
            {
                string ?version = fvi.FileVersion;
                Text = "Cross Engine :: " + version;
            }
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            if (Program.Editor != null)
            {
                Program.ProjectName = Interaction.InputBox("Please enter your project name.", "New Project");
                if (Program.ProjectName == null || Program.ProjectName == "")
                {
                    MessageBox.Show("Name empty or null.");
                    return;
                }
                Program.ProjectName = Common.RemoveSpecialCharacters(Program.ProjectName);
                Program.Editor.Show();
                Program.Editor.Start();
            }

            Hide();
        }

        private void FrmIntro_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Exit Cross Engine?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Environment.Exit(0);
                return;
            }

            e.Cancel = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Cross Engine?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Environment.Exit(0);
                return;
            }
        }
    }
}
