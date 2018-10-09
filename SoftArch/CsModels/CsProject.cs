
using System.Collections.Generic;
using System.Linq;

namespace SoftArch.CsModels {

    public class CsProject {

        public string Name { get; set; }

        public IEnumerable<CsClass> Classes = new List<CsClass>();

        public CsClass GetClass(string className) {

            return Classes.Where(x => x.Name.Equals(className)).FirstOrDefault();
        }
    }
}