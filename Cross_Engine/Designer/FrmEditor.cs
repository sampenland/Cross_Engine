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

            Program.CrossGame = new Game((uint)ndGameWidth.Value, (uint)ndGameHeight.Value, "Game", SFML.Window.Keyboard.Key.Escape, true);

            if (loadFile == "none")
            {
                Scene mainScene = new Scene(Program.CrossGame, "Main");

                ConsoleDisplay crossDisplay = new ConsoleDisplay(Program.CrossGame, "CrossGameCon", 0, (int)ndGameHeight.Value - 300, (int)ndGameWidth.Value, 300);
                crossDisplay.LinkInputHandler(Program.CrossGame.GetInputHandler());
                crossDisplay.visible = true;
                crossDisplay.Print("Default Main Scene Started");
                mainScene.LinkConsole(crossDisplay);

                Program.CrossGame.AddScene(mainScene);
                
            }
            else
            {
                LoadProject(loadFile);
            }

            DisplayUIChanges();
        }

        private void InitWorldEditor()
        {
            Size worldMakerSize = new Size(900, 500);
            Program.WorldMaker = new Game((uint)worldMakerSize.Width, (uint)worldMakerSize.Height, "", SFML.Window.Keyboard.Key.Backspace, true);
            worldEditorSurface = new DrawingSurface();
            worldEditorSurface.Location = new Point(170, 50);
            worldEditorSurface.Size = worldMakerSize;
            Controls.Add(worldEditorSurface);
            Program.WorldMaker.RebuildWindow((uint)worldMakerSize.Width, (uint)worldMakerSize.Height, "", SFML.Window.Keyboard.Key.Backspace, worldEditorSurface.Handle);
            Scene worldMakerScene = new Scene(Program.WorldMaker, "WorldMaker");

            ConsoleDisplay worldMakerConsole = new ConsoleDisplay(Program.WorldMaker, "WorldMakerCon", 0, worldMakerSize.Height - 200, worldMakerSize.Width, 200);
            worldMakerConsole.LinkInputHandler(Program.WorldMaker.GetInputHandler());
            worldMakerConsole.visible = true;
            worldMakerScene.LinkConsole(worldMakerConsole);
            worldMakerConsole.Print("World Editor Started");

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
                if (Program.Editor != null) Program.Editor.Hide();
                if (Program.WorldMaker != null) Program.WorldMaker.Paused = true;

                Program.CrossForm = new Form();
                Program.CrossForm.Text = "Cross Engine :: Game";
                Program.CrossForm.StartPosition = FormStartPosition.CenterScreen;
                Program.CrossForm.FormClosing += EndCross;
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
            if (Program.Editor != null) Program.Editor.Show();
            if (Program.WorldMaker != null) Program.WorldMaker.Paused = false;

            if (Program.CrossGame != null)
            {
                Program.CrossGame.Stop();
                if (Program.CrossForm != null)
                {
                    Program.CrossForm.Dispose();
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
