using Cross_Engine.Designer;
using Cross_Engine.Examples;
using CrossEngine.Engine;

namespace Cross_Engine
{
    internal static class Program
    {
        // Cross Engine
        public static Game ?CrossGame;
        public static Form ?CrossForm;
        public static DrawingSurface ?CrossSurface;

        public static Game? SandboxGame;
        public static Form? SandboxForm;
        public static DrawingSurface? SandboxSurface;

        public static Game? WorldMaker;
        public static string ProjectName = "";

        // Lua 
        public static string[] LuaSceneFiles = ["init.lua", "create.lua", "update.lua"];

        // ------------------------------

        public static List<Form> ?Forms;

        public static FrmIntro? Intro;
        public static FrmEditor ?Editor;

        // Colors
        public static Color C_Background = Color.LightGray;

        [STAThread]

        static void Main()
        {
            ApplicationConfiguration.Initialize();

#if SANDBOX
            RunSandbox();
#else

            InitWindows();

            if (Intro != null) Application.Run(Intro);

#endif
        }

        static void RunSandbox()
        {

            SandboxGame = new Game(1280, 720, "Sandbox", SFML.Window.Keyboard.Key.Escape, true, true);
            SandboxForm = new Form();
            SandboxForm.Text = "Cross Engine :: Game";
            SandboxForm.StartPosition = FormStartPosition.CenterScreen;
            SandboxForm.Width = 1280;
            SandboxForm.Height = 720;
            SandboxForm.Show();
            SandboxSurface = new DrawingSurface();
            SandboxSurface.Location = new Point(0, 0);
            SandboxSurface.Size = new Size(SandboxForm.Width, SandboxForm.Height);
            SandboxForm.Controls.Add(SandboxSurface);
            SandboxGame.RebuildWindow(1280, 720, "Sandbox", SFML.Window.Keyboard.Key.Escape, SandboxSurface.Handle);

            DrawableExamples drawableExamples = new DrawableExamples(SandboxGame, "DrawablesExample");
            SandboxGame.Start(drawableExamples);

            Application.Run(SandboxForm);
        }

        public static void Reset()
        {
            if (Forms == null) return;

            foreach(Form form in Forms)
            {
                form.Hide();
            }

            InitWindows();
            if (Intro != null) Intro.Show();
        }

        static void InitWindows()
        {
            Forms = new List<Form>();

            Intro = new FrmIntro();
            Forms.Add(Intro);

            Editor = new FrmEditor();
            Forms.Add(Editor);

            foreach(Form form in Forms)
            {
                form.BackColor = C_Background;
                form.StartPosition = FormStartPosition.CenterScreen;
            }

            ProjectName = "";
        }
    }
}