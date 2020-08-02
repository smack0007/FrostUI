#nullable enable
using System;
using System.Collections.Generic;

namespace Icicle.Views
{
	public class Button : View
	{
		public string Text { get; }

		public Action? OnClick { get; }

		public Button(
			string text,
			Action? onClick = default)
		{
			 Text = text;
			 OnClick = onClick;
		}
	}
}

