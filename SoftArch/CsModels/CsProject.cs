
using System.Collections.Generic;

namespace SoftArch.CsModels {

    public class CsProject {

        public string Name { get; set; }

        public IEnumerable<CsClass> Classes;

        
    }
}