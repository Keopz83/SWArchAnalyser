using SoftArch;
using SoftArch.CsModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfUi.ViewModel
{
    public class MainViewModel: INotifyPropertyChanged
    {
        private CsSolution _parsedSolution;

        private string _solutionPath;
        public string SolutionPath
        {
            get
            {
                return _solutionPath;
            }
            set
            {
                if(_solutionPath != value)
                {
                    _solutionPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public void ParseSolution()
        {
            _parsedSolution = SolutionParser.ParseSolution(SolutionPath);
            var classCount = 0;

            foreach (var project in _parsedSolution.Projects)
            {
                //Console.WriteLine();
                //Console.WriteLine($"Project: {project.Name}");
                //Console.WriteLine();

                foreach (var csClass in project.Classes)
                {
                    classCount++;
                    //Console.WriteLine(csClass.ToString());
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
