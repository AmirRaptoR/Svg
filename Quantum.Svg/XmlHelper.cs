using System.Xml;

namespace Quantum.Svg
{
    internal static class XmlHelper
    {
        public static void AddAttribute(this XmlNode node, string name, string value, XmlDocument doc = null)
        {
            if (doc == null && node.OwnerDocument == null)
                return;
            var attrib = (doc ?? node.OwnerDocument).CreateAttribute(name);
            attrib.Value = value;
            node.Attributes.Append(attrib);
        }
    }
}