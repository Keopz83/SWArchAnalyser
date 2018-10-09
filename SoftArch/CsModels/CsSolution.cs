using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftArch.CsModels {

    public class CsSolution {

        public IEnumerable<CsProject> Projects = new List<CsProject>();

        public CsProject GetProject(string projectName) {

            return Projects.Where(x => x.Name.Equals(projectName)).FirstOrDefault();
        }
    }
}
