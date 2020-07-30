using System;
using System.Collections.Generic;
using System.Text;

namespace Icicle
{
    public abstract class ViewEngine
    {
        private Func<View> _render;

        public void Run(Func<View> render)
        {
            _render = render;
            
            Initialize();
        }

        protected abstract void Initialize();

        protected void OnStarted()
        {
            var view = _render();
            UpdateView(view);
        }

        protected abstract void UpdateView(View view);
    }
}
