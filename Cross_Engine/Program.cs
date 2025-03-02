using Cross_Engine.Designer;
using CrossEngine.Engine;
using CrossEngine.Engine.WorldMaker;

namespace Cross_Engine
{
    internal static class Program
    {
        // Cross Engine
        public static Game ?CrossGame;
        public static Form ?CrossForm;
        public static DrawingSurface ?CrossSurface;
        public static Game? WorldMaker;

        // ------------------------------

        public static List<Form> ?Forms;

        public static FrmIntro? Intro;
        public static FrmEditor ?Editor;

        // Colors
        public static Color C_Background = Color.MediumAquamarine;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            InitWindows();

            if (Intro != null) Application.Run(Intro);
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
        }
    }
}