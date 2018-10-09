using SoftArch;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ArchViewer {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            var baseDir = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);
            var solutionBaseDir = baseDir.Parent.Parent.Parent.FullName;
            var solutionFilePath = Path.Combine(solutionBaseDir, @"TestSolution\TestSolution.sln");
            var solution = SolutionParser.ParseSolution(solutionFilePath);
            var project = solution.Projects.First();

            var window = new MainWindow() {
                DataContext = project
            };

            window.Show();
        }
    }
}
