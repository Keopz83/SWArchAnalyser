
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftArch.CsModels {

    public class CsClass {

        public string Name { get; set; }

        public string ParentName { get; set; }

        public IEnumerable<string> InterfaceNames { get; set; } = new List<string>();

        public IEnumerable<CsProperty> Properties = new List<CsProperty>();

        public IEnumerable<CsMethod> Methods = new List<CsMethod>();

        public CsMethod GetMethod(string methodName) {

            return Methods.Where(x => x.Name.Equals(methodName)).FirstOrDefault();
        }

        public override string ToString() {

            var stringBuilder = new StringBuilder();

            if (ParentName != null) {
                stringBuilder.AppendLine($"C: {Name} -> {ParentName}");
            }
            else {
                stringBuilder.AppendLine("C: " + Name);
            }

            foreach (var prop in Properties) {
                stringBuilder.AppendLine(" " + prop.ToString());
            }

            foreach (var method in Methods) {
                stringBuilder.AppendLine(" " + method.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}