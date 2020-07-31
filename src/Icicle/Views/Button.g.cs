using System;

namespace Icicle.Views
{
	public class Button : View
	{
		public string Text { get; }

		public Action? OnClick { get; }

		public Button(
			string text = default,
			Action? onClick = default)
		{
			 Text = text;
			 OnClick = onClick;
		}
	}
}
