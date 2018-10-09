using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1 {

    public class Class2 {

        private int field1 = 1;
        public int field2 = 2;

        public Class1 RefToClass1 { get; set; }
        public Class1 Ref2ToClass1 { get; set; }

        public void MethodInternalExternalVars() {

            var internalVar1 = 3;
            var internalVar2 = "abc";
            var externalVar = new Class1();

        }

    }
}
