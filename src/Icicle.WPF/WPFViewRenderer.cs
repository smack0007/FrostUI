using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Icicle.Controls;
using WPFButton = System.Windows.Controls.Button;

namespace Icicle.WPF
{
    public class WPFViewRenderer
    {
        private Window _window;

        public WPFViewRenderer(Window window)
        {
            _window = window;
        }

        public void Render(View view)
        {
            switch (view)
            {
                case Button button:
                    _window.Content = new WPFButton()
                    {
                        Content = button.Text
                    };
                    break;
            }
        }
    }
}
