using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationTest
{
    public class Lever : IRotatable
    {
        public Axis Axis { get; }
        public PhysicalPoint Point1 { get; }
        public PhysicalPoint Point2 { get; }
        public float Angle { get; set; }

        // Omega - angular velocity
        public float Omega { get; set; }

        // Epsilon - angular acceleration
        public float Epsilon { get; set; }

        // I - moment of inertia
        public float I { get; }

        public Lever(Axis axis, PhysicalPoint point1, PhysicalPoint point2)
        {
            Axis = axis;
            Point1 = point1;
            Point2 = point2;
            Angle = Physics.PI;
            Omega = 0;
            Epsilon = 0;
            I = Physics.CalculateInertiaMoment(this);
            Physics.CalculatePosition(this);
        }

        public void UpdatePosition(float newX1, float newY1, float newX2, float newY2)
        {
            Point1.UpdatePosition(newX1, newY1);
            Point2.UpdatePosition(newX2, newY2);
        }
    }
}
