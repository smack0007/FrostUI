using System;
using Icicle;
using Icicle.Controls;
using Icicle.WPF;

namespace HelloWorld.WPF
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Func<View> render = () => new Button("Hello World!");

            var viewEngine = new WPFViewEngine();
            viewEngine.Run(render);
        }
    }
}
