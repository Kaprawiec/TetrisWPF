using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;

namespace TetrisWPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Rectangle> nwtlst = new List<Rectangle>();
        private Rectangle Rect;
        private Rectangle previous;
        private Board b = new Board();
        private Block actualBlock;
        private Block nextBlock;
        private Random rnd = new Random();
        private Boolean rotation;
        ImageBrush tlo = new ImageBrush();
        private int speed;
        private List<Player> recordList = new List<Player>();
        private Player newPlaye;
        long frames = 1;
        private DispatcherTimer _timer;
        DispatcherTimer gameTimer = new DispatcherTimer();
        ImageBrush[] c= new ImageBrush[7];


        public void initMainWindow()
        {
            /*Set Dimension to */
            for (int i = 0; i < 10; i++)
            {
                ColumnDefinition columnDefinitions = new ColumnDefinition();
                columnDefinitions.Width = new GridLength(20);
                grd1.ColumnDefinitions.Add(columnDefinitions);
            }
            for (int j = 0; j < 20; j++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(20);
                grd1.RowDefinitions.Add(rowDefinition);
            }
            speed = 100;
            for (int i = 0; i < 7; i++) c[i] = new ImageBrush();
            c[0].ImageSource = new BitmapImage(new Uri("images/czerwony.png", UriKind.Relative));
            c[1].ImageSource = new BitmapImage(new Uri("images/zolty.png", UriKind.Relative));
            c[2].ImageSource = new BitmapImage(new Uri("images/zielony.png", UriKind.Relative));
            c[3].ImageSource = new BitmapImage(new Uri("images/niebieski.png", UriKind.Relative));
            c[4].ImageSource = new BitmapImage(new Uri("images/blekit.png", UriKind.Relative));
            c[5].ImageSource = new BitmapImage(new Uri("images/fiolet.png", UriKind.Relative));
            c[6].ImageSource = new BitmapImage(new Uri("images/pomarancz.png", UriKind.Relative));
            tlo.ImageSource = new BitmapImage(new Uri("images/tlo.png", UriKind.Relative));



        }

        public void initGame (int Speed)
        {
            actualBlock = new Block(rnd.Next(7), rnd.Next(4));
            nextBlock = new Block(rnd.Next(7), rnd.Next(4));
            actualBlock.point.X = 0;
            actualBlock.point.Y = 0;
            this.speed=Speed;
           
        }

        public MainWindow()
        {
            InitializeComponent();
            initMainWindow();
            initGame(10);
            gameTimer.Interval = TimeSpan.FromMilliseconds(300);
            gameTimer.Tick += new EventHandler(gameTimer_Tick);
            gameTimer.Start();
        }


        void gameTimer_Tick(object sender, EventArgs e)
        {
            b.logicForMotion(ref actualBlock, ref nextBlock);
            if (actualBlock.point.X < 18 && b.checkcol(actualBlock, 'd'))
            {
                actualBlock.down();
                
            }
           
            b.lineErase();
            redraw();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (gameTimer.IsEnabled && actualBlock.point.Y + actualBlock.minx > 0 && b.checkcol(actualBlock, 'l'))
                        actualBlock.left();
                    redraw();
                    break;
                case Key.Right:
                    if (gameTimer.IsEnabled && actualBlock.point.Y + actualBlock.maxx < 9 && b.checkcol(actualBlock, 'r'))
                        actualBlock.right();
                    redraw();
                    break;
                case Key.Down:
                    if (gameTimer.IsEnabled && actualBlock.point.X + actualBlock.maxy < 18 && b.checkcol(actualBlock, 'd'))
                    {
                        actualBlock.down();
                        redraw();
                    }

                    break;
                case Key.Up:
                    actualBlock.rotate(b);
                    redraw();
                    break;
            }
        }

        public void redraw ()
        {
            all.Content = "";
            //Przerysowanie od gory 
            foreach(Rectangle r in nwtlst)
            {
                grd1.Children.Remove(r);
            }
            nwtlst.Clear();
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                        Rect = new Rectangle();
                        Rect.Width = Rect.Height = 20;
                        nwtlst.Add(Rect);
                        Rect.Fill = tlo;
                        Grid.SetColumn(Rect, j);
                        Grid.SetRow(Rect, (i));
                        grd1.Children.Add(Rect);
                }
            }      
           for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (b.GameBoard[i, j].isfree == 1)
                    {
                        Rect = new Rectangle();
                        Rect.Width = Rect.Height = 20;
                        nwtlst.Add(Rect);
                        Rect.Fill = c[b.GameBoard[i,j].color];
                        Grid.SetColumn(Rect, j);
                        Grid.SetRow(Rect, (i));
                        grd1.Children.Add(Rect);
                    }
                }
            }            
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (actualBlock.blockTable[i, j] > 0)
                    {
                        Rect = new Rectangle();
                        Rect.Width = Rect.Height = 20;
                        nwtlst.Add(Rect);
                        Rect.Fill = c[actualBlock.type];
                        Grid.SetColumn(Rect, j + actualBlock.point.Y );
                        Grid.SetRow(Rect, (i + actualBlock.point.X));
                        grd1.Children.Add(Rect);

                    }
                }
            }
            all.Content = "x: " + actualBlock.point.X.ToString() + " Y:" + actualBlock.point.Y.ToString() + "maxy: "+actualBlock.maxy;
           /* for(int i = 20;i>0;i--)
            {
                for (int j = 0; j < 10; j++) all.Content += b.GameBoard[i, j].isfree.ToString();
                all.Content += "\n";
            }*/
            
        }
    }
}
