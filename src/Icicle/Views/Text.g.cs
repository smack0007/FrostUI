#nullable enable
using System;
using System.Collections.Generic;

namespace Icicle.Views
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

