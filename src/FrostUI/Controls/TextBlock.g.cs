#nullable enable
using System;
using System.Collections.Generic;

namespace FrostUI.Controls
{
	public partial class TextBlock : View
	{
		public State<string> Value { get; }

		public TextBlock(
			State<string> value)
			: base(false)
		{
			Value = value;
			InitializeState();
		}
	}
}

