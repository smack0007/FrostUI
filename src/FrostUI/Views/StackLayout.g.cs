#nullable enable
using System;
using System.Collections.Generic;

namespace FrostUI.Views
{
	public partial class StackLayout : View
	{
		public State<Orientation> Orientation { get; }

		public IReadOnlyList<View> Children { get; }

		public StackLayout(
			State<Orientation> orientation,
			IReadOnlyList<View> children)
			: base(false)
		{
			Orientation = orientation;
			Children = children;
			InitializeState();
		}
	}
}

