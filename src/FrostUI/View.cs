using System.Linq;

namespace FrostUI
{
    public abstract class View
    {
        private View _content;

        internal ViewEngine? ViewEngine { get; set; }

        internal object? RenderedView { get; set; }

        public View Content
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

        public View(bool initializeState = true)
        {
            if (initializeState)
                InitializeState();
        }

        protected void InitializeState()
        {
            foreach (var property in GetType().GetProperties().Where(x => typeof(State).IsAssignableFrom(x.PropertyType)))
            {
                var state = property.GetValue(this) as State;

                if (state == null)
                    throw new FrostUIException($"{GetType().FullName}.{property.Name} was null. All state properties are required to be initialized.");

                state.View = this;
            }
        }

        internal void OnStateChanged(State state)
        {
            ViewEngine?.UpdateView(this);
        }

        private void OnContentChanged()
        {
        }
    }
}
