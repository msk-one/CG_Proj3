using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CG_Proj3
{
    class Algorithms
    {
        private void AddPixel(Canvas c, double x, double y)
        {
            Rectangle rec = new Rectangle();
            Canvas.SetTop(rec, y);
            Canvas.SetLeft(rec, x);
            rec.Width = 1;
            rec.Height = 1;
            rec.Fill = new SolidColorBrush(Colors.Black);
            c.Children.Add(rec);
        }

        public void MidpointLine(Canvas c, int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;

            int d = 2*dy - dx;
            int dE = 2*dy;
            int dNE = 2*(dy - dx);
            int x = x1, y = y1;

            AddPixel(c, x, y);
            while (x < x2)
            {
                if (d < 0)
                {
                    d += dE;
                    x++;
                }
                else
                {
                    d += dNE;
                    ++x;
                    ++y;
                }
                AddPixel(c, x, y);
            }
        }

        public void SymmetricMidpointLine(Canvas c, int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;

            int d = 2*dy - dx;
            int dE = 2*dy;
            int dNE = 2*(dy - dx);
            int xf = x1, yf = y1;
            int xb = x2, yb = y2;

            AddPixel(c, xf, yf);
            AddPixel(c, xb, yb);

            while (xf < xb)
            {
                ++xf;
                --xb;
                if (d < 0)
                    d += dE;
                else
                {
                    d += dNE;
                    ++yf;
                    --yb;
                }

                AddPixel(c, xf, yf);
                AddPixel(c, xb, yb);
            }
        }

        public void MidpointCircle(Canvas c, int R)
        {
            int dE = 3;
            int dSE = 5 - 2*R;
            int d = 1 - R;
            int x = 0;
            int y = R;

            AddPixel(c, x, y);
            while (y > x)
            {
                if (d < 0)
                {
                    d += dE;
                    dE += 2;
                    dSE += 2;
                }
                else
                {
                    d += dSE;
                    dE += 2;
                    dSE += 4;
                    --y;
                }
                ++x;
                AddPixel(c, x, y);
            }

        }

        //int IntensifyPixel(Canvas c, int x, int y, float thickness, float distance)
        //{
        //    float cov = coverage(thickness, distance);
        //    if (cov > 0)
        //        AddPixel(c, x, y, lerp(BKG_COLOR, LINE_COLOR, cov));
        //    return cov;
        //}

        //public int GuptaSproull(int x1, int y1, int x2, int y2, float thickness)
        //{
        //    int dx = x2 - x1, dy = y2 - y1;
        //    int dE = 2*dy, dNE = 2*(dy - dx);
        //    int d = 2*dy - dx;
        //    int two_v_dx = 0; //numerator, v=0 for the first pixel
        //    float invDenom = (float) (1/(2*Math.Sqrt(dx*dx + dy*dy)));
        //    float two_dx_invDenom = 2*dx*invDenom; //precomputed constant
        //    int x = x1, y = y1;
        //    int i;
        //    IntensifyPixel(x, y, thickness, 0);
        //    for (i = 1; IntensifyPixel(x, y + i, thickness, i*two_dx_invDenom); ++i) ;
        //    for (i = 1; IntensifyPixel(x, y - i, thickness, i*two_dx_invDenom); ++i) ;
        //    while (x < x2)
        //    {
        //        ++x;
        //        if (d < 0) // move to E
        //        {
        //            two_v_dx = d + dx;
        //            d += dE;
        //        }
        //        else // move to NE
        //        {
        //            two_v_dx = d - dx;
        //            d += dNE;
        //            ++y;
        //        }

        //        IntensifyPixel(x, y, thickness, two_v_dx*invDenom);
        //        for (i = 1; IntensifyPixel(x, y + i, thickness, i*two_dx_invDenom - two_v_dx*invDenom); ++i);
        //        for (i = 1; IntensifyPixel(x, y - i, thickness, i*two_dx_invDenom + two_v_dx*invDenom); ++i);
        //    }
        //}
    }
}
