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
            var clicks = new State<int>();

            Func<View> render = () => new Button(
                text: $"Clicks: {clicks.Value}",
                onClick: () =>
                {
                    clicks.Value++;
                });

            new WPFViewEngine().Run(render, clicks);
        }
    }
}
