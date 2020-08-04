#nullable enable
using System;
using System.Collections.Generic;

namespace FrostUI.Views
{
	public partial class Button : View
	{
		public View Label { get; }

		public Action? OnClick { get; }

		public Button(
			View label,
			Action? onClick = default)
		{
			 Label = label;
			 OnClick = onClick;
		}
	}
}

