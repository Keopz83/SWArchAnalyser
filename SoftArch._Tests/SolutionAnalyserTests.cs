using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using SoftArch;
using System.IO;
using TestProject1;
using SoftArch.CsModels;

namespace SoftArch._Tests {

    [TestClass]
    public class SolutionAnalyserTests {

        private static string _testSolutionFilePath;

        [TestInitialize]
        public void TestInit() {
            var baseDir = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);
            var testSolutionBaseDir = baseDir.Parent.Parent.Parent.FullName;
            _testSolutionFilePath = Path.Combine(testSolutionBaseDir, @"TestSolution\TestSolution.sln");
        }

        [TestMethod]
        public void Retrieves_solution_projects() {

            var solution = SolutionParser.ParseSolution(_testSolutionFilePath);

            Assert.AreEqual(1, solution.Projects.Count());
            var project = solution.Projects.ElementAt(0);
            Assert.AreEqual("TestProject1", project.Name);
        }

        [TestMethod]
        public void Retrieves_project_classes() {

            var solution = SolutionParser.ParseSolution(_testSolutionFilePath);

            var project = solution.Projects.ElementAt(0);
            Assert.AreEqual(3, project.Classes.Count());
            Assert.AreEqual("Class1", project.Classes.ElementAt(0).Name);
        }

        [TestMethod]
        public void Retrieves_base_type() {

            var solution = SolutionParser.ParseSolution(_testSolutionFilePath);

            var project = solution.Projects.ElementAt(0);
            var csClass = project.Classes.ElementAt(1);
            Assert.AreEqual("Class1Child1", csClass.Name);
            Assert.AreEqual("Class1", csClass.ParentName);
        }


        [TestMethod]
        public void Retrieves_type_methods() {

            CsSolution solution = SolutionParser.ParseSolution(_testSolutionFilePath);

            var project = solution.Projects.ElementAt(0);
            var csClass = project.GetClass(nameof(Class2));
            var csMethods = csClass.Methods;
            Assert.AreEqual(1, csMethods.Count(), "Unexpected number of methods.");
            Assert.AreEqual(nameof(Class2.MethodInternalExternalVars), csMethods.First().Name, "Unexpected method name.");
        }


        [TestMethod]
        public void Retrieves_type_properties() {

            CsSolution solution = SolutionParser.ParseSolution(_testSolutionFilePath);
            CsProject project = solution.Projects.ElementAt(0);
            CsClass csClass = project.GetClass(nameof(Class2));
            var csProps = csClass.Properties;

            Assert.AreEqual(3, csProps.Count(), "Unexpected number of properties.");


            CsProperty csProp = csProps.First();

            Assert.AreEqual(nameof(Class2.MethodInternalExternalVars), csProp.Name, "Unexpected property name.");
            Assert.AreEqual(nameof(Class2.MethodInternalExternalVars), csProp.Type, "Unexpected property type.");
            Assert.AreEqual("public", csProp.Access, "Unexpected property access.");
        }


        [TestMethod]
        public void Retrieves_method_variables() {

        }


    }
}
