using System;
using FrostUI;
using FrostUI.Views;
using FrostUI.WPF;

namespace HelloWorld.WPF
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var clicks = 0;

            Func<View> render = () => new HStack(
                children: new View[] {
                    new VStack(
                        new View[] {
                            new Text($"Clicks: {clicks}"),
                            new Text($"Clicks: {clicks}"),
                            new Text($"Clicks: {clicks}"),
                        }
                    ),
                    new VStack(
                        new View[] {
                            new Text($"Clicks: {clicks}"),
                            new Button(
                                (Text)"Click me!",
                                onClick: () =>
                                {
                                    clicks++;
                                }
                            ),
                            new Text($"Clicks: {clicks}"),
                        }
                    ),
                    new VStack(
                        new View[] {
                            new Text($"Clicks: {clicks}"),
                            new Text($"Clicks: {clicks}"),
                            new Text($"Clicks: {clicks}")
                        }
                    )
                }
            );

            new WPFViewEngine().Run(render);
        }
    }
}
