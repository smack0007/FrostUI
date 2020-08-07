using System;
using System.Windows;
using FrostUI.Views;

namespace FrostUI.WPF
{
    public partial class WPFViewEngine : ViewEngine
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

        public WPFViewEngine()
        {
            _window = new Window();
            InitializeRenderWPFView();

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

        protected override object Render(View view, object? renderedView)
        {
            var result = RenderWPFView(view == RootView ? view.Content : view, renderedView);

            if (view == RootView)
                _window.Content = result;

            return result;
        }
    }
}
