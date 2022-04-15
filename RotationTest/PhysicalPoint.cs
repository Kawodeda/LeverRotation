using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationTest
{
    public class PhysicalPoint
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float m { get; }

        public PhysicalPoint(float x, float y, float _m)
        {
            X = x;
            Y = y;
            m = _m;
        }

        public void UpdatePosition(float newX, float newY)
        {
            X = newX;
            Y = newY;
        }
    }
}
