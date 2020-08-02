using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator
{
    public class ViewPropertyData
    {
        public string Type { get; set; } = "";

        public bool Required { get; set; } = false;

        public string Default { get; set; } = "default";
    }
}
