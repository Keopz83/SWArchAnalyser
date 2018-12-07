using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Build.Construction;
using SoftArch.CsModels;

namespace SoftArch
{
    public class SolutionParser
    {

        public static CsSolution ParseSolution(string solutionFilePath) {

            var solution = new CsSolution();
            SolutionFile solutionFile = SolutionFile.Parse(solutionFilePath);
            var projectFiles = solutionFile.ProjectsInOrder;
            var projects = new List<CsProject>();
            foreach (var projectFile in projectFiles) {
                
                if(!Path.GetExtension(projectFile.AbsolutePath).Equals(".csproj")){
                    //TODO: support recursive folder search
                    Console.WriteLine($"Skipping path '{projectFile.AbsolutePath}'...");
                    continue;
                }

                ProjectRootElement projectToParse = ProjectRootElement.Open(projectFile.AbsolutePath);
                var csProject = new CsProject() { Name = projectFile.ProjectName };
                csProject.Classes = ParseProjectRoot(projectFile, projectToParse);
                projects.Add(csProject);
            }
            solution.Projects = projects;
            return solution;
        }

        private static List<CsClass> ParseProjectRoot(ProjectInSolution solutionProject, ProjectRootElement projectRoot) {

            
            var csFiles = projectRoot.AllChildren.OfType<ProjectItemElement>().Where(x => x.ItemType.Equals("Compile"));

            var projectClasses = new List<CsClass>();
            foreach (var csFile in csFiles) {
                var csFileClasses = ParseCsFile(solutionProject, csFile);
                projectClasses.AddRange(csFileClasses);
            }

            return projectClasses;
        }


        private static List<CsClass> ParseCsFile(ProjectInSolution solutionProject, ProjectItemElement csFile) {

            var filePath = csFile.Include;
            var fileAbsolutePath = Path.Combine(Path.GetDirectoryName(solutionProject.AbsolutePath), filePath);
            var source = File.ReadAllText(fileAbsolutePath);
            SyntaxTree projectSyntaxTree = CSharpSyntaxTree.ParseText(source);
            var projectTree = (CompilationUnitSyntax)projectSyntaxTree.GetRoot();

            var namespaceTree = projectTree.Members.OfType<NamespaceDeclarationSyntax>().SingleOrDefault();

            var csFileClasses = new List<CsClass>();
            if (namespaceTree == null) {
                return csFileClasses;
            }

            foreach (var classTree in namespaceTree.Members.OfType<ClassDeclarationSyntax>()) {

                if(classTree.BaseList?.Types == null)
                {
                    continue;
                }

                foreach (var baseType in classTree.BaseList?.Types.OfType<SimpleBaseTypeSyntax>()) {

                    var props = classTree.Members.OfType<PropertyDeclarationSyntax>()
                        .Select(x => new CsProperty() { Name = x.Identifier.ToString(), Type = x.Type.ToString() });

                    var methods = classTree.Members.OfType<MethodDeclarationSyntax>()
                        .Select(x => ParseMethod(x));

                    var csClass = new CsClass() {
                        Name = classTree.Identifier.ToString(),
                        ParentName = baseType?.Type.ToString(),
                        Properties = props,
                        Methods = methods
                    };

                    csFileClasses.Add(csClass);
                }

            }

            return csFileClasses;
        }

        private static CsMethod ParseMethod(MethodDeclarationSyntax x) {

            var method = new CsMethod() {
                Name = x.Identifier.ToString(),
                Type = x.ReturnType.ToString()
            };

            return method;
        }
    }

}
