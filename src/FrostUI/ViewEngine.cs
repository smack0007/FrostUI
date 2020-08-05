using System;
using FrostUI.Views;

namespace FrostUI
{
    public abstract class ViewEngine
    {
        private View? _root = null;

        public void Run(View root)
        {
            _root = root;
            _root.ViewEngine = this;

            Initialize();

            _root.ViewEngine = null;
        }

        protected abstract void Initialize();

        protected void OnStarted()
        {
            UpdateView(_root!);
        }

        protected internal abstract void UpdateView(View view);

        protected void OnButtonClick(Button button)
        {
            button.OnClick?.Invoke();
        }
    }
}
