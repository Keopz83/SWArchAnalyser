using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftArch.CsModels {
    
    interface ICsGraphNode {

        IEnumerable<CsGraphEdge> Edges { get; }
    }
}
