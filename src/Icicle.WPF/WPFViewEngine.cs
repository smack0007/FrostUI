using System;
using System.Windows;

namespace Icicle.WPF
{
    public class WPFViewEngine : ViewEngine
    {
        class App : Application
        {
            private Action _onStartup;

            public App(Action onStartup)
            {
                _onStartup = onStartup;
            }

            protected override void OnStartup(StartupEventArgs e)
            {
                base.OnStartup(e);
                _onStartup();
            }
        }

        private App _app;
        private Window _window;
        private WPFViewRenderer _renderer;

        protected override void Initialize()
        {
            _window = new Window();

            _renderer = new WPFViewRenderer(_window);

            _app = new App(() =>
            {
                OnStarted();
                _window.Show();
            });
            _app.Run();
        }

        protected override void UpdateView(View view)
        {
            _renderer.Render(view);
        }
    }
}
