using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using Quantum.Svg.Length;

namespace Quantum.Svg
{
    public class SvgDocument
    {
        private readonly List<SvgElement> _elements = new List<SvgElement>();

        public SvgDocument(SvgVersions version)
        {
            Version = version;
        }

        public SvgVersions Version { get; }
        public LengthUnit Width { get; set; }
        public LengthUnit Height { get; set; }

        public IEnumerable<SvgElement> Elements => _elements.AsReadOnly();

        public void AddSvgElement(SvgElement element)
        {
            _elements.Add(element);
        }

        public SvgElement GetById(string id)
        {
            return _elements.FirstOrDefault(x => x.Id == id);
        }

        public bool RemoveSvgElement(string id)
        {
            return _elements.Remove(GetById(id));
        }

        public void Save(string filename)
        {
            var xmlDoc = new XmlDocument { XmlResolver = null };
            var xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlDecl);
            var docType = xmlDoc.CreateDocumentType("svg", "-//W3C//DTD SVG 1.1//EN", "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd", null);
            xmlDoc.AppendChild(docType);
            var svgNode = xmlDoc.CreateNode(XmlNodeType.Element, "svg", "http://www.w3.org/2000/svg");
            svgNode.AddAttribute("width", Width.ToString());
            svgNode.AddAttribute("height", Height.ToString());
//            svgNode.AddAttribute("viewBox", $"0 0 {Width.Convert(LengthType.Pixel)} {Height.Convert(LengthType.Pixel)}");
            svgNode.AddAttribute("version", "1.1");

            foreach (var element in Elements)
                RenderElement(xmlDoc, svgNode, element);

            xmlDoc.AppendChild(svgNode);
            xmlDoc.Save(filename);
        }

        private void RenderElement(XmlDocument doc, XmlNode node, SvgElement element)
        {
            var el = element.Draw(doc);
            node.AppendChild(el);
            foreach (var elementChild in element.Children)
                RenderElement(doc, el, elementChild);
        }
    }
}