using System;
using System.Windows;
using Icicle.Views;

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

        public WPFViewEngine()
        {
            _window = new Window();
            _renderer = new WPFViewRenderer(this, _window);

            _app = new App(() => OnAppStarted());
        }

        protected override void Initialize()
        {
            _app.Run();
        }

        private void OnAppStarted()
        {
            OnStarted();
            _window.Show();
        }

        protected override void UpdateView(View view)
        {
            _renderer.Render(view);
        }

        internal void OnButtonClickInternal(Button button)
        {
            OnButtonClick(button);
        }
    }
}
