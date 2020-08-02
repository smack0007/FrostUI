using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Icicle.Views;

using WPFButton = System.Windows.Controls.Button;
using WPFGrid = System.Windows.Controls.Grid;
using WPFTextBlock = System.Windows.Controls.TextBlock;
using WPFView = System.Windows.FrameworkElement;
using WPFWindow = System.Windows.Window;

namespace Icicle.WPF
{
    public class WPFViewRenderer
    {
        private readonly WPFViewEngine _engine;
        private readonly WPFWindow _window;
        
        private readonly List<WPFView> _wpfViews = new List<WPFView>();

        private RoutedEventHandler _buttonClickHandler;

        public WPFViewRenderer(WPFViewEngine engine, WPFWindow window)
        {
            _engine = engine;
            _window = window;

            _buttonClickHandler = new RoutedEventHandler(OnButtonClick);
        }

        public void Render(View view)
        {
            foreach (var wpfView in _wpfViews)
            {
                wpfView.Tag = null;
            }

            _window.Content = RenderWPFView(view);

            foreach (var unusedWPFView in _wpfViews.Where(x => x.Tag == null))
            {
                switch (unusedWPFView)
                {
                    case WPFButton wpfButton:
                        wpfButton.RemoveHandler(WPFButton.ClickEvent, _buttonClickHandler);
                        break;
                }
            }

            _wpfViews.RemoveAll(x => x.Tag == null);
        }

        private WPFView RenderWPFView(View view)
        {
            bool isNew;

            switch (view)
            {
                case Button button:
                    {
                        var wpfButton = GetControl<WPFButton>(button, out isNew);
                        wpfButton.Content = RenderWPFView(button.Label);

                        if (isNew)
                        {
                            wpfButton.AddHandler(WPFButton.ClickEvent, _buttonClickHandler);
                        }

                        return wpfButton;
                    }

                case HStack hStack:
                    {
                        var wpfGrid = GetControl<WPFGrid>(hStack, out isNew);

                        wpfGrid.ColumnDefinitions.Clear();
                        wpfGrid.RowDefinitions.Clear();
                        wpfGrid.Children.Clear();

                        var gridLength = new System.Windows.GridLength(100.0 / hStack.Children.Count, GridUnitType.Star);

                        for (int i = 0; i < hStack.Children.Count; i++)
                        {
                            var child = hStack.Children[i];
                            var wpfChild = RenderWPFView(child);

                            WPFGrid.SetColumn(wpfChild, i);
                            WPFGrid.SetRow(wpfChild, 0);

                            wpfGrid.Children.Add(wpfChild);
                            wpfGrid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition() { Width = gridLength });
                        }

                        return wpfGrid;
                    }

                case Text text:
                    {
                        var wpfLabel = GetControl<WPFTextBlock>(text, out isNew);
                        wpfLabel.Text = text.Value;

                        return wpfLabel;
                    }

                case VStack vStack:
                    {
                        var wpfGrid = GetControl<WPFGrid>(vStack, out isNew);

                        wpfGrid.ColumnDefinitions.Clear();
                        wpfGrid.RowDefinitions.Clear();
                        wpfGrid.Children.Clear();

                        var gridLength = new System.Windows.GridLength(100.0 / vStack.Children.Count, GridUnitType.Star);

                        for (int i = 0; i < vStack.Children.Count; i++)
                        {
                            var child = vStack.Children[i];
                            var wpfChild = RenderWPFView(child);

                            WPFGrid.SetColumn(wpfChild, 0);
                            WPFGrid.SetRow(wpfChild, i);

                            wpfGrid.Children.Add(wpfChild);
                            wpfGrid.RowDefinitions.Add(new System.Windows.Controls.RowDefinition() { Height = gridLength });
                        }

                        return wpfGrid;
                    }
            }

            throw new NotImplementedException($"View type {view.GetType()} not implemented in {nameof(WPFViewRenderer)}.{nameof(RenderWPFView)}.");
        }

        private T GetControl<T>(View view, out bool isNew)
            where T : WPFView, new()
        {
            isNew = false;
            T? result = null;
            T? resultWithoutView = null;

            foreach (var control in _wpfViews.Where(x => x.GetType() == typeof(T)))
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
                    _wpfViews.Add(result);
                }
            }

            result.Tag = view;
            return result;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var wpfButton = (WPFButton)sender;
            var button = (Button)wpfButton.Tag;

            _engine.OnButtonClickInternal(button);
        }
    }
}
