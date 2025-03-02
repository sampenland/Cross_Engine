namespace Cross_Engine
{
    public partial class FrmIntro : Form
    {
        public FrmIntro()
        {
            InitializeComponent();
            Text = "Cross Engine :: " + Application.ProductVersion.ToString();
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            if (Program.Editor != null)
            {
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
