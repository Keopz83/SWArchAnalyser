using System;

namespace SoftArch._Console
{

    public class Program
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Solution path: ");
                var solutionFilePath = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    var solution = SolutionParser.ParseSolution(solutionFilePath);
                    var classCount = 0;


                    foreach (var project in solution.Projects)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Project: {project.Name}");
                        Console.WriteLine();

                        foreach (var csClass in project.Classes)
                        {
                            classCount++;
                            Console.WriteLine(csClass.ToString());
                        }
                    }

                    Console.ReadLine();

                } catch(Exception exc)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exc.ToString());
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }

        }

    }
}
