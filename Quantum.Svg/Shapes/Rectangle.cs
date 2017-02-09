using Quantum.Svg.Length;

namespace Quantum.Svg.Shapes
{
    public class Rectangle : SvgElement
    {
        [XmlAttribute(RenderAs = "x")]
        public LengthUnit X { get; set; }
        [XmlAttribute(RenderAs = "y")]
        public LengthUnit Y { get; set; }
        [XmlAttribute(RenderAs = "width")]
        public LengthUnit Width { get; set; }
        [XmlAttribute(RenderAs = "height")]
        public LengthUnit Height { get; set; }
        public override string TagName => "rect";
    }
}