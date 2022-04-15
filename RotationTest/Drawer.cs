using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationTest
{
    public class Drawer
    {
        public static List<Lever> Levers { get; set; }

        private static Bitmap bmp;
        private static Graphics g;
        private const float defaultAxisSize = 8;
        private const float defaultPointSize = 16;
        private const float defaultLeverSize = 8;
        public static void Draw(PictureBox pb)
        {
            if (bmp != null)
                bmp.Dispose();
            if (g != null)
                g.Dispose();

            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);

            foreach(Lever lever in Levers)
            {
                DrawLever(lever);
                DrawInfo(lever);
            }

            pb.Image = bmp;
        }

        private static void DrawAxis(Axis axis)
        {
            DrawBall(axis.X, axis.Y, defaultAxisSize, Color.Red);
        }

        private static void DrawPoint(PhysicalPoint point)
        {
            int blue = 255 / (int)(point.m),
                red = 255 - blue;
            Color color = Color.FromArgb(255, red, 0, blue);

            DrawBall(point.X, point.Y, defaultPointSize, color);
        }

        private static void DrawBall(float x, float y, float size, Color color)
        {
            Brush brush = new SolidBrush(color);

            g.FillEllipse(
                brush,
                x - size / 2,
                y - size / 2,
                size,
                size);
        }

        private static void DrawLever(Lever lever)
        {
            Pen pen = new Pen(Color.Black, defaultLeverSize);

            g.DrawLine(
                pen, 
                lever.Point1.X, 
                lever.Point1.Y, 
                lever.Point2.X, 
                lever.Point2.Y);

            DrawPoint(lever.Point1);
            DrawPoint(lever.Point2);
            DrawAxis(lever.Axis);
        }

        private static void DrawInfo(Lever lever)
        {
            Brush brush = new SolidBrush(Color.Black);
            Font font = new Font(new FontFamily("Arial"), 12, FontStyle.Regular);

            string infoI = "Момент инерции: " + lever.I;
            string infoEpsilon = "Угловое ускорение: " + lever.Epsilon + " рад/с^2";
            string infoOmega = "Угловая скорость: " + lever.Omega + " рад/с";
            string infoAngle = "Угол поворота: " + lever.Angle + " рад";
            string info = infoI + "\n" + infoEpsilon + "\n" + infoOmega + "\n" + infoAngle;

            g.DrawString(info, font, brush, lever.Axis.X + 150, lever.Axis.Y - 50);
        }
    }
}
