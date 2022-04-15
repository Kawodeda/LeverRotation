using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotationTest
{
    public interface IRotatable
    {
        float Angle { get; set; }
        float Omega { get; set; }
        float Epsilon { get; set; }
        float I { get;}
    }
}
