using System;
using System.IO;

namespace SoftArch._Console {

    public class Program {

        static void Main(string[] args) {


            //var solutionFilePath = @"C:\Users\user1\source\repos\SWArchAnalyser\SoftArch.sln";
            var solutionFilePath = @"C:\Users\user1\source\repos\SWArchAnalyser\TestSolution\TestSolution.sln";

            var solution = SolutionParser.ParseSolution(solutionFilePath);
            var classCount = 0;
            foreach(var project in solution.Projects) {
                Console.WriteLine();
                Console.WriteLine($"Project: {project.Name}");
                Console.WriteLine();

                foreach(var csClass in project.Classes) {
                    classCount++;
                    Console.WriteLine(csClass.ToString());
                }
            }

            Console.ReadLine();
        }

    }
}
