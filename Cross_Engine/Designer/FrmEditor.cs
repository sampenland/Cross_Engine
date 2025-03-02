using CrossEngine.Engine;

namespace Cross_Engine.Designer
{
    public partial class FrmEditor : Form
    {
        public DrawingSurface ?worldEditorSurface;

        public FrmEditor()
        {
            InitializeComponent();
            Text = "Cross Engine :: " + Application.ProductVersion.ToString();
        }

        public void Start(string loadFile = "none")
        {
            InitWorldEditor();

            DisplayUIChanges();

            Program.CrossGame = new Game((uint)ndGameWidth.Value, (uint)ndGameHeight.Value, "Game", SFML.Window.Keyboard.Key.Escape, true);

            if (loadFile == "none")
            {
                Program.CrossGame.AddScene(new Scene(Program.CrossGame, "Main"));
            }
            else
            {
                LoadProject(loadFile);
            }
        }

        private void InitWorldEditor()
        {
            Size worldMakerSize = new Size(100, 100);
            Program.WorldMaker = new Game((uint)worldMakerSize.Width, (uint)worldMakerSize.Height, "Test", SFML.Window.Keyboard.Key.Backspace, true);
            worldEditorSurface = new DrawingSurface();
            worldEditorSurface.Location = new Point(170, 50);
            worldEditorSurface.Size = worldMakerSize;
            Controls.Add(worldEditorSurface);
            Program.WorldMaker.RebuildWindow((uint)worldMakerSize.Width, (uint)worldMakerSize.Height, "Test", SFML.Window.Keyboard.Key.Backspace, worldEditorSurface.Handle);
            Scene worldMakerScene = new Scene(Program.WorldMaker, "WorldMaker");

            ConsoleDisplay worldMakerConsole = new ConsoleDisplay("WorldMakerCon", 0, 0, worldMakerSize.Width, worldMakerSize.Height - 20);
            worldMakerConsole.LinkInputHandler(Program.WorldMaker.GetInputHandler());
            worldMakerConsole.visible = true;
            worldMakerScene.LinkConsole(worldMakerConsole);

            Program.WorldMaker.AddPassiveScene(worldMakerScene);
            Program.WorldMaker.Start();
        }

        private void LoadProject(string loadFile)
        {

        }

        private void DisplayUIChanges()
        {
            lstScenes.Items.Clear();

            if (Program.CrossGame != null && Program.CrossGame.GetScenes() != null)
            {
                foreach (Scene scene in Program.CrossGame.GetScenes())
                {
                    lstScenes.Items.Add(scene.Name);
                }
            }
        }

        private void FrmEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to close?", "Exit?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.Reset();
            }

            e.Cancel = true;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            StartCross();
        }

        private void StartCross()
        {
            if (Program.CrossGame != null)
            {
                Program.CrossForm = new Form();
                Program.CrossForm.FormClosing += EndCross; ;
                Program.CrossForm.Width = (int)ndGameWidth.Value;
                Program.CrossForm.Height = (int)ndGameHeight.Value;
                Program.CrossForm.Show();
                Program.CrossSurface = new DrawingSurface();
                Program.CrossSurface.Location = new Point(0, 0);
                Program.CrossSurface.Size = new Size(Program.CrossForm.Width, Program.CrossForm.Height);
                Program.CrossForm.Controls.Add(Program.CrossSurface);
                Program.CrossGame.RebuildWindow((uint)ndGameWidth.Value, (uint)ndGameHeight.Value, "Cross Game", SFML.Window.Keyboard.Key.Escape, Program.CrossSurface.Handle);
                Program.CrossGame.Start(Program.CrossGame.GetScene("Main"));
            }
        }

        private void EndCross(object? sender, FormClosingEventArgs e)
        {
            if (Program.CrossGame != null)
            {
                Program.CrossGame.Stop();
                if (Program.CrossForm != null)
                {
                    Program.CrossForm.Close();
                }
            }
        }
    }
    public class DrawingSurface : System.Windows.Forms.Control
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
        }
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
        }
    }

}
