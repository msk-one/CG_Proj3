using System;
using System.Collections;
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
        private bool lineMode= false;
        private bool circleMode = false;
        private int lineSize = 1;
        private List<Point> linePoints;

        public MainWindow()
        {
            InitializeComponent();
            linePoints = new List<Point>();
        }

        private void mainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
        }

        private void mainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
            if (lineMode)
            {
                if (linePoints.Count < 1)
                {
                    linePoints.Add(new Point(e.GetPosition(mainCanvas).X, e.GetPosition(mainCanvas).Y));
                }
                else if (linePoints.Count == 1)
                {
                    linePoints.Add(new Point(e.GetPosition(mainCanvas).X, e.GetPosition(mainCanvas).Y));
                    //Line line = new Line();
                    //line.Stroke = Brushes.Black;
                    //line.StrokeThickness = lineSize;
                    //line.X1 = linePoints[0].X;
                    //line.Y1 = linePoints[0].Y;
                    //line.X2 = linePoints[1].X;
                    //line.Y2 = linePoints[1].Y;

                    //mainCanvas.Children.Add(line);
                    //linePoints.Clear();

                    Algorithms alg = new Algorithms();
                    //TODO: midline, symm. midline, gupta-sproull
                    if (antialiasing_type.Text == "MidLine")
                    {
                        alg.MidpointLine(mainCanvas, Convert.ToInt32(linePoints[0].X), Convert.ToInt32(linePoints[0].Y), Convert.ToInt32(linePoints[1].X), Convert.ToInt32(linePoints[1].Y));
                    }
                    else if (antialiasing_type.Text == "Symmetric MidLine")
                    {
                        alg.SymmetricMidpointLine(mainCanvas, Convert.ToInt32(linePoints[0].X), Convert.ToInt32(linePoints[0].Y), Convert.ToInt32(linePoints[1].X), Convert.ToInt32(linePoints[1].Y));
                    }
                    else if (antialiasing_type.Text == "Gupta-Sproull")
                    {

                    }
                    linePoints.Clear();
                }
            }
            else
            {
                //Ellipse customCircle = new Ellipse();
                //Point brushPosition = new Point(e.GetPosition(mainCanvas).X, e.GetPosition(mainCanvas).Y);
                //customCircle.Fill = Brushes.Transparent;
                //customCircle.StrokeThickness = lineSize;
                //customCircle.Stroke = Brushes.Black;

                //customCircle.Width = lineSize*10;
                //customCircle.Height = lineSize*10;

                //Canvas.SetTop(customCircle, brushPosition.Y);
                //Canvas.SetLeft(customCircle, brushPosition.X);

                //mainCanvas.Children.Add(customCircle);

                //TODO: midline
                Algorithms alg = new Algorithms();
                if (antialiasing_type.Text == "MidLine")
                {
                    alg.MidpointCircle(mainCanvas, lineSize*10);
                }
            }
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e) {}

        private void line_button_Click(object sender, RoutedEventArgs e)
        {
            lineMode = true;
            circleMode = false;
            lineSize = Convert.ToInt32(brushSize.Text);
        }
   
        private void circle_button_Click(object sender, RoutedEventArgs e)
        {
            lineMode = false;
            circleMode = true;
            lineSize = Convert.ToInt32(brushSize.Text);
        }
    }
}
