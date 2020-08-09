using System;
using System.Windows;
using FrostUI.Controls;

using WPFButton = System.Windows.Controls.Button;
using WPFGrid = System.Windows.Controls.Grid;
using WPFTextBlock = System.Windows.Controls.TextBlock;
using WPFView = System.Windows.FrameworkElement;

namespace FrostUI.WPF
{
    public partial class WPFViewEngine
    {
        private RoutedEventHandler? _buttonClickHandler;

        private void InitializeRenderWPFView()
        {
            _buttonClickHandler = new RoutedEventHandler(OnButtonClick);
        }

        public WPFView RenderWPFView(View view, object? renderedView)
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

                case StackLayout stackLayout:
                    {
                        var wpfGrid = renderedView as WPFGrid;

                        if (wpfGrid == null)
                            wpfGrid = new WPFGrid();

                        wpfGrid.Tag = view;
                        wpfGrid.ColumnDefinitions.Clear();
                        wpfGrid.RowDefinitions.Clear();
                        wpfGrid.Children.Clear();

                        var gridLength = new System.Windows.GridLength(100.0 / stackLayout.Children.Count, GridUnitType.Star);

                        for (int i = 0; i < stackLayout.Children.Count; i++)
                        {
                            var child = stackLayout.Children[i];
                            var wpfChild = RenderWPFView(child, null);

                            if (stackLayout.Orientation == Orientation.Horizontal)
                            {
                                WPFGrid.SetColumn(wpfChild, i);
                                WPFGrid.SetRow(wpfChild, 0);
                                
                                wpfGrid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition() { Width = gridLength });
                            }
                            else
                            {
                                WPFGrid.SetRow(wpfChild, i);
                                WPFGrid.SetColumn(wpfChild, 0);

                                wpfGrid.RowDefinitions.Add(new System.Windows.Controls.RowDefinition() { Height = gridLength });
                            }
                            
                            wpfGrid.Children.Add(wpfChild);                                
                        }

                        return wpfGrid;
                    }

                case TextBlock textBlock:
                    {
                        var wpfLabel = renderedView as WPFTextBlock;

                        if (wpfLabel == null)
                            wpfLabel = new WPFTextBlock();

                        wpfLabel.Tag = view;
                        wpfLabel.Text = textBlock.Value;

                        return wpfLabel;
                    }
            }

            throw new NotImplementedException($"View type {view.GetType()} not implemented in {nameof(WPFViewEngine)}.{nameof(RenderWPFView)}.");
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var wpfButton = (WPFButton)sender;
            var button = (Button)wpfButton.Tag;

            OnButtonClick(button);
        }
    }
}
