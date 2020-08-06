#nullable enable
using System;
using System.Collections.Generic;

namespace FrostUI.Views
{
	public partial class VStack : View
	{
		public IReadOnlyList<View> Children { get; }

		public VStack(
			IReadOnlyList<View> children)
			: base(false)
		{
			Children = children;
			InitializeState();
		}
	}
}

