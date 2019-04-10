using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisWPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /*7 rodzajów blokow narazie implementacja tylko 4*/
    [Serializable]
    public class Block
    {
        public Point point { get; set; }
        public int[,] blockTable = new int[5, 5];
        public int type { get; set; }
        public int minx { get; set; }
        public int maxx { get; set; }
        public int maxy { get; set; }
        public int rot { get; set; }
        public int color { get; set; }
        private BlocksStrukture blockStruk = new BlocksStrukture();

        /*First Constructor*/
        public Block(Block newBlock)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    blockTable[i, j] = newBlock.blockTable[i, j];
                }
            }
            this.type = newBlock.type;
            color = newBlock.color;
            point = new Point(newBlock.point.X, newBlock.point.Y);
            rot = newBlock.rot;
            minx = newBlock.minx;
            maxx = newBlock.maxx;
            maxy = newBlock.maxy;
        }

        /*Second Constructor*/
        public Block(int t, int r)
        {
            point = new Point(1, 1);
            color = t;
            type = t;
            rot = r;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    blockTable[i, j] = blockStruk.blockTypes[t, r, i, j];
                }
            }
            minx = blockStruk.tabminx[type, rot];
            maxx = blockStruk.tabmaxx[type, rot];
            maxy = blockStruk.tabmaxy[type, rot];
        }


        public void rotate(Board b)
        {
            if (rot == 3)
            {
                rot = 0;
            }
            else
            {
                rot++;
            }
            if (b.checkcol(this, 'l') && b.checkcol(this, 'r') && b.checkcol(this, 'd'))
            {
                minx = blockStruk.tabminx[type, rot];
                maxx = blockStruk.tabmaxx[type, rot];
                maxy = blockStruk.tabmaxy[type, rot];
                if (point.Y + minx < 2 && type==1)
                {
                    point.Y = point.Y + 2;
                }
                else if (point.Y + minx < 2)
                {
                    point.Y = point.Y+1;
                }
                if (point.Y + maxx > 9)
                {
                    point.Y = point.Y-2 ;
                }
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        blockTable[i, j] = blockStruk.blockTypes[type, rot, i, j];
                    }
                }
            }


        }
        /**/
        public void down()
        {
            point.X++;
        }
        public void left()
        {
            point.Y--;
        }
        public void right()
        {
            point.Y++;
        }
    }
}

