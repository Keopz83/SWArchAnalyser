

namespace SoftArch.CsModels {
 
    public class CsMethod {

        public string Name { get; set; }

        public string Type { get; set; }

        public override string ToString() {

            if (Type != null) {
                return $"M: {Type} {Name}";
            }
            return Name;
        }

    }
}