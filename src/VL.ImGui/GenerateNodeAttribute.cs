using System;
using System.Collections.Generic;
using System.Text;

namespace VL.ImGui
{
    public class GenerateNodeAttribute : Attribute
    {
        public string? Name { get; set; }
    }
}
