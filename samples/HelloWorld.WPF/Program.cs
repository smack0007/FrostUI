﻿using System;
using FrostUI;
using FrostUI.Controls;
using FrostUI.WPF;

namespace HelloWorld.WPF
{
    public static class Program
    {
        class HelloWorldView : ContentView
        {
            public MutableState<int> Clicks { get; } = 0;

            public HelloWorldView()
            {
                Content = new StackLayout(
                    Orientation.Horizontal,
                    new View[] {
                        new StackLayout(
                            Orientation.Vertical,
                            new View[] {
                                new TextBlock(Clicks.Bind(x => $"Clicks: {x}")),
                                new TextBlock(Clicks.Bind(x => $"Clicks: {x}")),
                                new TextBlock(Clicks.Bind(x => $"Clicks: {x}")),
                            }
                        ),
                        new StackLayout(
                            Orientation.Vertical,
                            new View[] {
                                new TextBlock(Clicks.Bind(x => $"Clicks: {x}")),
                                new Button(
                                    "Click me!",
                                    onClick: () =>
                                    {
                                        Clicks.Value++;
                                    }
                                ),
                                new TextBlock(Clicks.Bind(x => $"Clicks: {x}")),
                            }
                        ),
                        new StackLayout(
                            Orientation.Vertical,
                            new View[] {
                                new TextBlock(Clicks.Bind(x => $"Clicks: {x}")),
                                new TextBlock(Clicks.Bind(x => $"Clicks: {x}")),
                                new TextBlock(Clicks.Bind(x => $"Clicks: {x}")),
                            }
                        ),
                    }
                );
            }
        }

        [STAThread]
        public static void Main()
        {
            new WPFViewEngine().Run(new HelloWorldView());
        }
    }
}
