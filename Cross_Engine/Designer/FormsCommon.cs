using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_Engine.Designer
{
    class FormsCommon
    {
        public static void OpenText(string text, int width = 1024, int height = 720)
        {
            Form textForm = new Form();
            textForm.Width = width;
            textForm.Height = height;
            RichTextBox tb = new RichTextBox();
            tb.ReadOnly = true;

            tb.Text = text;

            tb.Width = width;
            tb.Height = height;

            tb.Parent = textForm;
            tb.Dock = DockStyle.Fill;

            textForm.Show();
        }
    }
}
