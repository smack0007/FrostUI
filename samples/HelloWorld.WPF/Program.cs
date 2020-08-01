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
                        children: new View[] {
                            new Label(
                                text: $"Clicks: {clicks}"
                            ),
                            new Label(
                                text: $"Clicks: {clicks}"
                            ),
                            new Label(
                                text: $"Clicks: {clicks}"
                            )
                        }
                    ),
                    new VStack(
                        children: new View[] {
                            new Label(
                                text: $"Clicks: {clicks}"
                            ),
                            new Button(
                                text: $"Clicks: {clicks}",
                                onClick: () =>
                                {
                                    clicks++;
                                }
                            ),
                            new Label(
                                text: $"Clicks: {clicks}"
                            )
                        }
                    ),
                    new VStack(
                        children: new View[] {
                            new Label(
                                text: $"Clicks: {clicks}"
                            ),
                            new Label(
                                text: $"Clicks: {clicks}"
                            ),
                            new Label(
                                text: $"Clicks: {clicks}"
                            )
                        }
                    )
                }
            );

            new WPFViewEngine().Run(render);
        }
    }
}
