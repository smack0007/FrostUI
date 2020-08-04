#nullable enable
using System;
using System.Collections.Generic;

namespace FrostUI.Views
{
	public partial class Text : View
	{
		public string Value { get; }

		public Text(
			string value)
		{
			 Value = value;
		}
	}
}

