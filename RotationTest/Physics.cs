using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationTest
{
    public class Physics
    {
        public const float g = 1;
        public const float PI = (float)Math.PI;

        public static void RunLeverDynamics(List<Lever> levers)
        {
            foreach(Lever lever in levers)
            {
                CalculateOmega(lever);
                CalculatePosition(lever);
                ResetEpsilon(lever);
                ApplyGravity(lever);
            }
        }

        private static void ApplyGravity(Lever lever)
        {
            PhysicalPoint p1 = lever.Point1, p2 = lever.Point2;

            ExertForce(lever, p1, 0, g * p1.m);
            ExertForce(lever, p2, 0, g * p2.m);
        }

        public static void ExertForce(Lever lever, PhysicalPoint point, float fx, float fy)
        {
            Axis axis = lever.Axis;
            float rx = point.X - axis.X,
                ry = point.Y - axis.Y,
                M = CrossProduct(rx, ry, fx, fy),
                epsilon = lever.Epsilon + M / lever.I;

            lever.Epsilon = epsilon;
        }

        private static void ResetEpsilon(Lever lever)
        {
            lever.Epsilon = 0;
        }

        private static void CalculateOmega(Lever lever)
        {
            float omega = lever.Omega + lever.Epsilon;

            lever.Omega = omega;
        }

        public static void CalculatePosition(Lever lever)
        {
            Axis axis = lever.Axis;
            float 
                d1 = Distance(axis.X, axis.Y, lever.Point1.X, lever.Point1.Y), 
                d2 = Distance(axis.X, axis.Y, lever.Point2.X, lever.Point2.Y), 
                dAngle = lever.Omega, 
                newAngle = lever.Angle + dAngle, 
                x1 = (float)Math.Cos(newAngle) * -d1 + axis.X, 
                y1 = (float)Math.Sin(newAngle) * -d1 + axis.Y, 
                x2 = (float)Math.Cos(newAngle) * d2 + axis.X, 
                y2 = (float)Math.Sin(newAngle) * d2 + axis.Y;

            lever.Angle = newAngle;
            lever.UpdatePosition(x1, y1, x2, y2);
        }

        public static float CalculateInertiaMoment(Lever lever)
        {
            Axis axis = lever.Axis;
            PhysicalPoint p1 = lever.Point1, p2 = lever.Point2;
            float 
                r1 = Distance(axis.X, axis.Y, p1.X, p1.Y),
                r2 = Distance(axis.X, axis.Y, p2.X, p2.Y);

            return p1.m * r1 * r1 + p2.m * r2 * r2;
        }

        public static float CalculateAngle(Lever lever)
        {
            Axis axis = lever.Axis;
            PhysicalPoint p1 = lever.Point1;
            float dx = p1.X - axis.X,
                dy = p1.Y - axis.Y,
                r = Distance(p1.X, p1.Y, axis.X, axis.Y),
                sin = dy / r,
                cos = dx / r,
                angle = (float)Math.Acos(cos);

            if(sin < 0)
            {
                angle += (PI - angle) * 2;
            }

            return angle;
        }

        private static float Distance(float ax, float ay, float bx, float by)
        {
            float dx = ax - bx,
                dy = ay - by;

            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        private static float CrossProduct(float ax, float ay, float bx, float by)
        {
            return ax * by - ay * bx;
        }
    }
}
