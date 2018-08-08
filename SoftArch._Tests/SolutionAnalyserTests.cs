using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using SoftArch;
using System.IO;

namespace SoftArch._Tests {

    [TestClass]
    public class SolutionAnalyserTests {

        private static string solutionFilePath;

        [TestInitialize]
        public void TestInit() {
            var baseDir = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);
            var testSolutionBaseDir = baseDir.Parent.Parent.Parent.FullName;
            solutionFilePath = Path.Combine(testSolutionBaseDir, @"TestSolution\TestSolution.sln");
        }

        [TestMethod]
        public void Retrieves_solution_projects() {

            var solution = SolutionParser.ParseSolution(solutionFilePath);

            Assert.AreEqual(1, solution.Projects.Count());
            var project = solution.Projects.ElementAt(0);
            Assert.AreEqual("TestProject1", project.Name);
        }

        [TestMethod]
        public void Retrieves_project_classes() {

            var solution = SolutionParser.ParseSolution(solutionFilePath);

            var project = solution.Projects.ElementAt(0);
            Assert.AreEqual(3, project.Classes.Count());
            Assert.AreEqual("Class1", project.Classes.ElementAt(0).Name);
        }

        [TestMethod]
        public void Retrieves_base_type() {

            var solution = SolutionParser.ParseSolution(solutionFilePath);

            var project = solution.Projects.ElementAt(0);
            var csClass = project.Classes.ElementAt(1);
            Assert.AreEqual("Class1Child1", csClass.Name);
            Assert.AreEqual("Class1", csClass.ParentName);
        }


    }
}
