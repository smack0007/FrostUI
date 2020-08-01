using System;
using System.Collections.Generic;

namespace Icicle.Views
{
	public class HStack : View
	{
		public IReadOnlyList<View> Children { get; }

		public HStack(
			IReadOnlyList<View> children = default)
		{
			 Children = children;
		}
	}
}

