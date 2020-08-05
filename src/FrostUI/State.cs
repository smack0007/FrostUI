using System;
using System.Collections.Generic;
using System.Text;

namespace FrostUI
{
    public class State
    {
        internal View? View { get; set; }

        internal List<State>? Binds { get; set; }

        internal State()
        {
        }

        protected void OnValueChanged()
        {
            if (Binds != null)
                foreach (var bind in Binds)
                    bind.OnValueChanged();

            View?.OnStateChanged(this);
        }
    }

    public abstract class StateValue<T> : State
    {
        public abstract T GetValue();
    }

    public class State<T> : StateValue<T>
    {
        private T _value;

        public T Value
        {
            get => _value;

            set
            {
                _value = value;
                OnValueChanged();
            }
        }

        public State(T defaultValue = default)
        {
            _value = defaultValue;
        }

        public override string ToString() => Value?.ToString() ?? "";

        public override T GetValue() => Value;

        public static implicit operator State<T>(T value) => new State<T>(value);

        public BindState<U> Bind<U>(Func<U> bind) => new BindState<U>(this, bind);
    }

    public class BindState<T> : StateValue<T>
    {
        private Func<T> _bind;

        public T Value => _bind();

        public BindState(State source, Func<T> bind)
        {
            if (source.Binds == null)
                source.Binds = new List<State>();

            source.Binds.Add(this);

            _bind = bind;
        }

        public override T GetValue() => Value;
    }
}
