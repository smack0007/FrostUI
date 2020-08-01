using System;
using System.Collections.Generic;

namespace Icicle.Views
{
	public class VStack : View
	{
		public IReadOnlyList<View> Children { get; }

		public VStack(
			IReadOnlyList<View> children = default)
		{
			 Children = children;
		}
	}
}

