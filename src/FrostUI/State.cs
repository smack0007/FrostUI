using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace FrostUI
{
    public abstract class State
    {
        private List<WeakReference<IStateListener>>? _listeners;
        
        internal List<State>? Binds { get; set; }

        internal State()
        {
        }

        protected void OnValueChanged()
        {
            if (Binds != null)
                foreach (var bind in Binds)
                    bind.OnValueChanged();


            if (_listeners != null)
            {
                foreach (var listener in _listeners)
                {
                    if (listener.TryGetTarget(out var target))
                        target.OnStateChanged(this);
                }
            }
        }

        public void Subscribe(IStateListener listener)
        {
            if (_listeners == null)
                _listeners = new List<WeakReference<IStateListener>>();

            _listeners.Add(new WeakReference<IStateListener>(listener));
        }

        public void Unsubscribe(IStateListener listener)
        {
            if (_listeners == null)
                return;

            _listeners.RemoveAll(x => x.TryGetTarget(out var target) && target == listener);
        }
    }

    public abstract class State<T> : State
    {
        public abstract T GetValue();

        public override string ToString() => GetValue()?.ToString() ?? "";

        public static implicit operator State<T>(T value) => new ConstState<T>(value);

        public static implicit operator T(State<T> state) => state.GetValue();

        public BindState<T, T> Bind() => new BindState<T, T>(this);

        public BindState<T, R> Bind<R>(Func<T, R> bind) => new BindState<T, R>(this, bind);
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

    public class BindState<T, R> : State<R>
    {
        private State<T> _source;
        private Func<T, R> _bind;

        public R Value => _bind(_source.GetValue());

        public BindState(State<T> source)
            : this(source, (T x) => (R)(object)x!)
        {
        }

        public BindState(State<T> source, Func<T, R> bind)
        {
            _source = source;

            if (source.Binds == null)
                source.Binds = new List<State>();

            source.Binds.Add(this);

            _bind = bind;
        }

        public override R GetValue() => Value;
    }
}
