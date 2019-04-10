using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisWPF
{
    [Serializable]
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int xx, int yy)
        {
            this.X = xx;
            this.Y = yy;
        }
    }
}
