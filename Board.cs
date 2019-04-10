using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisWPF
{
    [Serializable]
    public class Board
    {
        public BoardCell[,] GameBoard;
        public int Scores = 0;
        public int lines = 0;

        [Serializable]
        public class BoardCell
        {
            public int isfree;
            public int color;
        }

        public Board()
        {
            GameBoard = new BoardCell[22, 10];

            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    GameBoard[i, j] = new BoardCell();
                    GameBoard[i, j].isfree = 0;
                }
            }
        }

        public bool checkcol(Block b, char d)
        {
            if (d == 'l')
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (b.blockTable[i, j] > 0)
                        {
                            try
                            {
                                if (GameBoard[b.point.X + i , b.point.Y + j-1].isfree == 1)
                                {
                                    return false;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {

                            }

                        }
                    }
                }
            }

            else if (d == 'r')
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {

                        if (b.blockTable[i, j] > 0)
                        {
                            try
                            {
                                if (GameBoard[b.point.X + i, b.point.Y + j+1].isfree == 1)
                                {
                                    return false;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {

                            }

                        }
                    }
                }
            }

            else if (d == 'd')
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (b.blockTable[i, j] > 0)
                        {
                            try
                            {
                                if (GameBoard[b.point.X + i+1, b.point.Y + j].isfree > 0)
                                {
                                    return false;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {

                            }

                        }
                    }
                }
            }
            return true;
        }

        public void lineErase()
        {
            //zbijanie linii
            bool line = false;
            int erasedlinecounter = 0;
            int ii = 20;
            while (ii > 0)
            {
                line = true;
                for (int j = 0; j < 10; j++) // sprawdzenie czy caly rzad zajety
                {
                    if (GameBoard[ii, j].isfree == 0)
                    {
                        line = false;
                        break;
                    }
                }
                if (line)
                {
                    erasedlinecounter++;
                    for (int j = 0; j < 10; j++) // usuniecie linii
                    {
                        GameBoard[ii, j].isfree = 0;
                    }
                    for (int k = ii; k > 0; k--)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            GameBoard[k, j].isfree = GameBoard[k-1, j].isfree;
                        }
                    }           
                }
                ii--;
            }
        }


        public void logicForMotion(ref Block actual, ref Block next)
        {
            ////////////////////////////////////////////
            /////////// LOGIKA ////////////////////////
            //////////////////////////////////////////
            if (actual.point.X + actual.maxy > 18 || !checkcol(actual, 'd'))
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                                try
                                {
                                    if (actual.blockTable[i, j] > 0)
                                    {
                                        GameBoard[actual.point.X+i, actual.point.Y+j].isfree = 1;
                                        GameBoard[actual.point.X + i, actual.point.Y+j].color = actual.color;
                                    }
                                }
                                catch (IndexOutOfRangeException)
                                {

                                }
                    }
                }
                Random rnd = new Random();
                int randblock = rnd.Next(7);
                int randrot = rnd.Next(4);
                actual = new Block(next);
                next = new Block(randblock, randrot);


                actual.point.X = 0;
                actual.point.Y = 4;

            }
        }

        public Boolean checkGameOver(Board b)
        {
            for (int i = 0; i < 10; i++)
            {
                if (b.GameBoard[i, 1].isfree == 1)
                {
                    return true;
                }
            }
            return false;
        }




    }
}
