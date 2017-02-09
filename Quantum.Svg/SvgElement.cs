using System.Collections.Generic;
using System.Xml;
using Quantum.Svg.Shapes;

namespace Quantum.Svg
{
    public abstract class SvgElement
    {
        public abstract string TagName { get; }
        [XmlAttribute(RenderAs = "id")]
        public string Id { get; set; }
        [XmlAttribute(RenderAs = "class")]
        public string Class { get; set; }
        [XmlAttribute(RenderAs = "style")]
        public StyleAttribute Style { get; } = new StyleAttribute();
        public ICollection<SvgElement> Children { get; } = new List<SvgElement>();

        public virtual XmlElement Draw(XmlDocument doc)
        {
            var attributes = new Dictionary<string, string>();
            var properties = GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var attrs = propertyInfo.GetCustomAttributes(typeof(XmlAttributeAttribute), true);
                if (attrs.Length == 0)
                    continue;
                if (!(attrs[0] is XmlAttributeAttribute))
                    continue;
                attributes[((XmlAttributeAttribute)attrs[0]).RenderAs] = propertyInfo.GetValue(this)?.ToString() ?? "";
            }

            var element = doc.CreateElement(TagName);
            foreach (var attribute in attributes)
                element.AddAttribute(attribute.Key, attribute.Value);

            return element;
        }
    }
}