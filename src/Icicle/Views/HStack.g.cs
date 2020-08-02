#nullable enable
using System;
using System.Collections.Generic;

namespace Icicle.Views
{
	public partial class HStack : View
	{
		public IReadOnlyList<View> Children { get; }

		public HStack(
			IReadOnlyList<View> children)
		{
			 Children = children;
		}
	}
}

