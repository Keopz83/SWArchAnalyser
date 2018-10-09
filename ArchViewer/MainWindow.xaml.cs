using SoftArch.CsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArchViewer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();

            
        }

        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);

            TryDraw();
        }

        private void TryDraw() {

            int left = 10;
            int top = 10;

            foreach (var csClass in (DataContext as CsProject).Classes) {
                AddClass(csClass, left, top);
                top += 60;
            }
            
        }

        private void AddClass(CsClass csClass, int left, int top) {

            var border = new Border() {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1)
            };

            var stackPanel = new StackPanel();

            border.Child = stackPanel;

            var className = new Label() {
                Content = csClass.Name,
                //Background = Brushes.LightGray,
                FontWeight = FontWeights.Bold
            };

            stackPanel.Children.Add(className);

            stackPanel.Children.Add(new Separator());

            foreach (var csProp in csClass.Properties) {
                //var border = new Border() {
                //    BorderBrush = Brushes.Black,
                //    BorderThickness = new Thickness(1)
                //};
                var prop = new Label() {
                    Content = csProp.Name,
                    //Background = Brushes.LightGray
                };
                //border.Child = prop;
                stackPanel.Children.Add(prop);
            }

            
            Canvas.SetLeft(border, left);
            Canvas.SetTop(border, top);
            Canvas1.Children.Add(border);
        }
    }
}
