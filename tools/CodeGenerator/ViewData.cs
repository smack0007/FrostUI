using System.Collections.Generic;

namespace CodeGenerator
{
    public class ViewData
    {
        public Dictionary<string, ViewPropertyData> Properties { get; set; } = default!;
    }
}
