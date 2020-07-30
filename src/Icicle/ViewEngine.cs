using System;

namespace Icicle
{
    public abstract class ViewEngine
    {
        private Func<View>? _render;

        private View? _view;

        public ViewEngine()
        {
            
        }

        public void Run(Func<View> render, params State[] state)
        {
            void OnStateChanged(object sender, EventArgs e) => Render();

            if (state != null)
            {
                foreach (var item in state)
                {
                    item.Changed += OnStateChanged;
                }
            }

            _render = render;

            Initialize();

            if (state != null)
            {
                foreach (var item in state)
                {
                    item.Changed -= OnStateChanged;
                }
            }
        }

        protected abstract void Initialize();

        protected void OnStarted() => Render();

        private void Render()
        {
            var view = _render!();
            UpdateView(view, _view);
            _view = view;
        }

        protected abstract void UpdateView(View view, View? oldView);
    }
}
