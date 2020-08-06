#nullable enable
using System;
using System.Collections.Generic;

namespace FrostUI.Views
{
	public partial class Text : View
	{
		public StateValue<string> Value { get; }

		public Text(
			StateValue<string> value)
			: base(false)
		{
			Value = value;
			InitializeState();
		}
	}
}

