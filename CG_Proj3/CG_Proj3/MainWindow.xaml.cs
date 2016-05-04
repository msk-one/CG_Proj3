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
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace CG_Proj3
{
    public partial class MainWindow : Window
    {
        private bool lineMode= false;
        private bool circleMode = false;
        private int lineSize = 1;
        private bool mouseClicked = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void line_button_Click(object sender, RoutedEventArgs e)
        {
            lineMode = true;
            circleMode = false;
            int x1, x2, y1, y2;
            int thickness = 1;

            try
            {
                x1 = Convert.ToInt32(X1.Text);
                x2 = Convert.ToInt32(X2.Text);
                y1 = Convert.ToInt32(Y1.Text);
                y2 = Convert.ToInt32(Y2.Text);

                thickness = Convert.ToInt32(brushSize.Text);

                Algorithms.pixelSize = thickness;

                if (antialiasing_type.Text == "MidLine")
                {
                    Algorithms.MidpointLine(mainCanvas, x1, y1, x2, y2);
                }
                else if (antialiasing_type.Text == "Symmetric MidLine")
                {
                    Algorithms.SymmMidpointLine(mainCanvas, x1, y1, x2, y2);
                }
                else if (antialiasing_type.Text == "Gupta-Sproull")
                {
                    //Might want to look at both algorithms, thus not working properly either
                    //Algorithms.GuptaSproull(mainCanvas, x1, y1, x2, y2, thickness); -- this one hangs in infinite loop
                    Algorithms.GuptaSproull2(mainCanvas, x1, y1, x2, y2); //this one is close, but draws weird line
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Input of wrong format!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something wrong happened!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
   
        private void circle_button_Click(object sender, RoutedEventArgs e)
        {
            lineMode = false;
            circleMode = true;
            int thickness = 1;

            try
            {
                int x1 = Convert.ToInt32(X1.Text);
                int y1 = Convert.ToInt32(Y1.Text);

                thickness = Convert.ToInt32(brushSize.Text);
                Algorithms.pixelSize = thickness;

                if (antialiasing_type.Text == "MidLine")
                {
                    Algorithms.MidpointCircle(mainCanvas, x1, y1, Convert.ToInt32(R.Text));
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Input of wrong format!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something wrong happened!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseClicked) {
                coord_X.Content = "X: " + e.GetPosition(mainCanvas).X;
                coord_Y.Content = "Y: " + e.GetPosition(mainCanvas).Y;
            }
        }

        private void mainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mouseClicked) { mouseClicked = false; }
            else { mouseClicked = true; }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string coordsX = coord_X.Content as string;
            string coordsY = coord_Y.Content as string;

            X1.Text = coordsX.Substring(3);
            Y1.Text = coordsY.Substring(3);

            mouseClicked = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string coordsX = coord_X.Content as string;
            string coordsY = coord_Y.Content as string;

            X2.Text = coordsX.Substring(3);
            Y2.Text = coordsY.Substring(3);

            mouseClicked = true;
        }
    }
}
