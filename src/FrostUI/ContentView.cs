using System;
using System.Collections.Generic;
using System.Text;

namespace FrostUI
{
    public class ContentView : View
    {
        private View? _content;

        public View? Content
        {
            get => _content;

            set
            {
                if (value != _content)
                {
                    _content = value;
                    OnContentChanged();
                }
            }
        }

        protected virtual void OnContentChanged()
        {
        }
    }
}
