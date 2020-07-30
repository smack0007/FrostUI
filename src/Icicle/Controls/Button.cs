using System;
using System.Collections.Generic;
using System.Text;

namespace Icicle.Controls
{
    public class Button : View
    {
        public string Text { get; }

        public Button(string text)
        {
            Text = text;
        }
    }
}
