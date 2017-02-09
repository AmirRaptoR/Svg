using System.Collections.Generic;
using System.Text;

namespace Quantum.Svg
{
    public class StyleAttribute
    {
        public Dictionary<string, string> Parts { get; } = new Dictionary<string, string>();

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var part in Parts)
            {
                stringBuilder.Append($"{part.Key}:{part.Value};");
            }
            return stringBuilder.ToString();
        }
    }
}