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

namespace CG_Proj3
{
    public partial class MainWindow : Window
    {
        private bool isMouseDown = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
        }

        private void mainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Ellipse mybrush = new Ellipse();

            mybrush.Width = 5;
            mybrush.Height = 5;

            if (isMouseDown)
            {
                Point brushPosition = new Point(e.GetPosition(mainCanvas).X, e.GetPosition(mainCanvas).Y);
                mybrush.Fill = new SolidColorBrush(Colors.Black);

                Canvas.SetTop(mybrush, brushPosition.Y);
                Canvas.SetLeft(mybrush, brushPosition.X);

                mainCanvas.Children.Add(mybrush);
            }
        }
    }
}
