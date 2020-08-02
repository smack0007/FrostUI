#nullable enable
using System;
using System.Collections.Generic;

namespace Icicle.Views
{
	public partial class VStack : View
	{
		public IReadOnlyList<View> Children { get; }

		public VStack(
			IReadOnlyList<View> children)
		{
			 Children = children;
		}
	}
}

