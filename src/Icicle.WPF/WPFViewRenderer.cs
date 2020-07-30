using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Icicle.Views;

using WPFControl = System.Windows.Controls.Control;
using WPFButton = System.Windows.Controls.Button;
using System.Linq;

namespace Icicle.WPF
{
    public class WPFViewRenderer
    {
        private Window _window;
        private List<WPFControl> _controls = new List<WPFControl>();

        public WPFViewRenderer(Window window)
        {
            _window = window;
        }

        public void Render(View view, View? oldView)
        {
            if (oldView != null)
            {
                foreach (var control in _controls)
                    control.Tag = null;
            }

            object? wpfContent = null;
            switch (view)
            {
                case Button button:
                    var wpfButton = GetControl<WPFButton>(button, out var isNew);
                    wpfButton.Content = button.Text;

                    if (isNew)
                    {
                        wpfButton.AddHandler(WPFButton.ClickEvent, new RoutedEventHandler(OnButtonClick));
                    }

                    wpfContent = wpfButton;
                    break;
            }

            if (oldView != null)
            {
                _controls.RemoveAll(x => x.Tag == null);
            }

            _window.Content = wpfContent;
        }

        private T GetControl<T>(View view, out bool isNew)
            where T : WPFControl, new()
        {
            isNew = false;
            T? result = null;
            T? resultWithoutView = null;

            foreach (var control in _controls.Where(x => x.GetType() == typeof(T)))
            {
                if (control.Tag == view)
                {
                    result = (T)control;
                    break;
                }
                else if (control.Tag == null && resultWithoutView == null)
                {
                    resultWithoutView = (T)control;
                }
            }

            if (result == null)
            {
                if (resultWithoutView != null)
                {
                    result = resultWithoutView;
                }
                else
                {
                    isNew = true;
                    result = new T();
                    _controls.Add(result);
                }
            }

            result.Tag = view;
            return result;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var wpfButton = (WPFButton)sender;
            var button = (Button)wpfButton.Tag;

            button.OnClick?.Invoke();
        }
    }
}
