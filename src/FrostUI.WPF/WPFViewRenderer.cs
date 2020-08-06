using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FrostUI.Views;

using WPFButton = System.Windows.Controls.Button;
using WPFGrid = System.Windows.Controls.Grid;
using WPFTextBlock = System.Windows.Controls.TextBlock;
using WPFView = System.Windows.FrameworkElement;
using WPFWindow = System.Windows.Window;

namespace FrostUI.WPF
{
    public class WPFViewRenderer
    {
        private readonly WPFViewEngine _engine;
        private readonly WPFWindow _window;

        private RoutedEventHandler _buttonClickHandler;

        public WPFViewRenderer(WPFViewEngine engine, WPFWindow window)
        {
            _engine = engine;
            _window = window;

            _buttonClickHandler = new RoutedEventHandler(OnButtonClick);
        }

        public WPFView Render(View view, object? renderedView)
        {
            switch (view)
            {
                case Button button:
                    {
                        var wpfButton = renderedView as WPFButton;

                        if (wpfButton == null)
                        {
                            wpfButton = new WPFButton();
                            wpfButton.AddHandler(WPFButton.ClickEvent, _buttonClickHandler);
                        }

                        wpfButton.Tag = view;
                        wpfButton.Content = button.Label;

                        return wpfButton;
                    }

                case HStack hStack:
                    {
                        var wpfGrid = renderedView as WPFGrid;

                        if (wpfGrid == null)
                            wpfGrid = new WPFGrid();

                        wpfGrid.Tag = view;
                        wpfGrid.ColumnDefinitions.Clear();
                        wpfGrid.RowDefinitions.Clear();
                        wpfGrid.Children.Clear();

                        var gridLength = new System.Windows.GridLength(100.0 / hStack.Children.Count, GridUnitType.Star);

                        for (int i = 0; i < hStack.Children.Count; i++)
                        {
                            var child = hStack.Children[i];
                            var wpfChild = Render(child, null);

                            WPFGrid.SetColumn(wpfChild, i);
                            WPFGrid.SetRow(wpfChild, 0);

                            wpfGrid.Children.Add(wpfChild);
                            wpfGrid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition() { Width = gridLength });
                        }

                        return wpfGrid;
                    }

                case Text text:
                    {
                        var wpfLabel = renderedView as WPFTextBlock;

                        if (wpfLabel == null)
                            wpfLabel = new WPFTextBlock();

                        wpfLabel.Tag = view;
                        wpfLabel.Text = text.Value.GetValue();

                        return wpfLabel;
                    }

                case VStack vStack:
                    {
                        var wpfGrid = renderedView as WPFGrid;

                        if (wpfGrid == null)
                            wpfGrid = new WPFGrid();

                        wpfGrid.Tag = view;
                        wpfGrid.ColumnDefinitions.Clear();
                        wpfGrid.RowDefinitions.Clear();
                        wpfGrid.Children.Clear();

                        var gridLength = new System.Windows.GridLength(100.0 / vStack.Children.Count, GridUnitType.Star);

                        for (int i = 0; i < vStack.Children.Count; i++)
                        {
                            var child = vStack.Children[i];
                            var wpfChild = Render(child, null);

                            WPFGrid.SetColumn(wpfChild, 0);
                            WPFGrid.SetRow(wpfChild, i);

                            wpfGrid.Children.Add(wpfChild);
                            wpfGrid.RowDefinitions.Add(new System.Windows.Controls.RowDefinition() { Height = gridLength });
                        }

                        return wpfGrid;
                    }
            }

            throw new NotImplementedException($"View type {view.GetType()} not implemented in {nameof(WPFViewRenderer)}.{nameof(Render)}.");
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var wpfButton = (WPFButton)sender;
            var button = (Button)wpfButton.Tag;

            _engine.OnButtonClickInternal(button);
        }
    }
}
