using System;
using System.Collections.Generic;

namespace FrostUI
{
    public abstract class State
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

        public BindState<U> Bind<U>(Func<U> bind) => new BindState<U>(this, bind);
    }

    public abstract class State<T> : State
    {
        public abstract T GetValue();

        public override string ToString() => GetValue()?.ToString() ?? "";

        public static implicit operator State<T>(T value) => new ConstState<T>(value);
    }

    public class ConstState<T> : State<T>
    {
        public T Value { get; }

        public ConstState(T value)
        {
            Value = value;
        }

        public override T GetValue() => Value;

        public static implicit operator ConstState<T>(T value) => new ConstState<T>(value);
    }

    public class MutableState<T> : State<T>
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

        public MutableState(T defaultValue = default)
        {
            _value = defaultValue;
        }

        public override T GetValue() => Value;

        public static implicit operator MutableState<T>(T value) => new MutableState<T>(value);
    }

    public class BindState<T> : State<T>
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
