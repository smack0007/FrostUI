using System;
using Icicle;
using Icicle.Views;
using Icicle.WPF;

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
                                "Click me!",
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
