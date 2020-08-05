#nullable enable
using System;
using System.Collections.Generic;

namespace FrostUI.Views
{
	public partial class Button : View
	{
		public string Label { get; }

		public Action? OnClick { get; }

		public Button(
			string label,
			Action? onClick = default)
		{
			 Label = label;
			 OnClick = onClick;
		}
	}
}

