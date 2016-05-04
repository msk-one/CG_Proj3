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

        public static void DrawPixel(Canvas c, int x, int y, int color)
        {
            Rectangle rec = new Rectangle();
            Canvas.SetTop(rec, y);
            Canvas.SetLeft(rec, x);
            rec.Width = 1;
            rec.Height = 1;
            //rec.Fill = new SolidColorBrush(Color.);
            c.Children.Add(rec);
        }

        public static void MidpointLine(Canvas c, int x0, int y0, int x1, int y1)
        {
            bool steep;
            int dx;
            int dy;
            int err;
            int y;
            int ystep;

            //octant handling:
            steep = Math.Abs(x1 - x0) < Math.Abs(y1 - y0);
            if (steep) {
                int t;
                t = x0;
                x0 = y0;
                y0 = t;

                t = x1;
                x1 = y1;
                y1 = t;
            }
            if (x0 > x1) {
                int t;
                t = x0;
                x0 = x1;
                x1 = t;

                t = y0;
                y0 = y1;
                y1 = t;
            }

            dx = x1-x0;
            dy = Math.Abs(y1-y0);
            err = dx/2;
            y = y0;

            if (y0 < y1) {
                ystep = 1;
            }
            else {
                ystep = -1;
            }

            for (int x = x0; x <= x1; x++) {
                DrawPixel(c, (steep ? y : x), (steep ? x : y));

                err -= dy;
                if (err < 0) {
                    err += dx;
                    y += ystep;
                }
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

        public static void MidpointCircle(Canvas c, int x0, int y0, int R)
        {
            int x = 0;
            int y = R;
            int d = 1-R;

            do {
                if (d < 0) {
                    d = d + 2*(++x) + 3;
                }
                else {
                    d = d + 2*(++x) - 2*(--y) + 5;
                }

                DrawPixel(c, x0 + x, y0 + y);
                DrawPixel(c, x0 - x, y0 + y);
                DrawPixel(c, x0 + x, y0 - y);
                DrawPixel(c, x0 - x, y0 - y);
                DrawPixel(c, x0 + y, y0 + x);
                DrawPixel(c, x0 - y, y0 + x);
                DrawPixel(c, x0 + y, y0 - x);
                DrawPixel(c, x0 - y, y0 - x);

            } while (x < y);

        }

        private static float coverage(float d, float r)
        {
            if (d <= r)
            {
                return (float) (0.5 - (d*Math.Sqrt(r*r - d*d)/(Math.PI*r*r)) - (1/(Math.PI*Math.Asin(d/r))));
            }
            else
            {
                return 0;
            }
        }

        //public int colorLerp(int A, int B, int l, int L)
        //{
        //    int Ar = (A >> 16) & 0xff;
        //    int Ag = (A >> 8) & 0xff;
        //    int Ab = A & 0xff;
        //    int Br = (B >> 16) & 0xff;
        //    int Bg = (B >> 8) & 0xff;
        //    int Bb = B & 0xff;

        //    int Yr = (int)(Ar + l * (Br - Ar) / (float)L);
        //    int Yg = (int)(Ag + l * (Bg - Ag) / (float)L);
        //    int Yb = (int)(Ab + l * (Bb - Ab) / (float)L);
        //    return (int) (0xff000000 |
        //                  ((Yr << 16) & 0xff0000) |
        //                  ((Yg << 8) & 0xff00) |
        //                  (Yb & 0xff));
        //}

        private static float lerp(float v0, float v1, float t)
        {
            return (1 - t) * v0 + t * v1;
        }

        private static int IntensifyPixel(Canvas c, int x, int y, float d, float r)
        {
            float cov = coverage(d, r);
            if (cov > 0)
                DrawPixel(c, x, y, (int)lerp(System.Drawing.Color.White.ToArgb(), System.Drawing.Color.Black.ToArgb(), cov));
            return (int) cov;
        }

        public static void GuptaSproull(Canvas c, int x1, int y1, int x2, int y2, float thickness)
        {
            int dx = x2 - x1, dy = y2 - y1;
            int dE = 2 * dy, dNE = 2 * (dy - dx);
            int d = 2 * dy - dx;
            int two_v_dx = 0;
            float invDenom = (float)(1 / (2*Math.Sqrt(dx*dx + dy*dy)));
            float two_dx_invDenom = 2 * dx * invDenom;
            int x = x1, y = y1;
            int i;

            IntensifyPixel(c, x, y, thickness, 0);

            for (i = 1; IntensifyPixel(c, x, y + i, thickness, i * two_dx_invDenom) != 0; ++i) ;
            for (i = 1; IntensifyPixel(c, x, y - i, thickness, i * two_dx_invDenom) != 0; ++i) ;

            while (x < x2)
            {
                ++x;
                if (d < 0)
                {
                    two_v_dx = d+dx;
                    d += dE;
                }
                else
                {
                    two_v_dx = d-dx;
                    d += dNE;
                    ++y;
                }

                IntensifyPixel(c, x, y, thickness, two_v_dx * invDenom);

                for (i = 1; IntensifyPixel(c, x, y + i, thickness, i * two_dx_invDenom - two_v_dx * invDenom) != 0; ++i) ;
                for (i = 1; IntensifyPixel(c, x, y - i, thickness, i * two_dx_invDenom + two_v_dx * invDenom) != 0; ++i) ;
            }
        }
    }
}
