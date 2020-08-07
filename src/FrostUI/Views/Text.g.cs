#nullable enable
using System;
using System.Collections.Generic;

namespace FrostUI.Views
{
	public partial class Text : View
	{
		public State<string> Value { get; }

		public Text(
			State<string> value)
			: base(false)
		{
			Value = value;
			InitializeState();
		}
	}
}

