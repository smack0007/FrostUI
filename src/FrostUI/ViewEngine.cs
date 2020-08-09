using System;
using FrostUI.Controls;

namespace FrostUI
{
    public abstract class ViewEngine
    {
        protected View? RootView { get; private set; }

        public void Run(View root)
        {
            RootView = root;
            RootView.ViewEngine = this;

            Initialize();

            RootView.ViewEngine = null;
        }

        protected abstract void Initialize();

        protected void OnStarted()
        {
            UpdateView(RootView!);
        }

        protected internal void UpdateView(View view)
        {
            view.RenderedView = Render(view, view.RenderedView);
        }

        protected abstract object Render(View view, object? renderedView);

        protected void OnButtonClick(Button button)
        {
            button.OnClick?.Invoke();
        }
    }
}
