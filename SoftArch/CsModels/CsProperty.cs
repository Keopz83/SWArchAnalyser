
namespace SoftArch.CsModels {

    public class CsProperty {

        public string Name { get; set; }

        public string Type { get; set; }

        public override string ToString() {

            if (Type != null) {
                return $"P: {Type} {Name}";
            }
            return Name;
        }
    }
}