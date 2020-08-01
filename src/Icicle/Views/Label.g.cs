using System;
using System.Collections.Generic;

namespace Icicle.Views
{
	public class Label : View
	{
		public string Text { get; }

		public Label(
			string text = default)
		{
			 Text = text;
		}
	}
}

