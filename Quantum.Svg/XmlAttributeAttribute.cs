using System;

namespace Quantum.Svg
{
    [AttributeUsage(AttributeTargets.Property)]
    public class XmlAttributeAttribute : Attribute
    {
        public string RenderAs { get; set; }
    }
}