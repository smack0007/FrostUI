#nullable enable
using System;
using System.Collections.Generic;

namespace FrostUI.Views
{
	public partial class Button : View
	{
		public State<string> Label { get; }

		public Action? OnClick { get; }

		public Button(
			State<string> label,
			Action? onClick = default)
			: base(false)
		{
			Label = label;
			OnClick = onClick;
			InitializeState();
		}
	}
}

