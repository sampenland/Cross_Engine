using CrossEngine.Engine;
using ScintillaNET;

namespace Cross_Engine.Designer
{
    public partial class FrmSourceEditor : Form
    {
        List<string> Files;
        Scene? EditingScene;

        public FrmSourceEditor()
        {
            InitializeComponent();
            Files = new List<string>();
            SetupCodeEditor();
        }

        private void SetupCodeEditor()
        {
            // Extracted from the Lua codeEditor lexer and SciTE .properties file

            var alphaChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var numericChars = "0123456789";
            var accentedChars = "ŠšŒœŸÿÀàÁáÂâÃãÄäÅåÆæÇçÈèÉéÊêËëÌìÍíÎîÏïÐðÑñÒòÓóÔôÕõÖØøÙùÚúÛûÜüÝýÞþßö";

            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            codeEditor.StyleResetDefault();
            codeEditor.Styles[Style.Default].Font = "Consolas";
            codeEditor.Styles[Style.Default].Size = 16;
            codeEditor.StyleClearAll();

            // Configure the Lua lexer styles
            codeEditor.Styles[Style.Lua.Default].ForeColor = Color.Black;
            codeEditor.Styles[Style.Lua.Comment].ForeColor = Color.Green;
            codeEditor.Styles[Style.Lua.CommentLine].ForeColor = Color.Green;
            codeEditor.Styles[Style.Lua.Number].ForeColor = Color.Olive;
            codeEditor.Styles[Style.Lua.Word].ForeColor = Color.Blue;
            codeEditor.Styles[Style.Lua.Word2].ForeColor = Color.BlueViolet;
            codeEditor.Styles[Style.Lua.Word3].ForeColor = Color.DarkSlateBlue;
            codeEditor.Styles[Style.Lua.Word4].ForeColor = Color.DarkSlateBlue;
            codeEditor.Styles[Style.Lua.Word5].ForeColor = Color.Brown;
            codeEditor.Styles[Style.Lua.String].ForeColor = Color.Red;
            codeEditor.Styles[Style.Lua.Character].ForeColor = Color.Red;
            codeEditor.Styles[Style.Lua.LiteralString].ForeColor = Color.Red;
            codeEditor.Styles[Style.Lua.StringEol].BackColor = Color.Pink;
            codeEditor.Styles[Style.Lua.Operator].ForeColor = Color.Purple;
            codeEditor.Styles[Style.Lua.Preprocessor].ForeColor = Color.Maroon;
            codeEditor.LexerName = "lua";
            codeEditor.WordChars = alphaChars + numericChars + accentedChars;

            // Console.WriteLine(codeEditor.DescribeKeywordSets());

            // Keywords
            codeEditor.SetKeywords(0, "and break do else elseif end for function if in local nil not or repeat return then until while" + " false true" + " goto");
            // Basic Functions
            codeEditor.SetKeywords(1, "assert collectgarbage dofile error _G getmetatable ipairs loadfile next pairs pcall print rawequal rawget rawset setmetatable tonumber tostring type _VERSION xpcall string table math coroutine io os debug" + " getfenv gcinfo load loadlib loadstring require select setfenv unpack _LOADED LUA_PATH _REQUIREDNAME package rawlen package bit32 utf8 _ENV");
            // String Manipulation & Mathematical
            codeEditor.SetKeywords(2, "string.byte string.char string.dump string.find string.format string.gsub string.len string.lower string.rep string.sub string.upper table.concat table.insert table.remove table.sort math.abs math.acos math.asin math.atan math.atan2 math.ceil math.cos math.deg math.exp math.floor math.frexp math.ldexp math.log math.max math.min math.pi math.pow math.rad math.random math.randomseed math.sin math.sqrt math.tan" + " string.gfind string.gmatch string.match string.reverse string.pack string.packsize string.unpack table.foreach table.foreachi table.getn table.setn table.maxn table.pack table.unpack table.move math.cosh math.fmod math.huge math.log10 math.modf math.mod math.sinh math.tanh math.maxinteger math.mininteger math.tointeger math.type math.ult" + " bit32.arshift bit32.band bit32.bnot bit32.bor bit32.btest bit32.bxor bit32.extract bit32.replace bit32.lrotate bit32.lshift bit32.rrotate bit32.rshift" + " utf8.char utf8.charpattern utf8.codes utf8.codepoint utf8.len utf8.offset");
            // Input and Output Facilities and System Facilities
            codeEditor.SetKeywords(3, "coroutine.create coroutine.resume coroutine.status coroutine.wrap coroutine.yield io.close io.flush io.input io.lines io.open io.output io.read io.tmpfile io.type io.write io.stdin io.stdout io.stderr os.clock os.date os.difftime os.execute os.exit os.getenv os.remove os.rename os.setlocale os.time os.tmpname" + " coroutine.isyieldable coroutine.running io.popen module package.loaders package.seeall package.config package.searchers package.searchpath" + " require package.cpath package.loaded package.loadlib package.path package.preload");

            // Input Cross API ---------------
            codeEditor.SetKeywords(4, "log worldtext ");
            // -------------------------------

            // Instruct the lexer to calculate folding
            codeEditor.SetProperty("fold", "1");
            codeEditor.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            codeEditor.Margins[2].Type = MarginType.Symbol;
            codeEditor.Margins[2].Mask = Marker.MaskFolders;
            codeEditor.Margins[2].Sensitive = true;
            codeEditor.Margins[2].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                codeEditor.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                codeEditor.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            // Configure folding markers with respective symbols
            codeEditor.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            codeEditor.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            codeEditor.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            codeEditor.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            codeEditor.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            codeEditor.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            codeEditor.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            codeEditor.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
        }

        private void Save()
        {
            if (lstFunctions.SelectedIndex != -1)
            {
                string file = Files[lstFunctions.SelectedIndex];
                File.WriteAllText(file, codeEditor.Text);
            }

        }

        public void LoadScene(Scene scene)
        {
            EditingScene = scene;

            string dir = scene.LUA_DIR;
            string[] files = Directory.GetFiles(dir);

            foreach (string file in files)
            {
                Files.Add(file);
                lstFunctions.Items.Add(file.Substring(file.LastIndexOf("\\") + 1));
            }

            if (files.Length > 0)
            {
                codeEditor.Text = File.ReadAllText(files[0]);
            }

        }

        private void lstFunctions_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lstFunctions.SelectedIndex != -1)
            {
                string file = Files[lstFunctions.SelectedIndex];
                codeEditor.Text = File.ReadAllText(file);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void aPIToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string text = "";

            if (File.Exists(Environment.CurrentDirectory + "\\Lua\\" + "Lib.lua"))
            {
                string lib = File.ReadAllText(Environment.CurrentDirectory + "\\Lua\\" + "Lib.lua");
                text = lib;
            }
            else
            {
                text = "Could not find library.";
            }

            FormsCommon.OpenText(text);
        }
    }
}
