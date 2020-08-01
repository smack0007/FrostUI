using System;
using Icicle.Views;

namespace Icicle
{
    public abstract class ViewEngine
    {
        private Func<View>? _render;

        private View? _view;

        public void Run(Func<View> render)
        {
            _render = render;

            Initialize();
        }

        protected abstract void Initialize();

        protected void OnStarted() => Render();

        private void Render()
        {
            var view = _render!();
            UpdateView(view);
            _view = view;
        }

        protected abstract void UpdateView(View view);

        protected void OnButtonClick(Button button)
        {
            button.OnClick?.Invoke();
            Render();
        }
    }
}
