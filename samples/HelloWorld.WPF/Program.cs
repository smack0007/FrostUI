using System;
using FrostUI;
using FrostUI.Views;
using FrostUI.WPF;

namespace HelloWorld.WPF
{
    public static class Program
    {
        class HelloWorldView : View
        {
            public MutableState<int> Clicks { get; } = 0;

            public HelloWorldView()
            {
                Content = new HStack(
                    children: new View[] {
                        new VStack(
                            new View[] {
                                new Text(Clicks.Bind(() => $"Clicks: {Clicks}")),
                                new Text(Clicks.Bind(() => $"Clicks: {Clicks}")),
                                new Text(Clicks.Bind(() => $"Clicks: {Clicks}")),
                            }
                        ),
                        new VStack(
                            new View[] {
                                new Text(Clicks.Bind(() => $"Clicks: {Clicks}")),
                                new Button(
                                    "Click me!",
                                    onClick: () =>
                                    {
                                        Clicks.Value++;
                                    }
                                ),
                                new Text(Clicks.Bind(() => $"Clicks: {Clicks}")),
                            }
                        ),
                        new VStack(
                            new View[] {
                                new Text(Clicks.Bind(() => $"Clicks: {Clicks}")),
                                new Text(Clicks.Bind(() => $"Clicks: {Clicks}")),
                                new Text(Clicks.Bind(() => $"Clicks: {Clicks}")),
                            }
                        )
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
