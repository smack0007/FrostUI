using System;

namespace Icicle
{
    public class State
    {
        public event EventHandler? Changed = null;

        protected void OnChanged() =>
            Changed?.Invoke(this, EventArgs.Empty);
    }

    public class State<T> : State
    {
        private T _value = default;

        public T Value
        {
            get => _value;
            
            set
            {
                _value = value;
                OnChanged();
            }
        }
    }
}
