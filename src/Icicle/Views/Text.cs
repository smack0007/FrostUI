namespace Icicle.Views
{
    public partial class Text
    {
        public static implicit operator Text(string text) => new Text(text);
    }
}
