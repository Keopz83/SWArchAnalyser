
namespace SoftArch.CsModels {

    public class CsProperty {

        public string Name { get; set; }

        public string Type { get; set; }

        public string Access { get; set; }

        public override string ToString() {

            return $"P: {Access} {Type} {Name}";

        }
    }
}