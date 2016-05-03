using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CG_Proj3
{
    class Algorithms
    {
        public static Canvas canvas;

        public static void DrawPixel(Canvas c, int x, int y)
        {
            Rectangle rec = new Rectangle();
            Canvas.SetTop(rec, y);
            Canvas.SetLeft(rec, x);
            rec.Width = 1;
            rec.Height = 1;
            rec.Fill = new SolidColorBrush(Colors.Black);
            c.Children.Add(rec);
        }

        public static void MidpointLine(Canvas c, int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;

            int d = 2*dy - dx;
            int dE = 2*dy;
            int dNE = 2*(dy - dx);
            int x = x1;
            int y = y1;

            DrawPixel(c, x, y);

            if (dx == 0)
            {
                for (int i = y1; i < y2; i++)
                {
                    DrawPixel(c, x1, i);
                }
            }

            if (dy == 0)
            {
                for (int i = x1; i < x2; i++)
                {
                    DrawPixel(c, i, y1);
                }
            }

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
                DrawPixel(c, x, y);
            }
        }

        public static void SymmMidpointLine(Canvas c, int x1, int y1, int x2, int y2)
        {
            int d;
            int dx = x2 - x1;
            int dy = y2 - y1;

            int dE;
            int dNE;

            int sx = (int)Math.Sign(dx);
            int sy = (int)Math.Sign(dy);
            dx = Math.Abs(dx);
            dy = Math.Abs(dy);

            int xf = x1;
            int yf = y1;
            int xb = x2;
            int yb = y2;

            DrawPixel(c, xf, yf);
            DrawPixel(c, xb, yb);

            if (dx == 0) {
                for (int i = y1; i < y2; i++)
                {
                    DrawPixel(c, x1, i);
                }
            }

            if (dy == 0) {
                for (int i = x1; i < x2; i++)
                {
                    DrawPixel(c, i, y1);
                }
            }

            if (dy > dx) {

                dE = 2 * dx;
                dNE = 2 * (dx - dy);
                d = 2 * dx - dy;

                while (yf != y2)
                {
                    xf += sx;
                    xb -= sx;

                    if (d <= 0) {
                        d += dE;
                    }
                    else {
                        d += dNE;
                        yf += sy;
                        yb -= sy;
                    }

                    DrawPixel(c, xf, yf);
                    DrawPixel(c, xb, yb);
                }
            }
            else {

                dE = 2*dy;
                dNE = 2*(dy - dx);
                d = 2*dy - dx;

                while (xf != x2)
                {
                    xf += sx;
                    xb -= sx;

                    if (d <= 0) {
                        d += dE;
                    }
                    else {
                        d += dNE;
                        yf += sy;
                        yb -= sy;
                    }

                    DrawPixel(c, xf, yf);
                    DrawPixel(c, xb, yb);
                }
            }
        }

        public static void MidpointCircle(Canvas c, int x, int y, int R)
        {
            int dE = 3;
            int dSE = 5 - 2*R;
            int d = 1 - R;
            //int x = 0;
            //int y = R;

            DrawPixel(c, x, y);
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
                DrawPixel(c, x, y);
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
