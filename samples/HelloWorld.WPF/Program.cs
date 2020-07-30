using System;
using Icicle.WPF;

namespace HelloWorld.WPF
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var viewEngine = new WPFViewEngine();
            viewEngine.Run();
        }
    }
}
