using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantum.Svg;
using Quantum.Svg.Shapes;

namespace Test
{
    [TestClass]
    public class DrawTest
    {
        [TestMethod]
        public void TestRectangle()
        {
            var doc = new SvgDocument(SvgVersions.Version1X)
            {
                Width = "100mm",
                Height = "100mm"
            };

            var rect = new Rectangle
            {
                X = "20mm",
                Y = "20mm",
                Width = "35mm",
                Height = "35mm",
                Style = { Parts = { ["fill"] = "red" } },
            };
            doc.AddSvgElement(rect);

            doc.Save(@"rect.svg");
        }
    }
}
