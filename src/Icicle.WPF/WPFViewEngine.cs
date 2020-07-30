using System.Windows;

namespace Icicle.WPF
{
    public class WPFViewEngine : ViewEngine
    {
        class ViewEngineApplication : Application
        {
            protected override void OnStartup(StartupEventArgs e)
            {
                base.OnStartup(e);
                var window = new Window();
                window.Show();
            }
        }

        public override void Run()
        {
            var app = new ViewEngineApplication();
            app.Run();
        }
    }
}
